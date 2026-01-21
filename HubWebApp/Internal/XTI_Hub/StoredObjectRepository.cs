using Microsoft.EntityFrameworkCore;
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

    public Task<string> Store(StorageName storageName, GenerateKeyModel generateKey, object data, IClock clock, TimeSpan expireAfter, bool isSlidingExpiration, CancellationToken ct) =>
        AddOrUpdate(storageName, new GeneratedKeyFactory().Create(generateKey), data, clock.Now().Add(expireAfter), false, isSlidingExpiration ? expireAfter : TimeSpan.Zero, ct);

    public Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, IClock clock, TimeSpan expireAfter, CancellationToken ct) =>
        AddOrUpdate(storageName, new GeneratedKeyFactory().Create(generateKey), data, clock.Now().Add(expireAfter), true, TimeSpan.Zero, ct);

    private async Task<string> AddOrUpdate(StorageName storageName, IGeneratedKey generatedStorageKey, object data, DateTimeOffset timeExpires, bool isSingleUse, TimeSpan expirationTimeSpan, CancellationToken ct)
    {
        var serializedData = data is string dataStr ? dataStr : XtiSerializer.Serialize(data);
        var storedObject = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync(so => so.StorageName == storageName.Value && so.Data == serializedData, ct);
        if (storedObject == null)
        {
            var storageKey = generatedStorageKey.Value();
            var keyAttempts = 1;
            var keyExists = await DoesKeyExist(storageName, storageKey, ct);
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
                keyExists = await DoesKeyExist(storageName, storageKey, ct);
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
            await factory.DB.StoredObjects.Create(storedObject, ct);
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
                },
                ct
            );
        }
        return storedObject.StorageKey;
    }

    private Task<bool> DoesKeyExist(StorageName storageName, string storageKey, CancellationToken ct) =>
        factory.DB.StoredObjects.Retrieve()
            .AnyAsync(so => so.StorageName == storageName.Value && so.StorageKey == storageKey, ct);

    public Task<T> StoredObject<T>(StorageName storageName, string storageKey, DateTimeOffset now, int singleUseExpirationInSeconds, CancellationToken ct) where T : new() =>
        StoredObject(storageName, storageKey, now, singleUseExpirationInSeconds, () => new T(), ct);

    public async Task<T> StoredObject<T>(StorageName storageName, string storageKey, DateTimeOffset now, int singleUseExpirationInSeconds, Func<T> ifnull, CancellationToken ct)
    {
        var serialized = await SerializedStoredObject(storageName, storageKey, now, singleUseExpirationInSeconds, ct);
        return string.IsNullOrWhiteSpace(serialized) ? ifnull() : XtiSerializer.Deserialize<T>(serialized, ifnull);
    }

    public async Task<string> SerializedStoredObject(StorageName storageName, string storageKey, DateTimeOffset now, int singleUseExpirationInSeconds, CancellationToken ct)
    {
        var storedObject = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync
            (
                so =>
                    so.StorageName == storageName.Value &&
                    so.StorageKey == storageKey &&
                    so.TimeExpires >= now,
                ct
            );
        var data = storedObject?.Data ?? "";
        if (storedObject != null)
        {
            if (storedObject.IsSingleUse)
            {
                if(singleUseExpirationInSeconds <= 0)
                {
                    await factory.DB.StoredObjects.Delete(storedObject, ct);
                }
                else
                {
                    var expirationTime = now.AddSeconds(singleUseExpirationInSeconds);
                    if(storedObject.TimeExpires > expirationTime)
                    {
                        await factory.DB.StoredObjects.Update
                        (
                            storedObject,
                            so =>
                            {
                                so.TimeExpires = expirationTime;
                            },
                            ct
                        );
                    }
                }
            }
            else if (TimeSpan.TryParse(storedObject.ExpirationTimeSpan, out var expirationTimeSpan) && !expirationTimeSpan.Equals(TimeSpan.Zero))
            {
                await factory.DB.StoredObjects.Update
                (
                    storedObject,
                    so =>
                    {
                        so.TimeExpires = now.Add(expirationTimeSpan);
                    },
                    ct
                );
            }
        }
        return data;
    }

    public async Task DeleteExpired(DateTimeOffset expiredBefore, CancellationToken ct)
    {
        var storedObjects = await factory.DB.StoredObjects.Retrieve()
            .Where(so => so.TimeExpires < expiredBefore)
            .ToArrayAsync(ct);
        foreach (var storedObject in storedObjects)
        {
            await factory.DB.StoredObjects.Delete(storedObject, ct);
        }
    }
}
