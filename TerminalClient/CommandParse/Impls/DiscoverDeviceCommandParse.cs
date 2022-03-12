namespace TerminalClient.CommandParse.Impls;

using FantasyMASTApplication;
using FantasyResultModel;
using FantasyResultModel.Impls;
using System;

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
                        return new SuccessResultModel<bool>(false);
                    //获得当前用户已知的并在线的蓝牙设备
                    case "-b-online":
                        return new SuccessResultModel<bool>(false);
                    //获得当前用户已知的所有局域网设备
                    case "-n-all-list":
                        return n_all_list();
                    case "-n-online-list":
                        return new SuccessResultModel<bool>(false);
                    default:
                        return new SuccessResultModel<bool>(false);
                }
              
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

    private  ResultBase<bool> n_all_list()
    {

        DiscoverDevicesApplication discoverDevicesApplication = new DiscoverDevicesApplication();
        ResultBase<List<TransformInterface.Models.DiscoveredDeviceModel>> resultBase =  discoverDevicesApplication.LocalNetDiscoverAsync().Result;

        if (resultBase.Ok)
        {
            if (resultBase.Data!=null)
            {
                ConsoleHelper.WriteSuccessLine("发现设备:");
                foreach (var item in resultBase.Data )
                {
                    ConsoleHelper.WriteSuccessLine( $"      ip:{item.Ip}  设备名:{item.DeviceName}");
                }
                ConsoleHelper.WriteSuccessLine("-----------------------------------------------------");
            }
            else
            {
                ConsoleHelper.WriteWarningLine("未发现任何设备!");
            }
            return new SuccessResultModel<bool>(true);

        }
       return new ErrorResultModel<bool>(resultBase.ErrorMsg);
    }
}