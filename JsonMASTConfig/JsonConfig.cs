namespace JsonMASTConfig
{
    using System.Text.Json.Serialization;

    using MASTConfig;

    using Newtonsoft.Json;

    public class JsonConfig:ConfigBase
    {
        private readonly string filePath;

        public override bool Unregist()
        {
            throw new NotImplementedException();
        }

        public override bool UpdateDevices(List<DeviceInfo> devices)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateStorePath(string? path)
        {
            throw new NotImplementedException();
        }

        public JsonConfig(string filePath)
        {
            this.filePath = filePath;
        }

        public JsonConfig(string filePath, string userName)
            : base(userName)
        {
            this.filePath = filePath;

            File.Delete(this.filePath);

            this.UserName=userName;

            string config= JsonConvert.SerializeObject(this);
            File.WriteAllText(this.filePath,config);
        }
    }
}