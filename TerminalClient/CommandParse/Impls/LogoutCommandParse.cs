namespace TerminalClient.CommandParse.Impls;

using FantasyMASTApplication;

using FantasyResultModel;
using FantasyResultModel.Impls;

/// <summary>
/// 登出命令解析
/// </summary>
public class LogoutCommandParse: CommandParseBase
{
    private UserApplication userApplication = null;
    public LogoutCommandParse(string command)
        : base(command)
    {
        this.userApplication = new UserApplication();
    }

    protected override ResultBase<bool> ParseCommand(List<string> flags)
    {


        if (flags.Count==1)
        {

            if (flags[0]!="logout")
            {
                return new SuccessResultModel<bool>(false);
            }

            //清除用户
            ResultBase<bool> logout_res = this.userApplication.Logout();
            if (logout_res.Ok)
            {
                return new SuccessResultModel<bool>(true);
            }
            else
            {
                ConsoleHelper.WriteErrorLine(logout_res.ErrorMsg);
                return new SuccessResultModel<bool>(false);
            }
        }
        else
        {
         //   ConsoleHelper.WriteCommandParamCountErrorLine("logout");
            return new SuccessResultModel<bool>(false);
        }
    }
}