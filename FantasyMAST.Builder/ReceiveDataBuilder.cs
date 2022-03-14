namespace FantasyMAST.Builder;

using LocalNetTransformImpl;

using TransformInterface;

public static class ReceiveDataBuilder
{
    public static IReceiveData GetUdpReceiveDataInstance(string ipaddress, string receivePort)
    {
        return new UdpReceiveDataImpl(ipaddress, receivePort);
    }
}