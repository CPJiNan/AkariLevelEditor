namespace AkariLevelEditor.Configuration;

public class ConfigurationOptions(IConfiguration configuration)
{
    public char PathSeparator { get; set; } = '.';

    public bool CopyDefaults { get; set; }
}