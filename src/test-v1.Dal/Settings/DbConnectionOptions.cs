namespace test_v1.Dal.Settings;

public class DbConnectionOptions
{
    public string? Server { get; set; }
    public string? Database { get; set; }
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string ConnectionString =>
        $"Host={Server}; Database={Database};Username={UserId}; Password={Password};";
}