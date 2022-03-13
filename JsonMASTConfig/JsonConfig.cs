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

            this.SendPort = "";
            this.ReceivePort = "";
            this.DiscoverPort ="";
            this.GroupAddress = "";

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
            string content = "";
            using (StreamReader sr=new StreamReader(this.filePath))
            {
                content=sr.ReadToEnd();
            }
           
            if (content!="")
            {
                var jsonconfig = JsonConvert.DeserializeObject<JsonConfig>(content);
                this.UserName = jsonconfig.UserName;
                this.CurrentDeviceInfo = jsonconfig.CurrentDeviceInfo;
                this.Devices = jsonconfig.Devices;
                this.StorePath = jsonconfig.StorePath;
                this.SendPort = jsonconfig.SendPort;
                this.ReceivePort = jsonconfig.ReceivePort; ;
                this.DiscoverPort = jsonconfig.DiscoverPort;
                this.GroupAddress = jsonconfig.GroupAddress;

            }
            
        }

        public JsonConfig(string filePath, string userName,bool initPort)
            : base(userName)
        {
            this.filePath = filePath;

            File.Delete(this.filePath);

            this.UserName=userName;
            if (initPort)
            {
                this.SendPort = "8999";
                this.ReceivePort = "9000";
                this.DiscoverPort = "8888";
                this.GroupAddress = "224.0.0.1";

            }


            writeFile();
        }

        private bool writeFile()
        {
            try
            {
                string config = JsonConvert.SerializeObject(this);
                //File.WriteAllText(this.filePath, config);
                using (StreamWriter sw=new StreamWriter(this.filePath,false))
                {
                    sw.Write(config);
                  
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

            
        }

        public override bool UpdateSendPort(string? port)
        {
        
            this.SendPort=port;
            return this.writeFile();

        }

        public override bool UpdateReceivePort(string? port)
        {

            this.ReceivePort = port;
            return this.writeFile();
        }

        public override bool UpdateGroupAddress(string? address)
        {
            this.GroupAddress = address;
            return this.writeFile();

        }

        public override bool UpdateDiscoverPort(string? port)
        {
            this.DiscoverPort = port;
            return this.writeFile();
        }
    }
}