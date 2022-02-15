// See https://aka.ms/new-console-template for more information
using TransformInterface;
using LocalNetTransformImpl;


Console.WriteLine("hello");
var impl = new LocalNetTransformImpl.LocalNetTransformImpl("8999", "234.2.1.3","9000");
impl.TransformText("hellow");

