using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TransformInterface;
using TransformInterface.Models;

namespace LocalNetTransformImpl;

public class UdpDiscoverDeviceImpl:IDiscoverDevices
{
    private readonly string _localport;
    private readonly string _broadcastGroup;
    private readonly string _sendport;

    private readonly string discoverOrder;

    /// <summary>
    /// 服务发现等待时间，单位是秒
    /// </summary>
    public int WaitTime { get; set; } = 2;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="localport">本地启动的udp客户端端口</param>
    /// <param name="broadcastGroup">本地发送的组</param>
    /// <param name="sendport">本地发送的客户端端口</param>
    /// <param name="discoverOrder">发送的口令</param>
    public UdpDiscoverDeviceImpl(string localport,string broadcastGroup,string sendport,string discoverOrder)
    {
       
        _localport = localport;
        _broadcastGroup = broadcastGroup;
        _sendport = sendport;
        this.discoverOrder = discoverOrder;
     
    }
    
    /// <summary>
    /// 客户端发现，通过udp发送并接受
    /// </summary>
    /// <returns>返回接受到的客户端，如果没有客户端，那么返回的数量是0</returns>
    public async Task<List<DiscoveredDeviceModel>> Discover()
    {
        //保存服务发现的设备
        List<DiscoveredDeviceModel> discoveredDevices = new List<DiscoveredDeviceModel>();

        //设置一个接收服务发现的udp客户端
        var receClient = new UdpClient(int.Parse(this._sendport));
        var t= Task.Run(
            async () =>
                {
                  
                    receClient.EnableBroadcast = true;
                    while (true)
                    {
                        var receiveAsync = await receClient.ReceiveAsync();
                        if (receiveAsync != null)
                        {
                            DiscoveredDeviceModel dm = new DiscoveredDeviceModel();
                            dm.Port = receiveAsync.RemoteEndPoint.Port.ToString();
                            dm.Ip = receiveAsync.RemoteEndPoint.Address.ToString();
                            discoveredDevices.Add(dm);
                        }
                    }
               
                });
        //设置发送的udp客户端
        var udpClient = new UdpClient();
        byte[] sendbytes = Encoding.Unicode.GetBytes(this.discoverOrder);
        await udpClient.SendAsync(sendbytes, sendbytes.Length,
                           new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport)));

        udpClient.Close();

        List<Task> task_list = new List<Task>();
        task_list.Add(t);
        //延时时间
        Task.WaitAny(task_list.ToArray(), TimeSpan.FromSeconds(this.WaitTime));
        
        receClient.Close();
        receClient.Dispose();
        receClient = null;
        return discoveredDevices;


    }

  
}