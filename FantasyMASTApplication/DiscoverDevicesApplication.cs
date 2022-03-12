namespace FantasyMASTApplication;

using FantasyResultModel;

using LocalNetTransformImpl;

using TransformInterface;
using TransformInterface.Models;

/// <summary>
/// 设备发现服务
/// </summary>
public class DiscoverDevicesApplication
{
    private IDiscoverDevices udpDiscoverDevices = null;
    public DiscoverDevicesApplication()
    {
      
    }

    /// <summary>
    /// 设备发现
    /// </summary>
    /// <param name="token">设备发送的密文</param>
    /// <returns>返回结果</returns>
    public Task<ResultBase< List<DiscoveredDeviceModel>>> DiscoverAsync(string token)
    {
        // 读取配置文件，获得端口；

        return null;


    }
}