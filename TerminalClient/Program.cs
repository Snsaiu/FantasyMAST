// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using FantasyMASTApplication;

using FantasyResultModel;

using TerminalClient;


ConsoleHelper.WriteSuccessLine("_________________________________________");
ConsoleHelper.WriteSuccessLine("| 欢迎使用Fantasy MAST (多平台同步工具) |");
ConsoleHelper.WriteSuccessLine("-----------------------------------------");
string exitFlag = "exit";
string? input = "";


// 检查配置文件

BootstrapConfig bootstrapConfig = new BootstrapConfig();
while (true)
{
    ResultBase<string> loadConfigFile = bootstrapConfig.LoadConfigFile();
    if (loadConfigFile.Ok)
    {
        break;
    }
    else
    {
        ConsoleHelper.WriteErrorLine(loadConfigFile.ErrorMsg);
        ConsoleHelper.WriteErrorLine("请选择配置文件保存位置(不需要填入文件名，精确到文件夹即可):");
        string configfile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(configfile))
        {
            continue;
            
        }
        else
        {
            ResultBase<string> writeConfigPath = bootstrapConfig.WriteConfigPath(Path.Combine(configfile, "config.json"));
            if (writeConfigPath.Ok)
            {
                ConsoleHelper.WriteSuccessLine("配置成功!");
                break;
            }
        }

    }
}


CommandFacade commandFacade = new CommandFacade();
do
{
    try
    {
        input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            ConsoleHelper.WriteErrorLine("命令不能为空!");
        }
        else
        {
            commandFacade.Run(input);
        }
    }
    catch (Exception e)
    {
        
    }
  
}
while (input!=exitFlag);

ConsoleHelper.WriteInfoLine("Bye!");
