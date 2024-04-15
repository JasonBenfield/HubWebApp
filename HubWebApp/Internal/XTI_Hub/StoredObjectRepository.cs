using Microsoft.EntityFrameworkCore;
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

    public async Task<string> AddOrUpdate(StorageName storageName, IGeneratedKey generatedStorageKey, string data, DateTimeOffset timeExpires, bool isSingleUse, TimeSpan expirationTimeSpan)
    {
        var storedObject = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync(so => so.StorageName == storageName.Value && so.Data == data);
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
                Data = data,
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

    public async Task<string> StoredObject(StorageName storageName, string storageKey, DateTimeOffset now)
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
        if(storedObject != null)
        {
            if (storedObject.IsSingleUse)
            {
                await factory.DB.StoredObjects.Delete(storedObject);
            }
            else if(TimeSpan.TryParse(storedObject.ExpirationTimeSpan, out var expirationTimeSpan) && !expirationTimeSpan.Equals(TimeSpan.Zero))
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
