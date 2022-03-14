namespace FantasyMAST.Builder;

using LocalNetTransformImpl;

using TransformInterface;

public static class DiscoverDevicesBuilder
{

    public static IDiscoverDevices GetUdpDiscoverDevicesInstance(string broadcastGroup, string sendport, string discoverOrder, string token)
    {
        return new UdpDiscoverDeviceImpl(broadcastGroup, sendport, discoverOrder, token);
    }
}