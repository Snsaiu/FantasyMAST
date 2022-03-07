namespace JsonMASTConfig
{
    using MASTConfig;

    public class JsonConfig:ConfigBase
    {


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

        public JsonConfig()
        {
            
        }

        public JsonConfig(string userName)
            : base(userName)
        {
        }
    }
}