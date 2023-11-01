namespace ImageGallery.Infrastructure.Data;

public class DatabaseCredentialsOptions
{
    public static string SectionName = "DatabaseCredentials"; 
    public string ConnectionString { get; set; } = string.Empty;

    public string DbUser { get; set; }

    public string DbPassword { get; set; }

    public string InitialCatalog { get; set; }

    public string DataSource { get; set; }
}