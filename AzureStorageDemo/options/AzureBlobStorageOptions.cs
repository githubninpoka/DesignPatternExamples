namespace AzureStorageDemo.options;

public class AzureBlobStorageOptions
{
    public string? ConnectionString { get; set; }
    public string? ContainerName { get; set; }

    public  string? StorageAccountKey { get; set; }
    public string? StorageAccountName { get; set; }
}
