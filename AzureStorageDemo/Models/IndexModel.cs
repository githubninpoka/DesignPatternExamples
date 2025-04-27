namespace AzureStorageDemo.Models;

public class IndexModel
{
    public string? FileUrl { get; set; }
    public List<string> PageErrors { get; set; } = new();
}
