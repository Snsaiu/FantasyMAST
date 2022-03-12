namespace FantasyMASTApplication;

using FantasyMASTApplication.Models;
using FantasyResultModel;
using FantasyResultModel.Impls;

using JsonMASTConfig;

/// <summary>
/// 用户服务
/// </summary>
public class UserApplication
{

    private BootstrapConfig bootstrapConfig = null;

    private string filepath = "";

    public UserApplication()
    {
        this.bootstrapConfig = new BootstrapConfig();
        this.filepath = this.bootstrapConfig.LoadConfigFile().Data;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ResultBase<bool> Login(string name)
    {
        try
        {
            JsonConfig jc = new JsonConfig(this.filepath, name,true);
   
            return new SuccessResultModel<bool>(true);
        }
        catch (Exception e)
        {
            return new ErrorResultModel<bool>(e.Message);

        }
     
    }

    
    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    public ResultBase<bool> Logout()
    {
        try
        {
            JsonConfig jc = new JsonConfig(this.filepath);
            bool unregist_res = jc.Unregist();
            if (unregist_res)
            {
                return new SuccessResultModel<bool>(true);
            }

            return new ErrorResultModel<bool>("登出失败！");
        }
        catch (Exception e)
        {
            return new ErrorResultModel<bool>(e.Message);
        }

    }

    /// <summary>
    /// 获得用户信息
    /// </summary>
    /// <returns></returns>
    public ResultBase<string> GetUserInfo()
    {
        JsonConfig jc = new JsonConfig(this.filepath);
        if (string.IsNullOrWhiteSpace(jc.UserName))
        {
            return new ErrorResultModel<string>("用户不存在用户名!");
        }

        return new SuccessResultModel<string>(jc.UserName);
    }


}