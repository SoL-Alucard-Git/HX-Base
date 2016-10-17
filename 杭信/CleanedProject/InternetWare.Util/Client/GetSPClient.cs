using InternetWare.Lodging.Data;
using Aisino.Framework.Plugin.Core.Controls;

namespace InternetWare.Util.Client
{
    internal class GetSPClient : BaseClient
    {
        private GetSPArgs _args;

        public GetSPClient(GetSPArgs args)
        {
            _args = args;
        }

        internal override BaseResult DoService()
        {
            Aisino.Fwkp.Bmgl.Forms.BMSPSelect selet = new Aisino.Fwkp.Bmgl.Forms.BMSPSelect("", -1.0, 0, "");
            AisinoDataSet dataset = selet.GetSPData("", 10, 1);
            return new GetSPResult(_args, dataset.Data);
        }
    }
}
