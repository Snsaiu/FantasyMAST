namespace TransformInterface.Models;

using TransformInterface.Enums;

public class SendDataModel
{
    public SendDataModel(SendType sendType,DataType dataType,string? content)
    {
        this.SendType = sendType;
        this.DataType = dataType;
        this.Content = content;
    }

    public SendType SendType { get; set; }

    public DataType DataType { get; set; } = DataType.Other;

    public string? Content { get; set; }
}