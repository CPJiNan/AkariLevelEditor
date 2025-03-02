namespace AkariLevelEditor.Configuration;

public class ConfigOptions : ConfigurationOptions
{
    public ConfigOptions(Config configuration) : base(configuration)
    {
    }

    public new Config Configuration()
    {
        return (Config)base.Configuration();
    }

    public new ConfigOptions CopyDefaults(bool value)
    {
        base.CopyDefaults(value);
        return this;
    }

    public new ConfigOptions PathSeparator(char value)
    {
        _PathSeparator = value;
        return this;
    }
}