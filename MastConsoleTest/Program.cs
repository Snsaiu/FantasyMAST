// See https://aka.ms/new-console-template for more information

using System.Net;

using TransformInterface;
using LocalNetTransformImpl;

UdpReceiveDataImpl udpReceive = new UdpReceiveDataImpl("224.0.0.1", "9000");
udpReceive.ReceiveDataEvent += UdpReceive_ReceiveDataEvent;

void UdpReceive_ReceiveDataEvent(TransformInterface.Models.ReceiveDataModel content)
{

    Console.WriteLine(content.Content);
}

var discover = new UdpDiscoverDeviceImpl("8999", "224.0.0.1", "10000", "order");
var data= discover.Discover().Result;

Console.WriteLine(data.Count);

//Console.WriteLine("hello");
//var impl = new LocalNetTransformImpl.LocalNetTransformImpl("8999", IPAddress.Broadcast.ToString(),"9000");


//impl.ReceiveDataEvent += (x) =>
//{
//    Console.WriteLine(x.Content);
//};
while (true)
{
    
}
//while (true)
//{
//    impl.TransformText("sdsd");
//    Thread.Sleep(1000);
//}
// while (true)
// {
//     impl.TransformText("hellow");
//     Thread.Sleep(1000);
//     Console.WriteLine("send ");
// }


