using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AkariLevelEditor.Pages;

public partial class HomePage : INotifyPropertyChanged
{
    public HomePage()
    {
        InitializeComponent();
        DataContext = this;
        UpdatePreview();
    }

    private const string DefaultLanguage = "zh_CN";
    private const int DefaultConfigVersion = 6;
    private const bool DefaultCheckUpdate = true;
    private const bool DefaultOpNotify = true;
    private const bool DefaultSendMetrics = true;
    private const bool DefaultDebug = false;
    private const bool DefaultUseUuid = false;
    private const string DefaultDatabaseMethod = "JSON";
    private const string DefaultJsonFilePath = "database.json";
    private const string DefaultSqlHost = "localhost";
    private const int DefaultSqlPort = 3306;
    private const string DefaultSqlUser = "root";
    private const string DefaultSqlPassword = "password";
    private const string DefaultSqlDatabase = "minecraft";
    private const string DefaultSqlTable = "akarilevel";
    private const bool DefaultVanillaTrace = false;
    private const string DefaultDefaultTraceGroup = "";
    private const bool DefaultAutoResetTrace = true;
    private const bool DefaultTeamEnable = false;
    private const string DefaultTeamPlugin = "DungeonPlus";
    private const string DefaultTeamSource = "MYTHICMOBS_DROP_EXP";
    private const string DefaultTeamFormula = "%exp% * %size%";
    private const int DefaultTeamLeaderWeight = 1;
    private const int DefaultTeamMemberWeight = 1;
    private const bool DefaultAttributeEnable = false;
    private const string DefaultAttributePlugin = "AttributePlus";
    private const string DefaultAttributeName = "经验加成";
    private const string DefaultAttributeFormula = "%exp% * ( 1 + %attribute% / 100 )";
    private const string DefaultAttributeSource = "MYTHICMOBS_DROP_EXP\nVANILLA_EXP_CHANGE";
    private const string DefaultPlaceholderPrefix = "akarilevel";
    private const string DefaultLevelBarEmpty = "□";
    private const string DefaultLevelBarFull = "■";
    private const int DefaultLevelBarLength = 10;
    private const string DefaultExpBarEmpty = "□";
    private const string DefaultExpBarFull = "■";
    private const int DefaultExpBarLength = 10;

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
        // 当属性值改变时，同时触发对应的 IsDefault 属性改变
        if (propertyName != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"Is{propertyName}Default"));
    }

    // 为每个属性添加 IsDefault 属性
    public bool IsLanguageDefault => SelectedLanguage == DefaultLanguage;
    public bool IsConfigVersionDefault => ConfigVersion == DefaultConfigVersion;
    public bool IsCheckUpdateDefault => CheckUpdate == DefaultCheckUpdate;
    public bool IsOpNotifyDefault => OpNotify == DefaultOpNotify;
    public bool IsSendMetricsDefault => SendMetrics == DefaultSendMetrics;
    public bool IsDebugDefault => Debug == DefaultDebug;
    public bool IsUseUuidDefault => UseUuid == DefaultUseUuid;
    public bool IsDatabaseMethodDefault => SelectedDatabaseMethod == DefaultDatabaseMethod;
    public bool IsJsonFilePathDefault => JsonFilePath == DefaultJsonFilePath;
    public bool IsSqlHostDefault => SqlHost == DefaultSqlHost;
    public bool IsSqlPortDefault => SqlPort == DefaultSqlPort;
    public bool IsSqlUserDefault => SqlUser == DefaultSqlUser;
    public bool IsSqlPasswordDefault => SqlPassword == DefaultSqlPassword;
    public bool IsSqlDatabaseDefault => SqlDatabase == DefaultSqlDatabase;
    public bool IsSqlTableDefault => SqlTable == DefaultSqlTable;
    public bool IsVanillaTraceDefault => VanillaTrace == DefaultVanillaTrace;
    public bool IsDefaultTraceGroupDefault => DefaultTraceGroup == DefaultDefaultTraceGroup;
    public bool IsAutoResetTraceDefault => AutoResetTrace == DefaultAutoResetTrace;
    public bool IsTeamEnableDefault => TeamEnable == DefaultTeamEnable;
    public bool IsTeamPluginDefault => SelectedTeamPlugin == DefaultTeamPlugin;
    public bool IsTeamSourceDefault => TeamSource == DefaultTeamSource;
    public bool IsTeamFormulaDefault => TeamFormula == DefaultTeamFormula;
    public bool IsTeamLeaderWeightDefault => TeamLeaderWeight == DefaultTeamLeaderWeight;
    public bool IsTeamMemberWeightDefault => TeamMemberWeight == DefaultTeamMemberWeight;
    public bool IsAttributeEnableDefault => AttributeEnable == DefaultAttributeEnable;
    public bool IsAttributePluginDefault => SelectedAttributePlugin == DefaultAttributePlugin;
    public bool IsAttributeNameDefault => AttributeName == DefaultAttributeName;
    public bool IsAttributeFormulaDefault => AttributeFormula == DefaultAttributeFormula;
    public bool IsAttributeSourceDefault => AttributeSource == DefaultAttributeSource;
    public bool IsPlaceholderPrefixDefault => PlaceholderPrefix == DefaultPlaceholderPrefix;
    public bool IsLevelBarEmptyDefault => LevelBarEmpty == DefaultLevelBarEmpty;
    public bool IsLevelBarFullDefault => LevelBarFull == DefaultLevelBarFull;
    public bool IsLevelBarLengthDefault => LevelBarLength == DefaultLevelBarLength;
    public bool IsExpBarEmptyDefault => ExpBarEmpty == DefaultExpBarEmpty;
    public bool IsExpBarFullDefault => ExpBarFull == DefaultExpBarFull;
    public bool IsExpBarLengthDefault => ExpBarLength == DefaultExpBarLength;

    public ICommand ResetLanguage => new RelayCommand(() => SelectedLanguage = DefaultLanguage);
    public ICommand ResetConfigVersion => new RelayCommand(() => ConfigVersion = DefaultConfigVersion);
    public ICommand ResetCheckUpdate => new RelayCommand(() => CheckUpdate = DefaultCheckUpdate);
    public ICommand ResetOpNotify => new RelayCommand(() => OpNotify = DefaultOpNotify);
    public ICommand ResetSendMetrics => new RelayCommand(() => SendMetrics = DefaultSendMetrics);
    public ICommand ResetDebug => new RelayCommand(() => Debug = DefaultDebug);
    public ICommand ResetUseUuid => new RelayCommand(() => UseUuid = DefaultUseUuid);
    public ICommand ResetDatabaseMethod => new RelayCommand(() => SelectedDatabaseMethod = DefaultDatabaseMethod);
    public ICommand ResetJsonFilePath => new RelayCommand(() => JsonFilePath = DefaultJsonFilePath);
    public ICommand ResetSqlHost => new RelayCommand(() => SqlHost = DefaultSqlHost);
    public ICommand ResetSqlPort => new RelayCommand(() => SqlPort = DefaultSqlPort);
    public ICommand ResetSqlUser => new RelayCommand(() => SqlUser = DefaultSqlUser);
    public ICommand ResetSqlPassword => new RelayCommand(() => SqlPassword = DefaultSqlPassword);
    public ICommand ResetSqlDatabase => new RelayCommand(() => SqlDatabase = DefaultSqlDatabase);
    public ICommand ResetSqlTable => new RelayCommand(() => SqlTable = DefaultSqlTable);
    public ICommand ResetVanillaTrace => new RelayCommand(() => VanillaTrace = DefaultVanillaTrace);
    public ICommand ResetDefaultTraceGroup => new RelayCommand(() => DefaultTraceGroup = DefaultDefaultTraceGroup);
    public ICommand ResetAutoResetTrace => new RelayCommand(() => AutoResetTrace = DefaultAutoResetTrace);
    public ICommand ResetTeamEnable => new RelayCommand(() => TeamEnable = DefaultTeamEnable);
    public ICommand ResetTeamPlugin => new RelayCommand(() => SelectedTeamPlugin = DefaultTeamPlugin);
    public ICommand ResetTeamSource => new RelayCommand(() => TeamSource = DefaultTeamSource);
    public ICommand ResetTeamFormula => new RelayCommand(() => TeamFormula = DefaultTeamFormula);
    public ICommand ResetTeamLeaderWeight => new RelayCommand(() => TeamLeaderWeight = DefaultTeamLeaderWeight);
    public ICommand ResetTeamMemberWeight => new RelayCommand(() => TeamMemberWeight = DefaultTeamMemberWeight);
    public ICommand ResetAttributeEnable => new RelayCommand(() => AttributeEnable = DefaultAttributeEnable);
    public ICommand ResetAttributePlugin => new RelayCommand(() => SelectedAttributePlugin = DefaultAttributePlugin);
    public ICommand ResetAttributeName => new RelayCommand(() => AttributeName = DefaultAttributeName);
    public ICommand ResetAttributeFormula => new RelayCommand(() => AttributeFormula = DefaultAttributeFormula);
    public ICommand ResetAttributeSource => new RelayCommand(() => AttributeSource = DefaultAttributeSource);
    public ICommand ResetPlaceholderPrefix => new RelayCommand(() => PlaceholderPrefix = DefaultPlaceholderPrefix);
    public ICommand ResetLevelBarEmpty => new RelayCommand(() => LevelBarEmpty = DefaultLevelBarEmpty);
    public ICommand ResetLevelBarFull => new RelayCommand(() => LevelBarFull = DefaultLevelBarFull);
    public ICommand ResetLevelBarLength => new RelayCommand(() => LevelBarLength = DefaultLevelBarLength);
    public ICommand ResetExpBarEmpty => new RelayCommand(() => ExpBarEmpty = DefaultExpBarEmpty);
    public ICommand ResetExpBarFull => new RelayCommand(() => ExpBarFull = DefaultExpBarFull);
    public ICommand ResetExpBarLength => new RelayCommand(() => ExpBarLength = DefaultExpBarLength);

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
}