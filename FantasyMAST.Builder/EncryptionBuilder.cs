namespace FantasyMAST.Builder;

using AesEncryption;

using EncryptionInterface;

public static class EncryptionBuilder
{

    public static IEncryption GetInstance()
    {
        return new DesEncryptionImpl();
    }

    public static IEncryption GetInstance(string token)
    {
        return new DesEncryptionImpl(token);
    }
}