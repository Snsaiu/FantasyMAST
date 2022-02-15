namespace TransformInterface.Models;

public class DiscoveredDeviceModel
{
    /// <summary>
    /// 设备ip
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备发送端口
    /// </summary>
    public string Port { get; set; }
}