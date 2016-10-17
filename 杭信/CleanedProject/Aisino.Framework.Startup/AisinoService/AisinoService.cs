using InternetWare.Lodging.Data;
using System;
using System.IO;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Text;

// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“AisinoService”。
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class AisinoService : IAisinoService
{
    public string ChaXunMethod(string chaxunargs)
    {
        string result = DataService.DoService(chaxunargs, "ChaXunArgs");
        return result;
    }
    public string DaYinMethod(string dyargs)
    {
        dyargs = Encoding.UTF8.GetString(Convert.FromBase64String(dyargs));
        string result= DataService.DoService(dyargs, "InternetWare.Lodging.Data.DaYinArgs");
        return result;
    }

    public void DoWork()
    {
    }

    public string ZFChaXunMethod(string chaxunargs)
    {
        string result = DataService.DoService(chaxunargs, "ZuoFeiChaXunArgs");
        return result;
    }

    public string ZFMethod(string zfargs)
    {
        string result = DataService.DoService(zfargs, "ZuoFeiArgs");
        return result;
    }
}
