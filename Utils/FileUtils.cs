using System.IO;
using System.Reflection;

namespace AkariLevelEditor.Utils;

public class FileUtils
{
    /** 根目录 **/
    public static string RootFolder => AppDomain.CurrentDomain.BaseDirectory;

    /** 获取文件夹内所有文件 **/
    public static List<FileInfo> GetFiles(string dir, bool deep = false)
    {
        return GetDirectoryInfo(dir)
            .EnumerateFiles("*", deep ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            .ToList();
    }

    /** 获取文件夹内所有文件名称 **/
    public static List<string> GetFileNames(string dir, bool deep = false)
    {
        return GetFiles(dir, deep)
            .Select(f => f.Name)
            .ToList();
    }

    /** 获取文件夹内所有文件名称（不带扩展名） **/
    public static List<string> GetFileNamesWithoutExtensions(string dir, bool deep = false)
    {
        return GetFiles(dir, deep)
            .Select(f => Path.GetFileNameWithoutExtension(f.Name))
            .ToList();
    }

    /** 获取文件夹内文件 (不存在时返回 null) **/
    public static FileInfo? GetFileOrNull(string filePath)
    {
        return GetFileInfo(filePath) is { Exists: true } file ? file : null;
    }

    /** 获取文件夹内文件 (不存在时创建文件) **/
    public static FileInfo GetFileOrCreate(string filePath)
    {
        var file = GetFileInfo(filePath);
        if (file.Exists) return file;

        file.Directory?.Create();
        using (file.Create())
        {
        }

        file.Refresh();
        return file;
    }

    /** 读文本文件 **/
    public static string ReadText(FileInfo file)
    {
        return File.ReadAllText(file.FullName);
    }

    /** 写文本文件 **/
    public static void WriteText(FileInfo file, string text)
    {
        File.WriteAllText(file.FullName, text);
    }

    /** 释放资源文件 **/
    public static void SaveResource(string resourceName, string outPath = "")
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fullResourceName = $"{assembly.GetName().Name}.Resources.{resourceName}";
        using var stream = assembly.GetManifestResourceStream(fullResourceName);
        if (stream is null) return;

        var basePath = Path.Combine(RootFolder, outPath);
        var filePath = string.IsNullOrEmpty(Path.GetExtension(basePath))
            ? Path.Combine(basePath, resourceName)
            : basePath;

        if (File.Exists(filePath)) return;

        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        using var output = File.Create(filePath);
        stream.CopyTo(output);
    }

    private static DirectoryInfo GetDirectoryInfo(string dir)
    {
        return new DirectoryInfo(Path.Combine(RootFolder, dir));
    }

    private static FileInfo GetFileInfo(string path)
    {
        return new FileInfo(Path.Combine(RootFolder, path));
    }
}