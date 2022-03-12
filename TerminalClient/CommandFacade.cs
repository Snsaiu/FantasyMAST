namespace TerminalClient;

using TerminalClient.CommandParse.Impls;

public class CommandFacade
{

    public void Run(string command)
    {
        HelpCommandParse helpCommand = new HelpCommandParse(command);

        LoginCommandParse loginCommand = new LoginCommandParse(command);
        helpCommand.NextCommand = loginCommand;

        LogoutCommandParse logoutCommand = new LogoutCommandParse(command);
        loginCommand.NextCommand = logoutCommand;

        UserCommandParse userCommand = new UserCommandParse(command);
        logoutCommand.NextCommand= userCommand;

        SettingCommandParse settingCommand=new SettingCommandParse(command);
        userCommand.NextCommand = settingCommand;

        DiscoverDeviceCommandParse discoverDeviceCommandParse=new DiscoverDeviceCommandParse(command);
        settingCommand.NextCommand = discoverDeviceCommandParse;

        if (helpCommand.Parse())
        {
            ConsoleHelper.WriteSuccessLine("命令执行成功!");
        }
        else
        {
            ConsoleHelper.WriteErrorLine("命令执行错误!请检查命令格式是否正确");
        }
    }
}