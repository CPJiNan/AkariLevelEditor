namespace AkariLevelEditor.Configuration;

public interface IConfigurationSection
{
    HashSet<object> GetKeys(bool deep);
    Dictionary<object, object> GetValues(bool deep);
    bool Contains(string path);
    bool IsSet(string path);
    string GetCurrentPath();
    string GetName();
    IConfiguration GetRoot();
    IConfigurationSection GetParent();
    object Get(string path);
    object Get(string path, object def);
    void Set(string path, object value);
    IConfigurationSection CreateSection(string path);
    IConfigurationSection CreateSection(string path, Dictionary<string, object> map);
    string GetString(string path);
    string GetString(string path, string def);
    bool IsString(string path);
    int GetInt(string path);
    int GetInt(string path, int def);
    bool IsInt(string path);
    bool GetBoolean(string path);
    bool GetBoolean(string path, bool def);
    bool IsBoolean(string path);
    double GetDouble(string path);
    double GetDouble(string path, double def);
    bool IsDouble(string path);
    long GetLong(string path);
    long GetLong(string path, long def);
    bool IsLong(string path);
    List<T> GetList<T>(string path);
    List<T> GetList<T>(string path, List<T> def);
    bool IsList(string path);
    List<string> GetStringList(string path);
    List<int> GetIntegerList(string path);
    List<bool> GetBooleanList(string path);
    List<double> GetDoubleList(string path);
    List<float> GetFloatList(string path);
    List<long> GetLongList(string path);
    List<byte> GetByteList(string path);
    List<char> GetCharacterList(string path);
    List<short> GetShortList(string path);
    List<Dictionary<object, object>> GetMapList(string path);
    bool IsConfigurationSection(string path);
    IConfigurationSection GetConfigurationSection(string path);
}