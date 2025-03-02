using System.Text;

namespace AkariLevelEditor.Configuration;

public class ConfigSection : IConfigurationSection
{
    private readonly Dictionary<object, object> _map = new();
    private readonly IConfiguration _root;
    private readonly IConfigurationSection _parent;
    private readonly string _path;
    private readonly string _fullPath;

    protected ConfigSection()
    {
        _path = "";
        _fullPath = "";
        _parent = null;
        _root = (IConfiguration)this;
    }

    private ConfigSection(IConfigurationSection parent, string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentException(@"Path cannot be null or empty", nameof(path));

        _path = path;
        _parent = parent ?? throw new ArgumentNullException(nameof(parent), @"Parent cannot be null");
        _root = parent.GetRoot();

        if (_root == null) throw new InvalidOperationException("Path cannot be orphaned");

        _fullPath = CreatePath(parent, path);
    }

    public HashSet<object> GetKeys(bool deep)
    {
        var result = new HashSet<object>();

        var root = GetRoot();
        if (root != null && root.Options()._CopyDefaults)
        {
            var defaults = GetDefaultSection();

            if (defaults != null) result.UnionWith(defaults.GetKeys(deep));
        }

        MapChildrenKeys(result, this, deep);

        return result;
    }

    public Dictionary<object, object> GetValues(bool deep)
    {
        var result = new Dictionary<object, object>();

        foreach (var kvp in _map)
            if (kvp.Value is ConfigSection section && deep)
                result[kvp.Key] = section.GetValues(true);
            else
                result[kvp.Key] = kvp.Value;

        return result;
    }

    public bool Contains(string path)
    {
        return Get(path) != null;
    }

    public bool IsSet(string path)
    {
        var root = GetRoot();
        if (root == null) return false;

        if (root.Options()._CopyDefaults) return Contains(path);

        return Get(path, null) != null;
    }

    public string GetCurrentPath()
    {
        return _fullPath;
    }

    public string GetName()
    {
        return _path;
    }

    public IConfiguration GetRoot()
    {
        return _root;
    }

    public IConfigurationSection GetParent()
    {
        return _parent;
    }

    public void AddDefault(string path, object value)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentException(@"Path cannot be null or empty", nameof(path));

        var root = GetRoot();
        if (root == null) throw new InvalidOperationException("Cannot add default without root");

