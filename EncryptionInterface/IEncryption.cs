namespace EncryptionInterface
{
    /// <summary>
    /// 加密文本接口
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// 输入需要加密的文本，返回加密的结果
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Encryption(string text);

        /// <summary>
        /// 输入需要解密的文本，返回解密的结果
        /// </summary>
        /// <param name="encryption"></param>
        /// <returns></returns>
        string Deciphering(string encryption);

    }
}