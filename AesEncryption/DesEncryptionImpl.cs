namespace AesEncryption
{
    using System.Security.Cryptography;
    using System.Text;

    using EncryptionInterface;

    public class DesEncryptionImpl: IEncryption
    {
        private readonly string secretKey;
        // 默认密钥向量
        private  byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public DesEncryptionImpl(string secretKey)
        {
            this.secretKey = secretKey;
            if (secretKey.Length<8)
            {
                while (true)
                {
                    this.secretKey += "m";
                    if (this.secretKey.Length==8)
                    {
                        break;
                        
                    }
                }
            }
        }

        public DesEncryptionImpl()
        {
            this.secretKey = "Fantasym";
        }
        public string Encryption(string text)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(this.secretKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(text);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "";
            }

        }

        public string Deciphering(string encryption)
        {
            if (encryption == "")
                return "";
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(this.secretKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(encryption);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }

        }
    }
}