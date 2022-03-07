namespace TerminalClient.CommandParse.Impls;

using FantasyResultModel;
using FantasyResultModel.Impls;

public class HelpCommandParse:CommandParseBase
{
    public HelpCommandParse(string command)
        : base(command)
    {
    }

    /// <summary>
    /// 解析具体命令
    /// </summary>
    /// <returns>如果解析成功,并成功执行，那么data返回true，如果解析失败，那么data返回false(该指令不是该解析器可以处理的)，如果出现异常，直接返回false</returns>
    protected override ResultBase<bool> ParseCommand(List<string> flags)
    {
        foreach (var flag in flags)
        {
            Console.WriteLine(flag+"-");   
        }

        return new SuccessResultModel<bool>(true);
    }
}