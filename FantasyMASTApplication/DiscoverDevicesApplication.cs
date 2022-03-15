namespace FantasyMASTApplication;

using AesEncryption;

using EncryptionInterface;

using FantasyMAST.Builder;

using FantasyResultModel;
using FantasyResultModel.Impls;
using JsonMASTConfig;
using LocalNetTransformImpl;

using Newtonsoft.Json;

using TransformInterface;
using TransformInterface.Enums;
using TransformInterface.Models;

/// <summary>
/// 设备发现服务
/// </summary>
public class DiscoverDevicesApplication
{

    private BootstrapConfig bootstrapConfig = null;

 
    private string filepath = "";
    private IDiscoverDevices udpDiscoverDevices = null;
    public DiscoverDevicesApplication()
    {
      
        this.bootstrapConfig = new BootstrapConfig();
        this.filepath = this.bootstrapConfig.LoadConfigFile().Data;
    }

    /// <summary>
    /// 设备发现
    /// </summary>
    /// <param name="token">设备发送的密文</param>
    /// <returns>返回结果</returns>
    public async Task<ResultBase< List<DiscoveredDeviceModel>>> LocalNetDiscoverAsync()
    {
        // 读取配置文件，获得端口；
        JsonConfig jc = new JsonConfig(this.filepath);
       var encryption = EncryptionBuilder.GetInstance(jc.UserName);

       SendDataModel sdm = new SendDataModel(SendType.Discover, DataType.Other, jc.UserName);
       string data_str= JsonConvert.SerializeObject(sdm);

       string encryption_st= encryption.Encryption(data_str);

        // 设置口令

        this.udpDiscoverDevices = DiscoverDevicesBuilder.GetUdpDiscoverDevicesInstance(jc.GroupAddress, jc.DiscoverPort, encryption_st,jc.UserName);
        try
        {
            List<DiscoveredDeviceModel> task_res = await this.udpDiscoverDevices.Discover();

            return new SuccessResultModel<List<DiscoveredDeviceModel>>(task_res);

        }
        catch (Exception e)
        {

            return new ErrorResultModel<List<DiscoveredDeviceModel>>(e.Message);
        }



    }
}