namespace TransformInterface;

using TransformInterface.Models;

public delegate void ReceiveDataDelegate(ReceiveDataModel content);
public interface IReceiveData
{
  

    event ReceiveDataDelegate ReceiveDataEvent;

     /// <summary>
     /// ֹͣ����
     /// </summary>
     void CloseWatch();

}