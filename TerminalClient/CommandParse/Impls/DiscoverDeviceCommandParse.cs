namespace TerminalClient.CommandParse.Impls;

using FantasyResultModel;
using FantasyResultModel.Impls;

/// <summary>
/// 设备发现
/// </summary>
public class DiscoverDeviceCommandParse : CommandParseBase
{
    public DiscoverDeviceCommandParse(string command)
        : base(command)
    {
    }

    protected override ResultBase<bool> ParseCommand(List<string> flags)
    {

        if (flags[0]=="discover")
        {
            if (flags.Count==2)
            {
                //获得当前用户已知的所有蓝牙设备
                switch (flags[1])
                {
                    case "-b-all-list":
                        break;
                    //获得当前用户已知的并在线的蓝牙设备
                    case "-b-online":
                        break;
                    //获得当前用户已知的所有局域网设备
                    case "-n-all-list":
                        break;
                    case "-n-online-list":
                        break;
                }
                return null;
            }
            else
            {
                ConsoleHelper.WriteCommandParamCountErrorLine("discover");
                return new SuccessResultModel<bool>(false);
            }
        }
        else
        {
            return new SuccessResultModel<bool>(false);
        }


    }
}