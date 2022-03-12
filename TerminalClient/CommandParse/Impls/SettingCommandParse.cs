using FantasyMASTApplication;
using FantasyMASTApplication.Models;
using FantasyResultModel;
using FantasyResultModel.Impls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TerminalClient.CommandParse.Impls
{
    public class SettingCommandParse : CommandParseBase
    {
        private SettingApplication settingApplication = null;
       
        public SettingCommandParse(string command) : base(command)
        {
            this.settingApplication= new SettingApplication();
        }

        protected override ResultBase<bool> ParseCommand(List<string> flags)
        {
            if (flags[0]=="setting")
            {

                if (flags.Count == 2 )
                {
                    if (flags[1]=="-get")
                    {
                        return this.getInfo();
                        //return new SuccessResultModel<bool>(true);
                    }
                    else
                    {
                        ConsoleHelper.WriteCommandParamErrorLine("setting");
                        return new ErrorResultModel<bool>("setting 参数错误");
                    }
                }
                else if (flags.Count == 3)
                {
                    switch (flags[1])
                    {
                        case "-set-store-path":
                            return parseStorePath(flags[2]);
                        case "-set-group-address":
                            return parseGroupAddress(flags[2]);
                        case "-set-send-port":
                            return parseSendPort(flags[2]);
                        case "-set-receive-port":
                            return parseReceivePort(flags[2]);
                        case "-set-discover-port":
                            return parseDiscoverPort(flags[2]);
                        case "-get":
                            return getInfo();
                        default:
                            ConsoleHelper.WriteCommandParamErrorLine("setting");
                            return new ErrorResultModel<bool>("");
                    }
                }
                else
                {
                    ConsoleHelper.WriteCommandParamCountErrorLine("setting");

                    return new SuccessResultModel<bool>(false);
                }
            }
            else
            {
                return new SuccessResultModel<bool>(false);
            }
        }

        private ResultBase<bool> getInfo()
        {
            ResultBase<AddressPortModel> resultBase = this.settingApplication.GetAddressPorts();
            if (resultBase.Ok)
            {
             ResultBase<string> path_res=    this.settingApplication.GetStorePath();

                ConsoleHelper.WriteSuccessLine($"组:{resultBase.Data.GroupAddress} 发送端口:{resultBase.Data.SendPort} 接收端口:" +
                    $"{resultBase.Data.ReceivePort} 设备发现端口:{resultBase.Data.DiscoverPort} " +
                    $"文件存储路径:{path_res.Data}");
                return new SuccessResultModel<bool>(true);
            }
            else
            {
                return new ErrorResultModel<bool>(resultBase.ErrorMsg);
            }
        }

        private ResultBase<bool> parseDiscoverPort(string v)
        {
            int port = 0;
            if (int.TryParse(v,out port))
            {
                this.settingApplication.SetDisconverDevicePort(v);
                return new SuccessResultModel<bool>(true);
            }
            else
            {
                return new ErrorResultModel<bool>("setting 参数内容错误");
            }

           

        }

        private ResultBase<bool> parseReceivePort(string v)
        {
            int port = 0;
            if (int.TryParse(v, out port))
            {
                this.settingApplication.SetReceivePort(v);
                return new SuccessResultModel<bool>(true);
            }
            else
            {
                return new ErrorResultModel<bool>("setting 参数内容错误");
            }


        }

        private ResultBase<bool> parseSendPort(string v)
        {
            int port = 0;
            if (int.TryParse(v, out port))
            {
                this.settingApplication.SetSendPort(v);
                return new SuccessResultModel<bool>(true);
            }
            else
            {
                return new ErrorResultModel<bool>("setting 参数内容错误");
            }


        }

        private ResultBase<bool> parseGroupAddress(string v)
        {

            this.settingApplication.SetGroupAdress(v);
            return new SuccessResultModel<bool>(true);
        }

        private ResultBase<bool> parseStorePath(string v)
        {
            if (Directory.Exists(v))
            {
                this.settingApplication.SetStorePath(v);
                return new SuccessResultModel<bool>(true);

            }
            else
            {
                return new ErrorResultModel<bool>("setting 参数错误");
            }
        }
    }
}
