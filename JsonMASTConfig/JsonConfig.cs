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
            this.UserName = "";
            this.CurrentDeviceInfo = null;
            this.Devices = null;
            this.StorePath = "";
            return this.writeFile();
        }

        public override bool UpdateDevices(List<DeviceInfo> devices)
        {
            this.Devices = devices;
            return this.writeFile();
        }

        public override bool UpdateStorePath(string? path)
        {
           this.StorePath=path;
           return this.writeFile();
        }

        public JsonConfig()
        {
            
        }

        public JsonConfig(string filePath)
        {
            this.filePath = filePath;
            string content= File.ReadAllText(this.filePath);
             var jsonconfig= JsonConvert.DeserializeObject<JsonConfig>(content);
             this.UserName = jsonconfig.UserName;
             this.CurrentDeviceInfo = jsonconfig.CurrentDeviceInfo;
             this.Devices = jsonconfig.Devices;
             this.StorePath = jsonconfig.StorePath;
        }

        public JsonConfig(string filePath, string userName)
            : base(userName)
        {
            this.filePath = filePath;

            File.Delete(this.filePath);

            this.UserName=userName;

            writeFile();
        }

        private bool writeFile()
        {
            try
            {
                string config = JsonConvert.SerializeObject(this);
                File.WriteAllText(this.filePath, config);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

            
        }
    }
}