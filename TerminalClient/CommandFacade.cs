namespace TerminalClient;

using TerminalClient.CommandParse.Impls;

public class CommandFacade
{

    public void Run(string command)
    {
        HelpCommandParse helpCommand = new HelpCommandParse(command);

        helpCommand.Parse();
    }
}