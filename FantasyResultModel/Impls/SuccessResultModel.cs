namespace FantasyResultModel.Impls;

public class SuccessResultModel<T>:ResultBase<T>
{
    public SuccessResultModel(int code)
    {
        this.Code = code;
        this.Ok = true;
    }

    public SuccessResultModel()
    {
        this.Ok = true;
    }

    public SuccessResultModel(T data)
    {
        this.Data = data;
        this.Ok=true;
        
    }

    public SuccessResultModel(T data ,int code)
    {
        this.Data = data;
        this.Code = code;
    }
}