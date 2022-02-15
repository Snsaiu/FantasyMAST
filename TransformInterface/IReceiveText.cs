namespace TransformInterface;

public delegate void ReceiveDataDelegate(string content);

public interface IReceiveText
{

     event ReceiveDataDelegate ReceiveDataEvent;

}