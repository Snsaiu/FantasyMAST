namespace LocalNetTransformImpl;

using System.Net;
using System.Net.Sockets;
using System.Text;

using TransformInterface;
using TransformInterface.Models;

public class UdpReceiveDataImpl:IReceiveData
{
    private readonly string ipaddress;

    private readonly string port;

    private readonly string receivePort;

    private bool receiveFlag = true;

    public UdpReceiveDataImpl(string ipaddress, string port,string receivePort)
    {
        this.ipaddress = ipaddress;
        this.port = port;
        this.receivePort = receivePort;
        this.receiveReady();
    }

    private Thread t;

    private void receiveReady()
    {
        var udpreceiver = new UdpClient(int.Parse(this.port));
        var recivecast = new IPEndPoint(IPAddress.Parse(this.ipaddress), int.Parse(this.receivePort));
        udpreceiver.EnableBroadcast = true;

        t = new Thread(
            async () =>
        {
            while (this.receiveFlag)
            {

                var result = await udpreceiver.ReceiveAsync();
                if (result != null)
                {
                    string data = Encoding.UTF8.GetString(result.Buffer);
                    ReceiveDataModel receiveDataModel = new ReceiveDataModel();
                    receiveDataModel.Content = data;
                    receiveDataModel.Flag1 = result.RemoteEndPoint.Address.ToString();
                    receiveDataModel.Flag2 = result.RemoteEndPoint.Port.ToString();
                    this.ReceiveDataEvent?.Invoke(receiveDataModel);
                }
            }
            udpreceiver.Close();

        });
        this.t.IsBackground = true;
        t.Start();

    }

    public event ReceiveDataDelegate? ReceiveDataEvent;

    public void CloseWatch()
    {
        this.receiveFlag = false;
    }
}