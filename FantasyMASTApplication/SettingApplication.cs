using FantasyMASTApplication.Models;
using FantasyResultModel;
using FantasyResultModel.Impls;
using JsonMASTConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMASTApplication
{
    public class SettingApplication
    {
        private BootstrapConfig bootstrapConfig = null;

        private string filepath = "";

        public SettingApplication()
        {
            this.bootstrapConfig = new BootstrapConfig();
            this.filepath = this.bootstrapConfig.LoadConfigFile().Data;
        }

        public ResultBase<bool> SetDisconverDevicePort(string port)
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            jc.UpdateDiscoverPort(port);
            return new SuccessResultModel<bool>(true);
        }

        public ResultBase<bool> SetSendPort(string port)
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            jc.UpdateSendPort(port);
            return new SuccessResultModel<bool>(true);
        }

        public ResultBase<bool> SetReceivePort(string port)
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            jc.UpdateReceivePort(port);
            return new SuccessResultModel<bool>(true);
        }


        public ResultBase<bool> SetGroupAdress(string address)
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            jc.UpdateGroupAddress(address);
            return new SuccessResultModel<bool>(true);
        }


        public ResultBase<bool> SetStorePath(string path)
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            jc.UpdateStorePath(path);
            return new SuccessResultModel<bool>(true);
        }

        public ResultBase<string> GetStorePath()
        {
            JsonConfig jc = new JsonConfig(this.filepath);

            string path = jc.StorePath;

            if (string.IsNullOrEmpty(path))
            {
                return new ErrorResultModel<string>("存储路径为空");
            }
            else
            {
                return new SuccessResultModel<string>(path);
            }
        }


        public ResultBase<AddressPortModel> GetAddressPorts()
        {
            JsonConfig jc = new JsonConfig(this.filepath);

            if (string.IsNullOrWhiteSpace(jc.SendPort))
            {
                return new ErrorResultModel<AddressPortModel>("发送端口不合法");
            }
            if (string.IsNullOrWhiteSpace(jc.ReceivePort))
            {
                return new ErrorResultModel<AddressPortModel>("接收端口不合法");
            }
            if (string.IsNullOrWhiteSpace(jc.GroupAddress))
            {
                return new ErrorResultModel<AddressPortModel>("组不合法");
            }
            if (string.IsNullOrWhiteSpace(jc.DiscoverPort))
            {
                return new ErrorResultModel<AddressPortModel>("设备发现端口不合法");
            }

            AddressPortModel addressPortModel = new AddressPortModel(jc.SendPort, jc.ReceivePort, jc.GroupAddress, jc.DiscoverPort);
            return new SuccessResultModel<AddressPortModel>(addressPortModel);

        }

    }
}
