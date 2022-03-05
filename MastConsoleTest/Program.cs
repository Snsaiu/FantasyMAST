// See https://aka.ms/new-console-template for more information
using TransformInterface;
using LocalNetTransformImpl;


Console.WriteLine("hello");
var impl = new LocalNetTransformImpl.LocalNetTransformImpl("8999", "224.0.0.123","9000");


impl.ReceiveDataEvent += (x) =>
{
    Console.WriteLine(x);
};
while (true)
{
    impl.TransformText("sdsd");
    Thread.Sleep(1000);
}
// while (true)
// {
//     impl.TransformText("hellow");
//     Thread.Sleep(1000);
//     Console.WriteLine("send ");
// }


