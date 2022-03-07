namespace TerminalClient.CommandParse;

using FantasyResultModel;

public abstract class CommandParseBase
{
    private readonly string command;

    public CommandParseBase(string command)
    {
        this.command = command;
    }
    /// <summary>
    /// 解析具体命令
    /// </summary>
    /// <returns>如果解析成功,并成功执行，那么data返回true，如果解析失败，那么data返回false(该指令不是该解析器可以处理的)，如果出现异常，直接返回false</returns>
    protected abstract ResultBase<bool> ParseCommand(List<string> flags);


    private List<string> splitCommand()
    {
        string[] splits = this.command.Split(" ");

        List<string> res = new List<string>();
        foreach (string split in splits)
        {
            if (string.IsNullOrWhiteSpace(split)==false)
            {
               res.Add(split); 
            }
        }

        return res;
    }

    public void Parse()
    {

         var splits= this.splitCommand();
         if (splits==null||splits.Count==0)
         {
             ConsoleHelper.WriteErrorLine("当前命令为空！无法继续执行");

             return;
         }

        ResultBase<bool> resultBase = this.ParseCommand(splits);
        if (resultBase.Ok&&resultBase.Data==false&&this.NextCommand!=null)
        {
            this.NextCommand.Parse();
        }
    }

    public CommandParseBase NextCommand { get; set; }
}