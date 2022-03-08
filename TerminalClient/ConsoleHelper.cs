namespace TerminalClient;

/// <summary>
/// 控制台帮助类
/// </summary>
public static class ConsoleHelper
{

    static void WriteColorLine(string str, ConsoleColor color)
    {
        ConsoleColor currentForeColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(str);
        Console.ForegroundColor = currentForeColor;
    }

    /// <summary>
    /// 打印错误信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteErrorLine(this string str, ConsoleColor color = ConsoleColor.Red)
    {
        WriteColorLine(str, color);
    }

    /// <summary>
    /// 命令参数错误提示
    /// </summary>
    /// <param name="commandName">命令名称</param>
    public static void WriteCommandParamCountErrorLine(string commandName)
    {
        WriteErrorLine(commandName+ " 命令参数数量错误,请使用 -help 查看帮助!");
    }

    /// <summary>
    /// 命令参数指令错误
    /// </summary>
    /// <param name="commandName">命令名称</param>
    public static void WriteCommandParamErrorLine(string commandName)
    {
        WriteErrorLine(commandName + " 命令指令错误,请使用 -help 查看帮助!");
    }

        /// <summary>
    /// 打印警告信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteWarningLine(this string str, ConsoleColor color = ConsoleColor.Yellow)
    {
        WriteColorLine(str, color);
    }
    /// <summary>
    /// 打印正常信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteInfoLine(this string str, ConsoleColor color = ConsoleColor.White)
    {
        WriteColorLine(str, color);
    }
    /// <summary>
    /// 打印成功的信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteSuccessLine(this string str, ConsoleColor color = ConsoleColor.Green)
    {
        WriteColorLine(str, color);
    }
}