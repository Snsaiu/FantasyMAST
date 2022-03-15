namespace FantasyMAST.Builder;

using LocalNetTransformImpl;

using TransformInterface;

public static class TransformTextBuilder
{

    public static ITransformText GetUdpTransform( string broadcastGroup, string sendport)
    {
        return new LocalNetTransformImpl( broadcastGroup, sendport);
    }
}