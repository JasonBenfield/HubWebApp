namespace XTI_Hub.Abstractions;

public sealed class StoredObjectFactory
{
    private readonly IStoredObjectDB db;

    public StoredObjectFactory(IStoredObjectDB db)
    {
        this.db = db;
    }

    public StoredObject CreateStoredObject(StorageName storageName) => 
        new StoredObject(db, storageName);
}
