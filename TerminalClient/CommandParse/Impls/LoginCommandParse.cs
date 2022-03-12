namespace TerminalClient.CommandParse.Impls;

using FantasyMASTApplication;

using FantasyResultModel;
using FantasyResultModel.Impls;

public class LoginCommandParse: CommandParseBase
{

    private UserApplication userApplication = null;

    public LoginCommandParse(string command)
        : base(command)
    {
        this.userApplication = new UserApplication();
    }

    protected override ResultBase<bool> ParseCommand(List<string> flags)
    {
        if (flags[0] == "login")
        {
            if (flags.Count==3)
            {
                if (flags[1]=="-u")
                {

                    string loginName = flags[2];
                 
                    ResultBase<bool> login_res = this.userApplication.Login(loginName);

                    if (login_res.Ok)
                    {
                        return new SuccessResultModel<bool>(true);
                    }
                    else
                    {
                        ConsoleHelper.WriteErrorLine(login_res.ErrorMsg);
                        return new ErrorResultModel<bool>(login_res.ErrorMsg);
                    }

                 
                }
                else
                {
                    ConsoleHelper.WriteCommandParamErrorLine("login");
                    return new SuccessResultModel<bool>(false);
                }



            }
            else
            { 
                ConsoleHelper.WriteCommandParamCountErrorLine("login");
                return new SuccessResultModel<bool>(false);
            }

        }
        else
        {
            return new SuccessResultModel<bool>(false);
        }



    }
}