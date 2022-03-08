// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using TerminalClient;


ConsoleHelper.WriteSuccessLine("_________________________________________");
ConsoleHelper.WriteSuccessLine("| 欢迎使用Fantasy MAST (多平台同步工具) |");
ConsoleHelper.WriteSuccessLine("-----------------------------------------");
string exitFlag = "exit";
string? input = "";


CommandFacade commandFacade = new CommandFacade();
do
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
while (input!=exitFlag);

ConsoleHelper.WriteInfoLine("Bye!");
