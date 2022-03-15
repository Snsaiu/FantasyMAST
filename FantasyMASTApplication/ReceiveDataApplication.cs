using FantasyResultModel;
using FantasyResultModel.Impls;
using JsonMASTConfig;
using LocalNetTransformImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformInterface;

namespace FantasyMASTApplication
{
    using System.Net.Sockets;

    using AesEncryption;

    using FantasyMAST.Builder;

    using Newtonsoft.Json;

    using TransformInterface.Enums;
    using TransformInterface.Models;

    public class ReceiveDataApplication
    {
        private BootstrapConfig bootstrapConfig = null;

        private string filepath = "";
        private IReceiveData receiveData = null;

        public ReceiveDataApplication()
        {
            this.bootstrapConfig = new BootstrapConfig();
            this.filepath = this.bootstrapConfig.LoadConfigFile().Data;

        }

        public event ReceiveDataDelegate ListenEvent;
        public ResultBase<string> StartListen()
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            if (string.IsNullOrEmpty(jc.ReceivePort))
            {
                return new ErrorResultModel<string>("未设置接收端口");
            }
            if (string.IsNullOrEmpty(jc.GroupAddress))
            {
                return new ErrorResultModel<string>("未设置组");
            }

            this.receiveData = ReceiveDataBuilder.GetUdpReceiveDataInstance(jc.GroupAddress, jc.ReceivePort);

            this.receiveData.ReceiveDataEvent += (data) =>
            {
                if (this.ListenEvent != null)
                {

                    var ency = EncryptionBuilder.GetInstance(jc.UserName);
                    string dec_str= ency.Deciphering(data.Content);
                    if (dec_str!="")
                    {
                        SendDataModel sdm= JsonConvert.DeserializeObject<SendDataModel>(dec_str);
                        switch (sdm.SendType)
                        {
                            case SendType.Discover:
                                discoverProcess(sdm,data.Flag1);
                                break;
                            case SendType.TranformData:
                                tranformdata(sdm.Content);
                                break;
                            default:
                                break;
                        }
                    }

                    this.ListenEvent.Invoke(data);
                }
            };

            return new SuccessResultModel<string>($"组:{jc.GroupAddress} 端口:{jc.ReceivePort} 开始监听");
        }

        private void tranformdata(string? content)
        {
            

        }

        private void discoverProcess(SendDataModel data, string? address)
        {

            JsonConfig jc = new JsonConfig(this.filepath);
            SendDataModel sdm = new SendDataModel(SendType.Discover, DataType.Other, "saiu");
            var enc= EncryptionBuilder.GetInstance(jc.UserName);
            string sendinfo = enc.Encryption(JsonConvert.SerializeObject(sdm));
            var udp= TransformTextBuilder.GetUdpTransform(jc.GroupAddress, jc.SendPort);
            udp.TransformText(sendinfo);
        }
    }
}
