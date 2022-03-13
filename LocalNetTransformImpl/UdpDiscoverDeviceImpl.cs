using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TransformInterface;
using TransformInterface.Models;

namespace LocalNetTransformImpl;

using AesEncryption;

using Newtonsoft.Json;

using TransformInterface.Enums;

public class UdpDiscoverDeviceImpl:IDiscoverDevices
{
    private readonly string _broadcastGroup;
    private readonly string _sendport;

    private readonly string discoverOrder;

    private readonly string token;

    public int WaitTime { get; set; } = 2;
    
    public UdpDiscoverDeviceImpl(string broadcastGroup,string sendport,string discoverOrder,string token)
    {
       
       
        _broadcastGroup = broadcastGroup;
        _sendport = sendport;
        this.discoverOrder = discoverOrder;
        this.token = token;
    }
    

    public async Task<List<DiscoveredDeviceModel>> Discover()
    {
 
        List<DiscoveredDeviceModel> discoveredDevices = new List<DiscoveredDeviceModel>();

        var receClient = new UdpClient(int.Parse(this._sendport));
        receClient.JoinMulticastGroup(IPAddress.Parse(this._broadcastGroup));
        var t= Task.Run(
            async () =>
                {
                  
                    receClient.EnableBroadcast = true;
                    while (true)
                    {
                        var receiveAsync = await receClient.ReceiveAsync();
                        if (receiveAsync != null)
                        {
                            try
                            {
                                string data = Encoding.UTF8.GetString( receiveAsync.Buffer,0,receiveAsync.Buffer.Length);
                               //解密
                               var encry = new DesEncryptionImpl(this.token);
                               string deciphering_str = encry.Deciphering(data);
                                SendDataModel sdm = JsonConvert.DeserializeObject<SendDataModel>(deciphering_str);
                                if (sdm.SendType==SendType.Discover)
                                {
                                    if (this.token==sdm.Content)
                                    {
                                        if (this.getLocalIp()!= receiveAsync.RemoteEndPoint.Address.ToString())
                                        {
                                            DiscoveredDeviceModel dm = new DiscoveredDeviceModel();
                                            dm.Port = receiveAsync.RemoteEndPoint.Port.ToString();
                                            dm.Ip = receiveAsync.RemoteEndPoint.Address.ToString();
                                            discoveredDevices.Add(dm);
                                        }
                                       
                                    }

                                }
                             
                            }
                            catch (Exception e)
                           {
                            
                           }
                      

                          
                        }
                    }
               
                });

        var udpClient = new UdpClient();
        byte[] sendbytes = Encoding.UTF8.GetBytes(this.discoverOrder);
        await udpClient.SendAsync(sendbytes, sendbytes.Length,
                           new IPEndPoint(IPAddress.Parse(this._broadcastGroup), Int32.Parse(this._sendport)));

        udpClient.Close();

        List<Task> task_list = new List<Task>();
        task_list.Add(t);
        
        Task.WaitAny(task_list.ToArray(), TimeSpan.FromSeconds(this.WaitTime));
        
        receClient.Close();
        receClient.Dispose();
        receClient = null;
        return discoveredDevices;


    }

    private string getLocalIp()
    {
        var addressList = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
        var ips = addressList.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .Select(address => address.ToString()).ToArray();
        if (ips.Length == 1)
        {
            return ips.First();
        }
        return ips.Where(address => !address.EndsWith(".1")).FirstOrDefault() ?? ips.FirstOrDefault();
    }

}