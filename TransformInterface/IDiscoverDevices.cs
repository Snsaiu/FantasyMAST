using TransformInterface.Models;

namespace TransformInterface;

/// <summary>
/// 检查周围可用的设备
/// </summary>
public interface IDiscoverDevices
{
    /// <summary>
    /// 发现周围可用设备
    /// </summary>
    /// <returns>返回可用设备列表</returns>
    Task<List<DiscoveredDeviceModel>> Discover();

}