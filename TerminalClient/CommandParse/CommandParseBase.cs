namespace TerminalClient.CommandParse;

using System.Text.RegularExpressions;

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

        List<string> res = new List<string>();

        int count = Regex.Matches(this.command, "\"").Count;
        if (count % 2 == 0)
        {
            int startIndex = 0;
            List<int> potIndexs = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int calc = this.command.IndexOf("\"", startIndex);
                potIndexs.Add(calc);
                startIndex = calc + 1;
            }

            string command_str = this.command;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < potIndexs.Count; i += 2)
            {
                string data = command_str.Substring(potIndexs[i] + 1, potIndexs[i + 1] - potIndexs[i] - 1);
                dic["@" + i] = $"\"{data}\"";
            }

            foreach (KeyValuePair<string, string> pair in dic)
            {
                command_str = command_str.Replace(pair.Value, pair.Key);
            }

            string[] splits = command_str.Split(" ");
            foreach (string split in splits)
            {
                bool flag=false;
                if (string.IsNullOrWhiteSpace(split) == false)
                {

                    foreach (KeyValuePair<string, string> keyValuePair in dic)
                    {
                        if (split==keyValuePair.Key)
                        {
                            res.Add(keyValuePair.Value.Replace("\"",""));
                            flag = true;
                            break;
                        }
                    }

                    if (flag==false)
                    {
                        res.Add(split);
                    }

                }
            }

            return res;

        }
       else if (count == 0)
        {
            string[] splits = this.command.Split(" ");
            foreach (string split in splits)
            {
                if (string.IsNullOrWhiteSpace(split) == false)
                {
                    res.Add(split);
                }
            }

            return res;
        }
        else
        {
            //命令中的单引号数量有错误
            ConsoleHelper.WriteErrorLine(this.command+" 命令中双引号数量有误!");
            return res;
        }



        //

    }

    public void Parse()
    {

         var splits= this.splitCommand();
         if (splits==null||splits.Count==0)
         {
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