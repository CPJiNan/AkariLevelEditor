using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using Microsoft.Win32;
using System.IO;
using AkariLevelEditor.Configuration.File;

namespace AkariLevelEditor.Pages;

public partial class OptionsPage : INotifyPropertyChanged
{
    public OptionsPage()
    {
        InitializeComponent();

        DataContext = this;
        UpdatePreview();
    }

    private string _defaultLanguage = "zh_CN";
    private int _defaultConfigVersion = 6;
    private bool _defaultCheckUpdate = true;
    private bool _defaultOpNotify = true;
    private bool _defaultSendMetrics = true;
    private bool _defaultDebug;
    private bool _defaultUseUuid;
    private string _defaultDatabaseMethod = "JSON";
    private string _defaultJsonFilePath = "database.json";
    private string _defaultSqlHost = "localhost";
    private int _defaultSqlPort = 3306;
    private string _defaultSqlUser = "root";
    private string _defaultSqlPassword = "password";
    private string _defaultSqlDatabase = "minecraft";
    private string _defaultSqlTable = "akarilevel";
    private bool _defaultVanillaTrace;
    private string _defaultDefaultTraceGroup = "";
    private bool _defaultAutoResetTrace = true;
    private bool _defaultTeamEnable;
    private string _defaultTeamPlugin = "DungeonPlus";
    private string _defaultTeamSource = "MYTHICMOBS_DROP_EXP";
    private string _defaultTeamFormula = "%exp% * %size%";
    private int _defaultTeamLeaderWeight = 1;
    private int _defaultTeamMemberWeight = 1;
    private bool _defaultAttributeEnable;
    private string _defaultAttributePlugin = "AttributePlus";
    private string _defaultAttributeName = "经验加成";
    private string _defaultAttributeFormula = "%exp% * ( 1 + %attribute% / 100 )";
    private string _defaultAttributeSource = "MYTHICMOBS_DROP_EXP\nVANILLA_EXP_CHANGE";
    private string _defaultPlaceholderPrefix = "akarilevel";
    private string _defaultLevelBarEmpty = "□";
    private string _defaultLevelBarFull = "■";
    private int _defaultLevelBarLength = 10;
    private string _defaultExpBarEmpty = "□";
    private string _defaultExpBarFull = "■";
    private int _defaultExpBarLength = 10;

    private string _selectedLanguage = "zh_CN";
    private int _configVersion = 6;
    private bool _checkUpdate = true;
    private bool _opNotify = true;
    private bool _sendMetrics = true;
    private bool _debug;
    private bool _useUuid;
    private string _selectedDatabaseMethod = "JSON";
    private string _jsonFilePath = "database.json";
    private string _sqlHost = "localhost";
    private int _sqlPort = 3306;
    private string _sqlUser = "root";
    private string _sqlPassword = "password";
    private string _sqlDatabase = "minecraft";
    private string _sqlTable = "akarilevel";
    private bool _vanillaTrace;
    private string _defaultTraceGroup = "";
    private bool _autoResetTrace = true;
    private bool _teamEnable;
    private string _selectedTeamPlugin = "DungeonPlus";
    private string _teamSource = "MYTHICMOBS_DROP_EXP";
    private string _teamFormula = "%exp% * %size%";
    private int _teamLeaderWeight = 1;
    private int _teamMemberWeight = 1;
    private bool _attributeEnable;
    private string _selectedAttributePlugin = "AttributePlus";
    private string _attributeName = "经验加成";
    private string _attributeFormula = "%exp% * ( 1 + %attribute% / 100 )";
    private string _attributeSource = "MYTHICMOBS_DROP_EXP\nVANILLA_EXP_CHANGE";
    private string _placeholderPrefix = "akarilevel";
    private string _levelBarEmpty = "□";
    private string _levelBarFull = "■";
    private int _levelBarLength = 10;
    private string _expBarEmpty = "□";
    private string _expBarFull = "■";
    private int _expBarLength = 10;

    private FileInfo? _importedFile;

