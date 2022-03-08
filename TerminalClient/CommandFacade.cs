namespace TerminalClient;

using TerminalClient.CommandParse.Impls;

public class CommandFacade
{

    public void Run(string command)
    {
        HelpCommandParse helpCommand = new HelpCommandParse(command);

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