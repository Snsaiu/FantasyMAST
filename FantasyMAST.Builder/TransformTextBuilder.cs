namespace FantasyMAST.Builder;

using LocalNetTransformImpl;

using TransformInterface;

public static class TransformTextBuilder
{

    public static ITransformText GetUdpTransform(string localport, string broadcastGroup, string sendport)
    {
        return new LocalNetTransformImpl(localport, broadcastGroup, sendport);
    }
}