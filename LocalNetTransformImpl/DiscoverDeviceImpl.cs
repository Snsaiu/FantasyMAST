using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TransformInterface;
using TransformInterface.Models;

namespace LocalNetTransformImpl;

public class DiscoverDeviceImpl:IDiscoverDevices
{
    private readonly string _localport;
    private readonly string _broadcastGroup;
    private readonly string _sendport;
    private readonly UdpClient _udpClient;

    public DiscoverDeviceImpl(string localport,string broadcastGroup,string sendport)
    {
       
        Debug.Assert(localport != null, nameof(localport) + " != null");
        _localport = localport;
        Debug.Assert(broadcastGroup != null, nameof(broadcastGroup) + " != null");
        _broadcastGroup = broadcastGroup;
        Debug.Assert(sendport != null, nameof(sendport) + " != null");
        _sendport = sendport;
        this._udpClient = new UdpClient(Int32.Parse(this._localport));
    }
    
    public async Task<List<DiscoveredDeviceModel>> Discover()
    {
        byte[] sendbytes = Encoding.Unicode.GetBytes("hello");
        var send_res= await this._udpClient.SendAsync(sendbytes, sendbytes.Length,
            new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport)));

        var data= await this._udpClient.ReceiveAsync();

        return null;
    }
}