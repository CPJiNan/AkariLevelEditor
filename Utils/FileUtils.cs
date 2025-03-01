using System.IO;
using System.Reflection;

namespace AkariLevelEditor.Utils;

public class FileUtils
{
    public static readonly string RootFolder = AppDomain.CurrentDomain.BaseDirectory;

    /** 获取文件夹内所有文件 **/
    public static List<FileInfo> GetFiles(string dir, bool deep = false)
    {
        var result = new List<FileInfo>();
        ForEachFile(dir, deep, result.Add);
        return result;
    }

    /** 获取文件夹内所有文件名称 **/
    public static List<string> GetFileNames(string dir, bool deep = false)
    {
        var result = new List<string>();
        ForEachFile(dir, deep, file => result.Add(file.Name));
        return result;
    }

    /** 获取文件夹内所有文件名称（不带扩展名） **/
    public static List<string> GetFileNamesWithoutExtensions(string dir, bool deep = false)
    {
        var result = new List<string>();
        ForEachFile(dir, deep, file => result.Add(Path.GetFileNameWithoutExtension(file.Name)));
        return result;
    }

    /** 获取文件夹内文件 (不存在时返回 null) **/
    public static FileInfo? GetFileOrNull(string filePath)
    {
        var file = new FileInfo(Path.Combine(RootFolder, filePath));
        return file.Exists ? file : null;
    }

    /** 获取文件夹内文件 (不存在时创建文件) **/
    public static FileInfo GetFileOrCreate(string filePath)
    {
        var file = new FileInfo(Path.Combine(RootFolder, filePath));
        if (!file.Exists)
        {
            file.Directory?.Create();
            file.Create().Close();
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
        if (stream == null) return;

        var filePath = Path.Combine(RootFolder, outPath);

        if (string.IsNullOrEmpty(Path.GetExtension(filePath))) filePath = Path.Combine(filePath, resourceName);

        var directoryPath = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        if (File.Exists(filePath)) return;

        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        stream.CopyTo(fileStream);
    }

    private static void ForEachFile(string dir, bool deep, Action<FileInfo> action)
    {
        var directory = new DirectoryInfo(Path.Combine(RootFolder, dir));
        if (!directory.Exists) return;

        foreach (var file in directory.GetFiles()) action(file);

        if (!deep) return;
        foreach (var subDir in directory.GetDirectories())
            ForEachFile(subDir.FullName, true, action);
    }
}