        root.AddDefault(CreatePath(this, path), value);
    }

    public IConfigurationSection GetDefaultSection()
    {
        var root = GetRoot();
        var defaults = root?.GetDefaults();

        if (defaults != null)
            if (defaults.IsConfigurationSection(GetCurrentPath()))
                return defaults.GetConfigurationSection(GetCurrentPath());

        return null;
    }

    public void Set(string path, object value)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException(@"Path cannot be null or empty", nameof(path));

        var keys = SplitPath(path, GetRoot().Options()._PathSeparator);

        var current = this;
        for (var i = 0; i < keys.Count - 1; i++)
        {
            if (!current._map.TryGetValue(keys[i], out var child) || !(child is ConfigSection))
            {
                child = new ConfigSection(current, keys[i]);
                current._map[keys[i]] = child;
            }

            current = (ConfigSection)child;
        }

        var lastKey = keys[keys.Count - 1];

        if (current._map.TryGetValue(lastKey, out var existingValue) &&
            existingValue is ConfigSection existingSection)
        {
            if (value is IDictionary<string, object> newMap)
                foreach (var entry in newMap)
                    existingSection.Set(entry.Key, entry.Value);
            else
                current._map[lastKey] = value;
        }
        else
        {
            current._map[lastKey] = value;
        }
    }

    public object Get(string path)
    {
        return Get(path, GetDefault(path));
    }

    public object Get(string path, object def)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException(@"Path cannot be null or empty", nameof(path));

        var keys = SplitPath(path, GetRoot().Options()._PathSeparator);

        var current = this;
        for (var i = 0; i < keys.Count; i++)
        {
            if (!current._map.TryGetValue(keys[i], out var child)) return def;

            if (i == keys.Count - 1) return child;

            if (!(child is ConfigSection value)) return def;

            current = value;
        }

        return def;
    }

    public IConfigurationSection CreateSection(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException(@"Cannot create section at empty path", nameof(path));

        var root = GetRoot();
        if (root == null) throw new InvalidOperationException("Cannot create section without a root");

        var separator = root.Options()._PathSeparator;
        int i1 = -1, i2;
        IConfigurationSection section = this;
        while ((i1 = path.IndexOf(separator, i2 = i1 + 1)) != -1)
        {
            var node = path.Substring(i2, i1 - i2);
            var subSection = section.GetConfigurationSection(node);
            section = subSection ?? section.CreateSection(node);
        }

        var key = path.Substring(i2);
        if (section == this)
        {
            IConfigurationSection result = new ConfigSection(this, key);
            _map[key] = result;
            return result;
        }

        return section.CreateSection(key);
    }

    public IConfigurationSection CreateSection(string path, Dictionary<string, object> map)
    {
        var section = CreateSection(path);

        foreach (var entry in map)
            if (entry.Value is Dictionary<string, object> subMap)
                section.CreateSection(entry.Key, subMap);
            else
                section.Set(entry.Key, entry.Value);

        return section;
    }

    public string GetString(string path)
    {
        var def = GetDefault(path);
        return GetString(path, def?.ToString());
    }

    public string GetString(string path, string def)
    {
        var val = Get(path, def);
        return val?.ToString() ?? def;
    }

    public bool IsString(string path)
    {
        var val = Get(path);
        return val is string;
    }

    public int GetInt(string path)
    {
        var def = GetDefault(path);
        return GetInt(path, def is int i ? i : 0);
    }

    public int GetInt(string path, int def)
    {
        var val = Get(path, def);
        if (val is int intValue)
            return intValue;
        else if (val is string str)
            if (int.TryParse(str, out intValue))
                return intValue;

        return def;
    }

    public bool IsInt(string path)
    {
        var val = Get(path);
        return val is int;
    }

    public bool GetBoolean(string path)
    {
        var def = GetDefault(path);
        return GetBoolean(path, def is bool b && b);
    }

    public bool GetBoolean(string path, bool def)
    {
        var val = Get(path, def);
        return val as bool? ?? def;
    }

    public bool IsBoolean(string path)
    {
        var val = Get(path);
        return val is bool;
    }

    public double GetDouble(string path)
    {
        var def = GetDefault(path);
        return GetDouble(path, def is double d ? d : 0.0);
    }

    public double GetDouble(string path, double def)
    {
        var val = Get(path, def);
        if (val is double value)
            return value;
        else if (val is string str)
            if (double.TryParse(str, out var doubleValue))
                return doubleValue;

        return def;
    }

    public bool IsDouble(string path)
    {
        var val = Get(path);
        return val is double;
    }

    public long GetLong(string path)
    {
        var def = GetDefault(path);
        return GetLong(path, def is long l ? l : 0L);
    }

    public long GetLong(string path, long def)
    {
        var val = Get(path, def);
        if (val is long value)
            return value;
        else if (val is string str)
            if (long.TryParse(str, out var longValue))
                return longValue;

        return def;
    }

    public bool IsLong(string path)
    {
        var val = Get(path);
        return val is long;
    }

    public List<object> GetList(string path)
    {
        var def = GetDefault(path);
        return GetList(path, def as List<object>);
    }

    public List<object> GetList(string path, List<object> def)
    {
        var val = Get(path, def);
        return val is List<object> list ? list : def;
    }

    public List<T> GetList<T>(string path)
    {
        var def = GetDefault(path);
        return GetList(path, def as List<T>);
    }

    public List<T> GetList<T>(string path, List<T> def)
    {
        var val = Get(path, def);
        return val as List<T> ?? def;
    }

    public bool IsList(string path)
    {
        var val = Get(path);
        return val is List<object>;
    }

    public List<string> GetStringList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<string>(0);

        var result = new List<string>();

        foreach (var obj in list)
            if (obj is string || IsPrimitiveWrapper(obj))
                result.Add(obj.ToString());

        return result;
    }

    public List<int> GetIntegerList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<int>(0);

        var result = new List<int>();

        foreach (var obj in list)
            if (obj is int intValue)
                result.Add(intValue);
            else if (obj is string str)
                try
                {
                    result.Add(int.Parse(str));
                }
                catch
                {
                    // ignored
                }
            else if (obj is char ch)
                result.Add(ch);
            else if (obj is IConvertible convertible) result.Add(convertible.ToInt32(null));

        return result;
    }

    public List<bool> GetBooleanList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<bool>(0);

        var result = new List<bool>();

        foreach (var obj in list)
            if (obj is bool boolValue)
            {
                result.Add(boolValue);
            }
            else if (obj is string str)
            {
                if (bool.TrueString.Equals(str, StringComparison.OrdinalIgnoreCase))
                    result.Add(true);
                else if (bool.FalseString.Equals(str, StringComparison.OrdinalIgnoreCase)) result.Add(false);
            }

        return result;
    }

    public List<double> GetDoubleList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<double>(0);

        var result = new List<double>();

        foreach (var obj in list)
            switch (obj)
            {
                case double doubleValue:
                    result.Add(doubleValue);
                    break;
                case string str:
                    try
                    {
                        result.Add(double.Parse(str));
                    }
                    catch
                    {
                        // ignored
                    }

                    break;
                case char ch:
                    result.Add(ch);
                    break;
                case IConvertible convertible:
                    result.Add(convertible.ToDouble(null));
                    break;
            }

        return result;
    }

    public List<float> GetFloatList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<float>(0);

        var result = new List<float>();

        foreach (var obj in list)
            if (obj is float floatValue)
                result.Add(floatValue);
            else if (obj is string str)
                try
                {
                    result.Add(float.Parse(str));
                }
                catch
                {
                    // ignored
                }
            else if (obj is char ch)
                result.Add(ch);
            else if (obj is IConvertible convertible) result.Add(convertible.ToSingle(null));

        return result;
    }

    public List<long> GetLongList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<long>(0);

        var result = new List<long>();

        foreach (var obj in list)
            if (obj is long longValue)
                result.Add(longValue);
            else if (obj is string str)
                try
                {
                    result.Add(long.Parse(str));
                }
                catch
                {
                    // ignored
                }
            else if (obj is char ch)
                result.Add(ch);
            else if (obj is IConvertible convertible) result.Add(convertible.ToInt64(null));

        return result;
    }

    public List<byte> GetByteList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<byte>(0);

        var result = new List<byte>();

        foreach (var obj in list)
            if (obj is byte byteValue)
                result.Add(byteValue);
            else if (obj is string str)
                try
                {
                    result.Add(byte.Parse(str));
                }
                catch
                {
                    // ignored
                }
            else if (obj is char ch)
                result.Add((byte)ch);
            else if (obj is IConvertible convertible) result.Add(convertible.ToByte(null));

        return result;
    }

    public List<char> GetCharacterList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<char>(0);

        var result = new List<char>();

        foreach (var obj in list)
            if (obj is char charValue)
                result.Add(charValue);
            else if (obj is string str && str.Length == 1)
                result.Add(str[0]);
            else if (obj is IConvertible convertible) result.Add((char)convertible.ToInt32(null));

        return result;
    }

    public List<short> GetShortList(string path)
    {
        var list = GetList(path);

        if (list == null) return new List<short>(0);

        var result = new List<short>();

        foreach (var obj in list)
            if (obj is short shortValue)
                result.Add(shortValue);
            else if (obj is string str)
                try
                {
                    result.Add(short.Parse(str));
                }
                catch
                {
                    // ignored
                }
            else if (obj is char ch)
                result.Add((short)ch);
            else if (obj is IConvertible convertible) result.Add(convertible.ToInt16(null));

        return result;
    }

    public List<Dictionary<object, object>> GetMapList(string path)
    {
        var list = GetList(path);
        var result = new List<Dictionary<object, object>>();

        if (list == null) return result;

        foreach (var obj in list)
            if (obj is Dictionary<object, object> mapValue)
                result.Add(mapValue);

        return result;
    }

    public IConfigurationSection GetConfigurationSection(string key)
    {
        var keys = SplitPath(key, GetRoot().Options()._PathSeparator);

        var current = this;
        foreach (var t in keys)
        {
            if (!current._map.TryGetValue(t, out var child) || !(child is ConfigSection))
            {
                child = new ConfigSection(this, t);
                current._map[t] = child;
            }

            current = (ConfigSection)child;
        }

        return current;
    }

    public bool IsConfigurationSection(string path)
    {
        var val = Get(path);
        return val is IConfigurationSection;
    }

    public static bool IsPrimitiveWrapper(object input)
    {
        return input is int || input is bool ||
               input is char || input is byte ||
               input is short || input is double ||
               input is long || input is float;
    }

    public object GetDefault(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentException(@"Path cannot be null or empty", nameof(path));

        var root = GetRoot();
        var defaults = root?.GetDefaults();
        return defaults?.Get(CreatePath(this, path));
    }

    private void MapChildrenKeys(ISet<object> output, IConfigurationSection section, bool deep)
    {
        if (section is ConfigSection sec)
        {
            foreach (var entry in sec._map)
            {
                output.Add(CreatePath(section, entry.Key.ToString(), this));

                if (!deep || !(entry.Value is IConfigurationSection subsection)) continue;
                MapChildrenKeys(output, subsection, true);
            }
        }
        else
        {
            ISet<object> keys = section.GetKeys(deep);

            foreach (var key in keys) output.Add(CreatePath(section, key.ToString(), this));
        }
    }

    private void MapChildrenValues(IDictionary<object, object> output, IConfigurationSection section, bool deep)
    {
        if (section is ConfigSection sec)
        {
            foreach (var entry in sec._map)
            {
                output[CreatePath(section, entry.Key.ToString(), this)] = entry.Value;

                if (entry.Value is IConfigurationSection value && deep) MapChildrenValues(output, value, true);
            }
        }
        else
        {
            IDictionary<object, object> values = section.GetValues(deep);

            foreach (var entry in values) output[CreatePath(section, entry.Key.ToString(), this)] = entry.Value;
        }
    }

    private static string CreatePath(IConfigurationSection section, string key)
    {
        return CreatePath(section, key, section?.GetRoot());
    }

    private static string CreatePath(IConfigurationSection section, string key, IConfigurationSection relativeTo)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section), @"Cannot create path without a section");

        var root = section.GetRoot();
        if (root == null) throw new InvalidOperationException("Cannot create path without a root");

        var separator = root.Options()._PathSeparator;

        var builder = new StringBuilder();
        for (var parent = section;
             parent != null && parent != relativeTo;
             parent = parent.GetParent())
        {
            if (builder.Length > 0) builder.Insert(0, separator);

            builder.Insert(0, parent.GetName());
        }

        if (!string.IsNullOrEmpty(key))
        {
            if (builder.Length > 0) builder.Append(separator);

            builder.Append(key);
        }

        return builder.ToString();
    }

    private static List<string> SplitPath(string path, char separator)
    {
        return path.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public override string ToString()
    {
        var root = GetRoot();
        return new StringBuilder()
            .Append(GetType().Name)
            .Append("[path='")
            .Append(GetCurrentPath())
            .Append("', root='")
            .Append(root?.GetType().Name)
            .Append("']")
            .ToString();
    }
}