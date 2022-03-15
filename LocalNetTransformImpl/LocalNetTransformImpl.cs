using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TransformInterface;

namespace LocalNetTransformImpl;

using System.Data.SqlTypes;

using TransformInterface.Models;

/// <summary>
/// 局域网发送文本实现
/// </summary>
public class LocalNetTransformImpl:ITransformText
{
 

    private readonly string _broadcastGroup;
    private readonly string _sendport;
    private readonly IDiscoverDevices _discoverDevices;

    /// <summary>
    /// 接受循环停止标记
    /// </summary>
    private bool receiveFlag = true;



    
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="localport">本机发送的端口</param>
    /// <param name="broadcastGroup">发送的局域网网段</param>
    /// <param name="sendport">发送时候局域网接受的端口</param>
    public LocalNetTransformImpl(string broadcastGroup,string sendport)
    {
       

        _broadcastGroup = broadcastGroup;
        _sendport = sendport;
    }
    public LocalNetTransformImpl(string broadcastGroup,string sendport,IDiscoverDevices discoverDevices)
    {
       
      
        _broadcastGroup = broadcastGroup;
        _sendport = sendport;
        _discoverDevices = discoverDevices;
       
    }
    //接收线程
    Thread t;
    
 
    
    public async Task TransformText(string content)
    {

        if (this._discoverDevices!=null)
        {
        //   await this._discoverDevices.Discover();
        }
        else
        {
            byte[] sendbytes = Encoding.Unicode.GetBytes(content);

            UdpClient uc = new UdpClient();
            var send_res = await uc.SendAsync(sendbytes, sendbytes.Length,
                new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport)));
            uc.Dispose();
            uc.Close();
        }

    }

    public event ReceiveDataDelegate? ReceiveDataEvent;


}