    public FileInfo? ImportedFile
    {
        get => _importedFile;
        set
        {
            _importedFile = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> Languages { get; } = ["zh_CN", "zh_TW", "en_US", "es_ES"];
    public ObservableCollection<string> DatabaseMethods { get; } = ["JSON", "SQL"];
    public ObservableCollection<string> TeamPlugins { get; } = ["DungeonPlus"];

    public ObservableCollection<string> AttributePlugins { get; } =
        ["AttributePlus", "SX-Attribute", "OriginAttribute", "AttributeSystem"];

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            _selectedLanguage = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public int ConfigVersion
    {
        get => _configVersion;
        set
        {
            _configVersion = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool CheckUpdate
    {
        get => _checkUpdate;
        set
        {
            _checkUpdate = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool OpNotify
    {
        get => _opNotify;
        set
        {
            _opNotify = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool SendMetrics
    {
        get => _sendMetrics;
        set
        {
            _sendMetrics = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool Debug
    {
        get => _debug;
        set
        {
            _debug = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool UseUuid
    {
        get => _useUuid;
        set
        {
            _useUuid = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SelectedDatabaseMethod
    {
        get => _selectedDatabaseMethod;
        set
        {
            _selectedDatabaseMethod = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string JsonFilePath
    {
        get => _jsonFilePath;
        set
        {
            _jsonFilePath = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SqlHost
    {
        get => _sqlHost;
        set
        {
            _sqlHost = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public int SqlPort
    {
        get => _sqlPort;
        set
        {
            _sqlPort = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SqlUser
    {
        get => _sqlUser;
        set
        {
            _sqlUser = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SqlPassword
    {
        get => _sqlPassword;
        set
        {
            _sqlPassword = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SqlDatabase
    {
        get => _sqlDatabase;
        set
        {
            _sqlDatabase = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SqlTable
    {
        get => _sqlTable;
        set
        {
            _sqlTable = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool VanillaTrace
    {
        get => _vanillaTrace;
        set
        {
            _vanillaTrace = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string DefaultTraceGroup
    {
        get => _defaultTraceGroup;
        set
        {
            _defaultTraceGroup = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool AutoResetTrace
    {
        get => _autoResetTrace;
        set
        {
            _autoResetTrace = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool TeamEnable
    {
        get => _teamEnable;
        set
        {
            _teamEnable = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SelectedTeamPlugin
    {
        get => _selectedTeamPlugin;
        set
        {
            _selectedTeamPlugin = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string TeamSource
    {
        get => _teamSource;
        set
        {
            _teamSource = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string TeamFormula
    {
        get => _teamFormula;
        set
        {
            _teamFormula = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public int TeamLeaderWeight
    {
        get => _teamLeaderWeight;
        set
        {
            _teamLeaderWeight = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public int TeamMemberWeight
    {
        get => _teamMemberWeight;
        set
        {
            _teamMemberWeight = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public bool AttributeEnable
    {
        get => _attributeEnable;
        set
        {
            _attributeEnable = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string SelectedAttributePlugin
    {
        get => _selectedAttributePlugin;
        set
        {
            _selectedAttributePlugin = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string AttributeName
    {
        get => _attributeName;
        set
        {
            _attributeName = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string AttributeFormula
    {
        get => _attributeFormula;
        set
        {
            _attributeFormula = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string AttributeSource
    {
        get => _attributeSource;
        set
        {
            _attributeSource = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string PlaceholderPrefix
    {
        get => _placeholderPrefix;
        set
        {
            _placeholderPrefix = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string LevelBarEmpty
    {
        get => _levelBarEmpty;
        set
        {
            _levelBarEmpty = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string LevelBarFull
    {
        get => _levelBarFull;
        set
        {
            _levelBarFull = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public int LevelBarLength
    {
        get => _levelBarLength;
        set
        {
            _levelBarLength = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string ExpBarEmpty
    {
        get => _expBarEmpty;
        set
        {
            _expBarEmpty = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public string ExpBarFull
    {
        get => _expBarFull;
        set
        {
            _expBarFull = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    public int ExpBarLength
    {
        get => _expBarLength;
        set
        {
            _expBarLength = value;
            OnPropertyChanged();
            UpdatePreview();
        }
    }

    private string? _previewText;

    public string? PreviewText
    {
        get => _previewText;
        set
        {
            _previewText = value;
            OnPropertyChanged();
        }
    }

    private void UpdatePreview()
    {
        PreviewText = $"""
                       #     _    _              _ _                   _  #
                       #    / \  | | ____ _ _ __(_) |    _____   _____| | #
                       #   / _ \ | |/ / _` | '__| | |   / _ \ \ / / _ \ | #
                       #  / ___ \|   < (_| | |  | | |__|  __/\ V /  __/ | #
                       # /_/   \_\_|\_\__,_|_|  |_|_____\___| \_/ \___|_| #

                       # 插件文档: https://cpjinan.github.io/Wiki/

                       ##################################
                       #       AkariLevel  Config       #
                       ##################################

                       ###########
                       # Options #
                       ###########

                       # 全局设置
                       Options:
                         # 语言
                         Language: "{SelectedLanguage}"
                         # 配置文件版本
                         Config-Version: {ConfigVersion}
                         # 检查版本更新
                         Check-Update: {CheckUpdate.ToString().ToLower()}
                         # OP 版本更新通知
                         OP-Notify: {OpNotify.ToString().ToLower()}
                         # 启用 bStats 统计
                         Send-Metrics: {SendMetrics.ToString().ToLower()}
                         # 启用调试模式
                         Debug: {Debug.ToString().ToLower()}

                       ############
                       # Database #
                       ############

                       # 数据存储设置
                       Database:
                         # 使用玩家 UUID 进行存储
                         UUID: {UseUuid.ToString().ToLower()}
                         # 存储方式 (JSON, SQL)
                         Method: {SelectedDatabaseMethod}
                         JSON:
                           file: {JsonFilePath}
                         SQL:
                           host: {SqlHost}
                           port: {SqlPort}
                           user: {SqlUser}
                           password: {SqlPassword}
                           database: {SqlDatabase}
                           table: {SqlTable}

                       #########
                       # Trace #
                       #########

                       # 等级组追踪设置
                       Trace:
                         # 是否保留原版等级系统
                         # 该功能开启后将不会取消原版经验变化事件，但需注意关闭等级组的追踪功能
                         Vanilla: {VanillaTrace.ToString().ToLower()}
                         # 失去追踪焦点时返回的默认等级组 (此项留空则不会进行追踪)
                         Default: "{DefaultTraceGroup}"
                         # 是否在玩家每次进入服务器时重置追踪的等级组
                         Auto-Reset: {AutoResetTrace.ToString().ToLower()}

                       ########
                       # Team #
                       ########

                       # 队伍经验共享设置
                       Team:
                         # 是否启用
                         Enable: {TeamEnable.ToString().ToLower()}
                         # 组队插件 (DungeonPlus)
                         Plugin: {SelectedTeamPlugin}
                         # 参与共享的经验来源
                         Source:
                       {string.Join("\n", TeamSource.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries).Select(s => $"    - \"{s}\""))}
                         # 待分配的经验池总额 (JavaScript 支持)
                         # %exp% -> 分配前的经验数额
                         # %size% -> 队伍人数
                         Total: "{TeamFormula}"
                         # 队长与队员各自的分配权重，实际所获经验 = 经验池总额 * ( 个人权重 / 队伍权重总和 )
                         Weight:
                           Leader: {TeamLeaderWeight}
                           Member: {TeamMemberWeight}

                       #############
                       # Attribute #
                       #############

                       # 属性插件经验加成
                       Attribute:
                         # 是否启用
                         Enable: {AttributeEnable.ToString().ToLower()}
                         # 属性插件 (AttributePlus, SX-Attribute, OriginAttribute, AttributeSystem)
                         Plugin: "{SelectedAttributePlugin}"
                         # 属性名称 (如属性插件为 OriginAttribute 则无需填写此项)
                         # AttributePlus - "经验加成"
                         # SX-Attribute - "ExpAddition"
                         # AttributeSystem - "ExpAddition"
                         Name: "{AttributeName}"
                         # 经验计算公式 (JavaScript支持)
                         # %exp% -> 获得的经验值数量
                         # %attribute% -> 额外加成经验数量
                         Formula: "{AttributeFormula}"
                         # 所监听的 PlayerExpChangeEvent 来源
                         Source:
                       {string.Join("\n", AttributeSource.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries).Select(s => $"    - \"{s}\""))}

                       ####################
                       #  PlaceholderAPI  #
                       ####################

                       # 变量设置
                       PlaceholderAPI:
                         # 变量前缀
                         Identifier: "{PlaceholderPrefix}"
                         # 经验条变量设置
                         Progress-Bar:
                           # 当前等级 / 最高等级
                           Level:
                             Empty: "{LevelBarEmpty}"
                             Full: "{LevelBarFull}"
                             Length: {LevelBarLength}
                           # 当前经验 / 升至下一级所需经验
                           Exp:
                             Empty: "{ExpBarEmpty}"
                             Full: "{ExpBarFull}"
                             Length: {ExpBarLength}
                       """;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        if (propertyName == null) return;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"Is{propertyName}Default"));
        MainWindow.IsOptionsPageModified = IsModified;
    }

    public bool IsLanguageDefault => SelectedLanguage == _defaultLanguage;
    public bool IsConfigVersionDefault => ConfigVersion == _defaultConfigVersion;
    public bool IsCheckUpdateDefault => CheckUpdate == _defaultCheckUpdate;
    public bool IsOpNotifyDefault => OpNotify == _defaultOpNotify;
    public bool IsSendMetricsDefault => SendMetrics == _defaultSendMetrics;
    public bool IsDebugDefault => Debug == _defaultDebug;
    public bool IsUseUuidDefault => UseUuid == _defaultUseUuid;
    public bool IsDatabaseMethodDefault => SelectedDatabaseMethod == _defaultDatabaseMethod;
    public bool IsJsonFilePathDefault => JsonFilePath == _defaultJsonFilePath;
    public bool IsSqlHostDefault => SqlHost == _defaultSqlHost;
    public bool IsSqlPortDefault => SqlPort == _defaultSqlPort;
    public bool IsSqlUserDefault => SqlUser == _defaultSqlUser;
    public bool IsSqlPasswordDefault => SqlPassword == _defaultSqlPassword;
    public bool IsSqlDatabaseDefault => SqlDatabase == _defaultSqlDatabase;
    public bool IsSqlTableDefault => SqlTable == _defaultSqlTable;
    public bool IsVanillaTraceDefault => VanillaTrace == _defaultVanillaTrace;
    public bool IsDefaultTraceGroupDefault => DefaultTraceGroup == _defaultDefaultTraceGroup;
    public bool IsAutoResetTraceDefault => AutoResetTrace == _defaultAutoResetTrace;
    public bool IsTeamEnableDefault => TeamEnable == _defaultTeamEnable;
    public bool IsTeamPluginDefault => SelectedTeamPlugin == _defaultTeamPlugin;
    public bool IsTeamSourceDefault => TeamSource == _defaultTeamSource;
    public bool IsTeamFormulaDefault => TeamFormula == _defaultTeamFormula;
    public bool IsTeamLeaderWeightDefault => TeamLeaderWeight == _defaultTeamLeaderWeight;
    public bool IsTeamMemberWeightDefault => TeamMemberWeight == _defaultTeamMemberWeight;
    public bool IsAttributeEnableDefault => AttributeEnable == _defaultAttributeEnable;
    public bool IsAttributePluginDefault => SelectedAttributePlugin == _defaultAttributePlugin;
    public bool IsAttributeNameDefault => AttributeName == _defaultAttributeName;
    public bool IsAttributeFormulaDefault => AttributeFormula == _defaultAttributeFormula;
    public bool IsAttributeSourceDefault => AttributeSource == _defaultAttributeSource;
    public bool IsPlaceholderPrefixDefault => PlaceholderPrefix == _defaultPlaceholderPrefix;
    public bool IsLevelBarEmptyDefault => LevelBarEmpty == _defaultLevelBarEmpty;
    public bool IsLevelBarFullDefault => LevelBarFull == _defaultLevelBarFull;
    public bool IsLevelBarLengthDefault => LevelBarLength == _defaultLevelBarLength;
    public bool IsExpBarEmptyDefault => ExpBarEmpty == _defaultExpBarEmpty;
    public bool IsExpBarFullDefault => ExpBarFull == _defaultExpBarFull;
    public bool IsExpBarLengthDefault => ExpBarLength == _defaultExpBarLength;

    public ICommand ResetLanguage => new RelayCommand(() => SelectedLanguage = _defaultLanguage);
    public ICommand ResetConfigVersion => new RelayCommand(() => ConfigVersion = _defaultConfigVersion);
    public ICommand ResetCheckUpdate => new RelayCommand(() => CheckUpdate = _defaultCheckUpdate);
    public ICommand ResetOpNotify => new RelayCommand(() => OpNotify = _defaultOpNotify);
    public ICommand ResetSendMetrics => new RelayCommand(() => SendMetrics = _defaultSendMetrics);
    public ICommand ResetDebug => new RelayCommand(() => Debug = _defaultDebug);
    public ICommand ResetUseUuid => new RelayCommand(() => UseUuid = _defaultUseUuid);
    public ICommand ResetDatabaseMethod => new RelayCommand(() => SelectedDatabaseMethod = _defaultDatabaseMethod);
    public ICommand ResetJsonFilePath => new RelayCommand(() => JsonFilePath = _defaultJsonFilePath);
    public ICommand ResetSqlHost => new RelayCommand(() => SqlHost = _defaultSqlHost);
    public ICommand ResetSqlPort => new RelayCommand(() => SqlPort = _defaultSqlPort);
    public ICommand ResetSqlUser => new RelayCommand(() => SqlUser = _defaultSqlUser);
    public ICommand ResetSqlPassword => new RelayCommand(() => SqlPassword = _defaultSqlPassword);
    public ICommand ResetSqlDatabase => new RelayCommand(() => SqlDatabase = _defaultSqlDatabase);
    public ICommand ResetSqlTable => new RelayCommand(() => SqlTable = _defaultSqlTable);
    public ICommand ResetVanillaTrace => new RelayCommand(() => VanillaTrace = _defaultVanillaTrace);
    public ICommand ResetDefaultTraceGroup => new RelayCommand(() => DefaultTraceGroup = _defaultDefaultTraceGroup);
    public ICommand ResetAutoResetTrace => new RelayCommand(() => AutoResetTrace = _defaultAutoResetTrace);
    public ICommand ResetTeamEnable => new RelayCommand(() => TeamEnable = _defaultTeamEnable);
    public ICommand ResetTeamPlugin => new RelayCommand(() => SelectedTeamPlugin = _defaultTeamPlugin);
    public ICommand ResetTeamSource => new RelayCommand(() => TeamSource = _defaultTeamSource);
    public ICommand ResetTeamFormula => new RelayCommand(() => TeamFormula = _defaultTeamFormula);
    public ICommand ResetTeamLeaderWeight => new RelayCommand(() => TeamLeaderWeight = _defaultTeamLeaderWeight);
    public ICommand ResetTeamMemberWeight => new RelayCommand(() => TeamMemberWeight = _defaultTeamMemberWeight);
    public ICommand ResetAttributeEnable => new RelayCommand(() => AttributeEnable = _defaultAttributeEnable);
    public ICommand ResetAttributePlugin => new RelayCommand(() => SelectedAttributePlugin = _defaultAttributePlugin);
    public ICommand ResetAttributeName => new RelayCommand(() => AttributeName = _defaultAttributeName);
    public ICommand ResetAttributeFormula => new RelayCommand(() => AttributeFormula = _defaultAttributeFormula);
    public ICommand ResetAttributeSource => new RelayCommand(() => AttributeSource = _defaultAttributeSource);
    public ICommand ResetPlaceholderPrefix => new RelayCommand(() => PlaceholderPrefix = _defaultPlaceholderPrefix);
    public ICommand ResetLevelBarEmpty => new RelayCommand(() => LevelBarEmpty = _defaultLevelBarEmpty);
    public ICommand ResetLevelBarFull => new RelayCommand(() => LevelBarFull = _defaultLevelBarFull);
    public ICommand ResetLevelBarLength => new RelayCommand(() => LevelBarLength = _defaultLevelBarLength);
    public ICommand ResetExpBarEmpty => new RelayCommand(() => ExpBarEmpty = _defaultExpBarEmpty);
    public ICommand ResetExpBarFull => new RelayCommand(() => ExpBarFull = _defaultExpBarFull);
    public ICommand ResetExpBarLength => new RelayCommand(() => ExpBarLength = _defaultExpBarLength);

    public ICommand ExportCommand => new RelayCommand(() =>
    {
        try
        {
            Clipboard.SetText(PreviewText ?? string.Empty);

            var dialog = new MessageBox
            {
                Title = "导出成功",
                Content = "已将配置内容复制到剪贴板！",
                CloseButtonText = "确认"
            };

            dialog.ShowDialogAsync();
        }
        catch (Exception)
        {
            // ignored
        }
    });

    public ICommand ImportCommand => new RelayCommand(() =>
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = "Yaml 配置文件|*.yml;*.yaml|所有文件|*.*",
            Title = "导入配置文件"
        };

        if (fileDialog.ShowDialog() != true) return;

        try
        {
            ImportedFile = new FileInfo(fileDialog.FileName);
            ImportedFile.Refresh();

            var config = YamlConfiguration.LoadConfiguration(ImportedFile);

            _selectedLanguage = _defaultLanguage = config.GetString("Options.Language", "zh_CN")!;
            _configVersion = _defaultConfigVersion = (int)config.GetInt("Options.Config-Version", 6)!;
            _checkUpdate = _defaultCheckUpdate = (bool)config.GetBoolean("Options.Check-Update", true)!;
            _opNotify = _defaultOpNotify = (bool)config.GetBoolean("Options.OP-Notify", true)!;
            _sendMetrics = _defaultSendMetrics = (bool)config.GetBoolean("Options.Send-Metrics", true)!;
            _debug = _defaultDebug = (bool)config.GetBoolean("Options.Debug", false)!;
            _useUuid = _defaultUseUuid = (bool)config.GetBoolean("Database.UUID", false)!;
            _selectedDatabaseMethod = _defaultDatabaseMethod = config.GetString("Database.Method", "JSON")!;
            _jsonFilePath = _defaultJsonFilePath = config.GetString("Database.JSON.file", "database.json")!;
            _sqlHost = _defaultSqlHost = config.GetString("Database.SQL.host", "localhost")!;
            _sqlPort = _defaultSqlPort = (int)config.GetInt("Database.SQL.port", 3306)!;
            _sqlUser = _defaultSqlUser = config.GetString("Database.SQL.user", "root")!;
            _sqlPassword = _defaultSqlPassword = config.GetString("Database.SQL.password", "password")!;
            _sqlDatabase = _defaultSqlDatabase = config.GetString("Database.SQL.database", "minecraft")!;
            _sqlTable = _defaultSqlTable = config.GetString("Database.SQL.table", "akarilevel")!;
            _vanillaTrace = _defaultVanillaTrace = (bool)config.GetBoolean("Trace.Vanilla", false)!;
            _defaultTraceGroup = _defaultDefaultTraceGroup = config.GetString("Trace.Default", "")!;
            _autoResetTrace = _defaultAutoResetTrace = (bool)config.GetBoolean("Trace.Auto-Reset", true)!;
            _teamEnable = _defaultTeamEnable = (bool)config.GetBoolean("Team.Enable", false)!;
            _selectedTeamPlugin = _defaultTeamPlugin = config.GetString("Team.Plugin", "DungeonPlus")!;
            _teamSource = _defaultTeamSource = string.Join("\n", config.GetStringList("Team.Source"));
            _teamFormula = _defaultTeamFormula = config.GetString("Team.Total", "%exp% * %size%")!;
            _teamLeaderWeight = _defaultTeamLeaderWeight = (int)config.GetInt("Team.Weight.Leader", 1)!;
            _teamMemberWeight = _defaultTeamMemberWeight = (int)config.GetInt("Team.Weight.Member", 1)!;
            _attributeEnable = _defaultAttributeEnable = (bool)config.GetBoolean("Attribute.Enable", false)!;
            _selectedAttributePlugin = _defaultAttributePlugin = config.GetString("Attribute.Plugin", "AttributePlus")!;
            _attributeName = _defaultAttributeName = config.GetString("Attribute.Name", "经验加成")!;
            _attributeFormula = _defaultAttributeFormula =
                config.GetString("Attribute.Formula", "%exp% * ( 1 + %attribute% / 100 )")!;
            _attributeSource = _defaultAttributeSource = string.Join("\n", config.GetStringList("Attribute.Source"));
            _placeholderPrefix =
                _defaultPlaceholderPrefix = config.GetString("PlaceholderAPI.Identifier", "akarilevel")!;
            _levelBarEmpty = _defaultLevelBarEmpty = config.GetString("PlaceholderAPI.Progress-Bar.Level.Empty", "□")!;
            _levelBarFull = _defaultLevelBarFull = config.GetString("PlaceholderAPI.Progress-Bar.Level.Full", "■")!;
            _levelBarLength = _defaultLevelBarLength =
                (int)config.GetInt("PlaceholderAPI.Progress-Bar.Level.Length", 10)!;
            _expBarEmpty = _defaultExpBarEmpty = config.GetString("PlaceholderAPI.Progress-Bar.Exp.Empty", "□")!;
            _expBarFull = _defaultExpBarFull = config.GetString("PlaceholderAPI.Progress-Bar.Exp.Full", "■")!;
            _expBarLength = _defaultExpBarLength = (int)config.GetInt("PlaceholderAPI.Progress-Bar.Exp.Length", 10)!;

            UpdatePreview();
        }
        catch (Exception)
        {
            // ignored
        }
    });

    private class RelayCommand(Action execute) : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute();
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }
    }

    public bool IsModified => !IsLanguageDefault ||
                              !IsConfigVersionDefault ||
                              !IsCheckUpdateDefault ||
                              !IsOpNotifyDefault ||
                              !IsSendMetricsDefault ||
                              !IsDebugDefault ||
                              !IsUseUuidDefault ||
                              !IsDatabaseMethodDefault ||
                              !IsJsonFilePathDefault ||
                              !IsSqlHostDefault ||
                              !IsSqlPortDefault ||
                              !IsSqlUserDefault ||
                              !IsSqlPasswordDefault ||
                              !IsSqlDatabaseDefault ||
                              !IsSqlTableDefault ||
                              !IsVanillaTraceDefault ||
                              !IsDefaultTraceGroupDefault ||
                              !IsAutoResetTraceDefault ||
                              !IsTeamEnableDefault ||
                              !IsTeamPluginDefault ||
                              !IsTeamSourceDefault ||
                              !IsTeamFormulaDefault ||
                              !IsTeamLeaderWeightDefault ||
                              !IsTeamMemberWeightDefault ||
                              !IsAttributeEnableDefault ||
                              !IsAttributePluginDefault ||
                              !IsAttributeNameDefault ||
                              !IsAttributeFormulaDefault ||
                              !IsAttributeSourceDefault ||
                              !IsPlaceholderPrefixDefault ||
                              !IsLevelBarEmptyDefault ||
                              !IsLevelBarFullDefault ||
                              !IsLevelBarLengthDefault ||
                              !IsExpBarEmptyDefault ||
                              !IsExpBarFullDefault ||
                              !IsExpBarLengthDefault;
}