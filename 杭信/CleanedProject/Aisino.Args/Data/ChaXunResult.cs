using Newtonsoft.Json;
using System.Data;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ChaXunResult : BaseResult
    {
        public DataTable DataTable { get; set; }

        public ChaXunResult(BaseArgs args, DataTable table, ErrorBase error = null) : base(args, error)
        {
            DataTable = table;
        }

        public ChaXunResult()
        {
        }
    }
}
