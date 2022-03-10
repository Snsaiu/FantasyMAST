namespace TerminalClient.CommandParse.Impls;

using FantasyMASTApplication;

using FantasyResultModel;
using FantasyResultModel.Impls;

/// <summary>
/// 获得用户信息
/// </summary>
public class UserCommandParse : CommandParseBase
{
    private UserApplication userApplication = null;
    public UserCommandParse(string command)
        : base(command)
    {
        this.userApplication = new UserApplication();
    }

    protected override ResultBase<bool> ParseCommand(List<string> flags)
    {
        if (flags.Count == 1)
        {

            if (flags[0] != "user")
            {
                return new SuccessResultModel<bool>(false);
            }

            //清除用户
            ResultBase<string> user_res = this.userApplication.GetUserInfo();
            if (user_res.Ok)
            {
                ConsoleHelper.WriteSuccessLine("当前用户名: "+ user_res.Data);
                return new SuccessResultModel<bool>(true);
            }
            else
            {
                ConsoleHelper.WriteErrorLine(user_res.ErrorMsg);
                return new SuccessResultModel<bool>(true);
            }
        }
        else
        {
            ConsoleHelper.WriteCommandParamCountErrorLine("user");
            return new SuccessResultModel<bool>(false);
        }
    }
}