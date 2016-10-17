using InternetWare.Lodging.Data;
using Aisino.Fwkp.Bmgl.BLLSys;
using Aisino.Framework.Plugin.Core.Controls;

namespace InternetWare.Util.Client
{
    internal class GetKHClient : BaseClient
    {
        private GetKHArgs _args;

        public GetKHClient(GetKHArgs args)
        {
            _args = args;
        }

        internal override BaseResult DoService()
        {
            Aisino.Fwkp.Bmgl.Forms.BMKHSelect selet = new Aisino.Fwkp.Bmgl.Forms.BMKHSelect();
            AisinoDataSet dataset = selet.GetKHData("", 99999, 1);
            return new GetKHResult(_args, dataset.Data);
        }
    }
}
