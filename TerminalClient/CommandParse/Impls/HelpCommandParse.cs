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

        if (flags[0]=="help")
        {


            ConsoleHelper.WriteInfoLine("login 登录/注册用户");
            ConsoleHelper.WriteInfoLine("   -u <name> 登录/注册的用户名");

            ConsoleHelper.WriteInfoLine("logout 登出");
            ConsoleHelper.WriteInfoLine("user 查看当前用户");

            return new SuccessResultModel<bool>(true);

        }
        else
        {
            return new SuccessResultModel<bool>(false);
        }

    }
}