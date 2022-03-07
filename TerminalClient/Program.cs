// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using TerminalClient;


ConsoleHelper.WriteSuccessLine("_________________________________________");
ConsoleHelper.WriteSuccessLine("| 欢迎使用Fantasy MAST (多平台同步工具) |");
ConsoleHelper.WriteSuccessLine("-----------------------------------------");
string exitFlag = "exit";
string? input = "";

string x="hello 'lu jin' -d 'fdfdfd '";

int count = Regex.Matches(x, "'").Count;
if (count%2==0)
{
    int startIndex=0;
    List<int> potIndexs = new List<int>();
    for (int i = 0; i < count; i++)
    {
        int calc= x.IndexOf("\'", startIndex);
        potIndexs.Add(calc);
        startIndex = calc+1;
    }

    string command_str = x;
    Dictionary<string, string> dic = new Dictionary<string, string>();
    for (int i = 0; i < potIndexs.Count; i+=2)
    {
       string data=  command_str.Substring(potIndexs[i]+1, potIndexs[i + 1]-potIndexs[i]-1);
       dic["@" + i] = $"'{data}'";
    }

    foreach (KeyValuePair<string, string> pair in dic)
    {
       command_str=  command_str.Replace(pair.Value, pair.Key);
    }

    string[] splits= command_str.Split(" ");
    foreach (string split in splits)
    {
        if (string.IsNullOrWhiteSpace(split)==false)
        {
            //todo 
        }
    }

}
else
{
    //命令中的单引号数量有错误
}

return;


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
