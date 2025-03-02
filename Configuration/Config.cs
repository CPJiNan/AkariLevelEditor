namespace AkariLevelEditor.Configuration;

public class Config : ConfigSection, IConfiguration
{
    private IConfiguration? _defaults;
    private ConfigOptions? _options;

    public Config()
    {
    }

    public Config(IConfiguration defaults)
    {
        _defaults = defaults;
    }

    public new void AddDefault(string path, object? value)
    {
        ArgumentNullException.ThrowIfNull(path);

        _defaults ??= new Config();

        _defaults.Set(path, value);
    }

    public void AddDefaults(Dictionary<string, object> defaults)
    {
        ArgumentNullException.ThrowIfNull(defaults);

        foreach (var entry in defaults) AddDefault(entry.Key, entry.Value);
    }

    public void AddDefaults(IConfiguration defaults)
    {
        ArgumentNullException.ThrowIfNull(defaults);

        AddDefaults(defaults.GetValues(true));
    }

    public void SetDefaults(IConfiguration defaults)
    {
        _defaults = defaults ?? throw new ArgumentNullException(nameof(defaults));
    }

    public IConfiguration? GetDefaults()
    {
        return _defaults;
    }

    public new IConfigurationSection? GetParent()
    {
        return null;
    }

    public ConfigurationOptions Options()
    {
        return _options ??= new ConfigOptions(this);
    }
}