using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class StoredObjectRepository
{
    private readonly HubFactory factory;

    public StoredObjectRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<string> AddOrUpdate(StorageName storageName, IGeneratedStorageKey generatedStorageKey, string data, DateTimeOffset timeExpires)
    {
        var entity = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync(so => so.StorageName == storageName.Value && so.Data == data);
        if (entity == null)
        {
            var storageKey = generatedStorageKey.Value();
            var keyAttempts = 1;
            var keyExists = await checkKeyExists(storageName, storageKey);
            while (keyExists)
            {
                storageKey = generatedStorageKey.Value();
                keyAttempts++;
                if(keyAttempts > 100)
                {
                    throw new Exception("Unable to generate a unique key");
                }
                keyExists = await checkKeyExists(storageName, storageKey);
            }
            entity = new StoredObjectEntity
            {
                StorageName = storageName.Value,
                StorageKey = storageKey,
                Data = data,
                TimeExpires = timeExpires
            };
            await factory.DB.StoredObjects.Create(entity);
        }
        else if(entity.TimeExpires != timeExpires)
        {
            await factory.DB.StoredObjects.Update(entity, so => so.TimeExpires = timeExpires);
        }
        return entity.StorageKey;
    }

    private Task<bool> checkKeyExists(StorageName storageName, string storageKey) =>
        factory.DB.StoredObjects.Retrieve()
            .AnyAsync(so => so.StorageName == storageName.Value && so.StorageKey == storageKey);

    public async Task<string> StoredObject(StorageName storageName, string storageKey, DateTimeOffset now)
    {
        var entity = await factory.DB.StoredObjects.Retrieve()
            .FirstOrDefaultAsync
            (
                so =>
                    so.StorageName == storageName.Value &&
                    so.StorageKey == storageKey &&
                    so.TimeExpires >= now
            );
        return entity?.Data ?? "";
    }
}
