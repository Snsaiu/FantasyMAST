namespace FantasyMASTApplication
{
    using FantasyMASTApplication.Models;

    using FantasyResultModel;
    using FantasyResultModel.Impls;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    public class BootstrapConfig
    {

     

        public BootstrapConfig()
        {
        
        }

        public ResultBase<string> LoadConfigFile()
        {
            try
            {
                //添加 json 文件路径
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                //创建配置根对象
                var configurationRoot = builder.Build();
                var nameSection = configurationRoot.GetSection("Path");

                if (string.IsNullOrWhiteSpace(nameSection.Value))
                {
                    return new ErrorResultModel<string>("配置文件中无法获得 path路径的值!");
                }
                else
                {
                    return new SuccessResultModel<string>(nameSection.Value);
                }

            }
            catch (Exception e)
            {
                return new ErrorResultModel<string>(e.Message);
            }
        }

        public ResultBase<string> WriteConfigPath(string path)
        {
            //判断文件是否存在

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Create(path).Close();
               

                ApplicationSetting applicationSetting = new ApplicationSetting();
                applicationSetting.Path = path;

                string json= JsonConvert.SerializeObject(applicationSetting);
                File.WriteAllText("appsettings.json",json);
                return new SuccessResultModel<string>("");
            }
            catch (Exception e)
            {
                return new ErrorResultModel<string>("");
            }
    
        }
    }
}