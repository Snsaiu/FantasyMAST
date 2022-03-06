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
    /// �����ֵȴ�ʱ�䣬��λ����
    /// </summary>
    public int WaitTime { get; set; } = 2;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="localport">����������udp�ͻ��˶˿�</param>
    /// <param name="broadcastGroup">���ط��͵���</param>
    /// <param name="sendport">���ط��͵Ŀͻ��˶˿�</param>
    /// <param name="discoverOrder">���͵Ŀ���</param>
    public UdpDiscoverDeviceImpl(string localport,string broadcastGroup,string sendport,string discoverOrder)
    {
       
        _localport = localport;
        _broadcastGroup = broadcastGroup;
        _sendport = sendport;
        this.discoverOrder = discoverOrder;
     
    }
    
    /// <summary>
    /// �ͻ��˷��֣�ͨ��udp���Ͳ�����
    /// </summary>
    /// <returns>���ؽ��ܵ��Ŀͻ��ˣ����û�пͻ��ˣ���ô���ص�������0</returns>
    public async Task<List<DiscoveredDeviceModel>> Discover()
    {
        //��������ֵ��豸
        List<DiscoveredDeviceModel> discoveredDevices = new List<DiscoveredDeviceModel>();

        //����һ�����շ����ֵ�udp�ͻ���
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
        //���÷��͵�udp�ͻ���
        var udpClient = new UdpClient();
        byte[] sendbytes = Encoding.Unicode.GetBytes(this.discoverOrder);
        await udpClient.SendAsync(sendbytes, sendbytes.Length,
                           new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport)));

        udpClient.Close();

        List<Task> task_list = new List<Task>();
        task_list.Add(t);
        //��ʱʱ��
        Task.WaitAny(task_list.ToArray(), TimeSpan.FromSeconds(this.WaitTime));
        
        receClient.Close();
        receClient.Dispose();
        receClient = null;
        return discoveredDevices;


    }

  
}