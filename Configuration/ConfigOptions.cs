namespace AkariLevelEditor.Configuration;

public class ConfigOptions(Config configuration) : ConfigurationOptions(configuration)
{
    public new ConfigOptions CopyDefaults(bool value)
    {
        base.CopyDefaults = value;
        return this;
    }

    public new ConfigOptions PathSeparator(char value)
    {
        base.PathSeparator = value;
        return this;
    }
}