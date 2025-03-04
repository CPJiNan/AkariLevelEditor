namespace AkariLevelEditor.Configuration;

public class ConfigurationOptions(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;
    public char PathSeparator { get; set; } = '.';
    public bool CopyDefaults { get; set; }
}