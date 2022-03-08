namespace TerminalClient.CommandParse.Impls;

using FantasyResultModel;
using FantasyResultModel.Impls;

public class LoginCommandParse: CommandParseBase
{
    public LoginCommandParse(string command)
        : base(command)
    {
    }

    protected override ResultBase<bool> ParseCommand(List<string> flags)
    {
        if (flags[0] == "-login")
        {
            if (flags.Count==3)
            {
                if (flags[1]=="-u")
                {

                    string loginName = flags[2];
                    //todo 登录命令执行



                    return new SuccessResultModel<bool>(true);
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