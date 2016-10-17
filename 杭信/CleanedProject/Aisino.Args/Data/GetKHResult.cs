using Newtonsoft.Json;
using System.Data;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class GetKHResult : BaseResult
    {
        public DataTable DataTable { get; private set; }

        public GetKHResult(BaseArgs args, DataTable dataTable, ErrorBase error = null) : base(args, error)
        {
            DataTable = dataTable;
        }
    }
}
