namespace FantasyResultModel.Impls;

public class ErrorResultModel<T>:ResultBase<T>
{

    public ErrorResultModel(string errorMessage,int code)
    {
        this.ErrorMsg=errorMessage;
        this.Ok = false;
        this.Code=code;
    }

    public ErrorResultModel(string? errorMessage)
    {
        this.ErrorMsg = errorMessage;
        this.Ok = false;
       
    }
}