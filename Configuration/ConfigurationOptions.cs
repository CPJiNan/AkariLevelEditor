namespace AkariLevelEditor.Configuration;

public class ConfigurationOptions
{
    private readonly IConfiguration _configuration;

    protected ConfigurationOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected IConfiguration Configuration()
    {
        return _configuration;
    }

    public char _PathSeparator { get; protected set; } = '.';

    public bool _CopyDefaults { get; private set; }

    public ConfigurationOptions PathSeparator(char value)
    {
        _PathSeparator = value;
        return this;
    }

    protected ConfigurationOptions CopyDefaults(bool value)
    {
        _CopyDefaults = value;
        return this;
    }
}