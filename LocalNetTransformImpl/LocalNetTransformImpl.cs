using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TransformInterface;

namespace LocalNetTransformImpl;

/// <summary>
/// 局域网发送文本实现
/// </summary>
public class LocalNetTransformImpl:ITransformText,IReceiveText
{
 
    private readonly string _localport;
    private readonly string _broadcastGroup;
    private readonly string _sendport;
    private readonly IDiscoverDevices _discoverDevices;

    private UdpClient _udpClient = null;
    private IPEndPoint _ipEndPoint = null;
    
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="localport">本机发送的端口</param>
    /// <param name="broadcastGroup">发送的局域网网段</param>
    /// <param name="sendport">发送时候局域网接受的端口</param>
    public LocalNetTransformImpl(string localport,string broadcastGroup,string sendport)
    {
       
        Debug.Assert(localport != null, nameof(localport) + " != null");
        _localport = localport;
        Debug.Assert(broadcastGroup != null, nameof(broadcastGroup) + " != null");
        _broadcastGroup = broadcastGroup;
        Debug.Assert(sendport != null, nameof(sendport) + " != null");
        _sendport = sendport;
        this._udpClient = new UdpClient(Int32.Parse(this._localport));
        this._udpClient.JoinMulticastGroup(IPAddress.Parse(this._broadcastGroup));
        this.receiveReady();
    }
    public LocalNetTransformImpl(string localport,string broadcastGroup,string sendport,IDiscoverDevices discoverDevices)
    {
       
        Debug.Assert(localport != null, nameof(localport) + " != null");
        _localport = localport;
        Debug.Assert(broadcastGroup != null, nameof(broadcastGroup) + " != null");
        _broadcastGroup = broadcastGroup;
        Debug.Assert(sendport != null, nameof(sendport) + " != null");
        _sendport = sendport;
        Debug.Assert(discoverDevices != null, nameof(discoverDevices) + " != null");
        _discoverDevices = discoverDevices;
        this._udpClient = new UdpClient(Int32.Parse(this._localport));
        this.receiveReady();
    }
    //接收线程
    Thread t;
    
    private void receiveReady()
    {

        this._ipEndPoint = new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport));
        
        t = new Thread(() =>
        {
            while (true)
            {
                if (this._udpClient!=null)
                {
                    var result = this._udpClient.Receive(ref this._ipEndPoint);
                    if (result.Length!=0)
                    {
                        this.ReceiveDataEvent?.Invoke("received");
                    }
                }
            }
            
        });
        t.Start();
    
    }
    
    public async Task TransformText(string content)
    {

        if (this._discoverDevices!=null)
        {
           await this._discoverDevices.Discover();
        }
        else
        {
            byte[] sendbytes = Encoding.Unicode.GetBytes(content);
            var send_res= await this._udpClient.SendAsync(sendbytes, sendbytes.Length,
                new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport)));
        }

    }

    public event ReceiveDataDelegate? ReceiveDataEvent;
}