using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class StoredObjectRepository
{
    private readonly HubFactory factory;

    public StoredObjectRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<string> Store(StorageName storageName, GenerateKeyModel generateKey, object data, IClock clock, TimeSpan expireAfter, bool isSlidingExpiration) =>
        AddOrUpdate(storageName, new GeneratedKeyFactory().Create(generateKey), data, clock.Now().Add(expireAfter), false, isSlidingExpiration ? expireAfter : TimeSpan.Zero);

    public Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, IClock clock, TimeSpan expireAfter) =>
        AddOrUpdate(storageName, new GeneratedKeyFactory().Create(generateKey), data, clock.Now().Add(expireAfter), true, TimeSpan.Zero);

    private async Task<string> AddOrUpdate(StorageName storageName, IGeneratedKey generatedStorageKey, object data, DateTimeOffset timeExpires, bool isSingleUse, TimeSpan expirationTimeSpan)
    {
        var serializedData = data is string dataStr ? dataStr : XtiSerializer.Serialize(data);
        var storedObject = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync(so => so.StorageName == storageName.Value && so.Data == serializedData);
        if (storedObject == null)
        {
            var storageKey = generatedStorageKey.Value();
            var keyAttempts = 1;
            var keyExists = await DoesKeyExist(storageName, storageKey);
            if (keyExists && generatedStorageKey is FixedGeneratedKey)
            {
                throw new Exception("Unable to generate a unique key");
            }
            while (keyExists)
            {
                storageKey = generatedStorageKey.Value();
                keyAttempts++;
                if (keyAttempts > 100)
                {
                    throw new Exception("Unable to generate a unique key");
                }
                keyExists = await DoesKeyExist(storageName, storageKey);
            }
            storedObject = new StoredObjectEntity
            {
                StorageName = storageName.Value,
                StorageKey = storageKey,
                Data = serializedData,
                TimeExpires = timeExpires,
                IsSingleUse = isSingleUse,
                ExpirationTimeSpan = expirationTimeSpan.ToString()
            };
            await factory.DB.StoredObjects.Create(storedObject);
        }
        else
        {
            await factory.DB.StoredObjects.Update
            (
                storedObject,
                so =>
                {
                    so.TimeExpires = timeExpires;
                    so.IsSingleUse = isSingleUse;
                    so.ExpirationTimeSpan = expirationTimeSpan.ToString();
                }
            );
        }
        return storedObject.StorageKey;
    }

    private Task<bool> DoesKeyExist(StorageName storageName, string storageKey) =>
        factory.DB.StoredObjects.Retrieve()
            .AnyAsync(so => so.StorageName == storageName.Value && so.StorageKey == storageKey);

    public Task<T> StoredObject<T>(StorageName storageName, string storageKey, DateTimeOffset now) where T : new() =>
        StoredObject(storageName, storageKey, now, () => new T());

    public async Task<T> StoredObject<T>(StorageName storageName, string storageKey, DateTimeOffset now, Func<T> ifnull)
    {
        var serialized = await SerializedStoredObject(storageName, storageKey, now);
        return string.IsNullOrWhiteSpace(serialized) ? ifnull() : XtiSerializer.Deserialize<T>(serialized, ifnull);
    }

    public async Task<string> SerializedStoredObject(StorageName storageName, string storageKey, DateTimeOffset now)
    {
        var storedObject = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync
            (
                so =>
                    so.StorageName == storageName.Value &&
                    so.StorageKey == storageKey &&
                    so.TimeExpires >= now
            );
        var data = storedObject?.Data ?? "";
        if (storedObject != null)
        {
            if (storedObject.IsSingleUse)
            {
                await factory.DB.StoredObjects.Delete(storedObject);
            }
            else if (TimeSpan.TryParse(storedObject.ExpirationTimeSpan, out var expirationTimeSpan) && !expirationTimeSpan.Equals(TimeSpan.Zero))
            {
                await factory.DB.StoredObjects.Update
                (
                    storedObject,
                    so =>
                    {
                        so.TimeExpires = now.Add(expirationTimeSpan);
                    }
                );
            }
        }
        return data;
    }

    public async Task DeleteExpired(DateTimeOffset expiredBefore)
    {
        var storedObjects = await factory.DB.StoredObjects.Retrieve()
            .Where(so => so.TimeExpires < expiredBefore)
            .ToArrayAsync();
        foreach (var storedObject in storedObjects)
        {
            await factory.DB.StoredObjects.Delete(storedObject);
        }
    }
}
