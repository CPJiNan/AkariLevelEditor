namespace AkariLevelEditor.Configuration;

public class Config : ConfigSection, IConfiguration
{
    private IConfiguration _defaults;
    private ConfigOptions _options;

    public Config()
    {
    }

    public Config(IConfiguration defaults)
    {
        _defaults = defaults ?? throw new ArgumentNullException(nameof(defaults));
    }

    public new void AddDefault(string path, object value)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));

        if (_defaults == null) _defaults = new Config();

        _defaults.Set(path, value);
    }

    public void AddDefaults(Dictionary<object, object> defaults)
    {
        if (defaults == null)
            throw new ArgumentNullException(nameof(defaults));

        foreach (var entry in defaults) AddDefault(entry.Key.ToString(), entry.Value);
    }

    public void AddDefaults(IConfiguration defaults)
    {
        if (defaults == null)
            throw new ArgumentNullException(nameof(defaults));

        AddDefaults(defaults.GetValues(true));
    }

    public void SetDefaults(IConfiguration defaults)
    {
        _defaults = defaults ?? throw new ArgumentNullException(nameof(defaults));
    }

    public IConfiguration GetDefaults()
    {
        return _defaults;
    }

    public new IConfigurationSection GetParent()
    {
        return null;
    }

    public ConfigurationOptions Options()
    {
        return _options ?? (_options = new ConfigOptions(this));
    }
}