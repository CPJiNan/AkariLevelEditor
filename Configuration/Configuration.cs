namespace AkariLevelEditor.Configuration;

public interface IConfiguration : IConfigurationSection
{
    void AddDefault(string path, object value);

    void AddDefaults(Dictionary<object, object> defaults);

    void AddDefaults(IConfiguration defaults);

    void SetDefaults(IConfiguration defaults);

    IConfiguration GetDefaults();

    ConfigurationOptions Options();
}