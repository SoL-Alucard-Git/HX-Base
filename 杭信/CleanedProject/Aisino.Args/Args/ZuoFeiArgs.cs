using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InternetWare.Lodging.Data
{

    [JsonObject(MemberSerialization.OptOut)]
    public class ZuoFeiArgs : BaseArgs
    {
        [JsonIgnore]
        public override ArgsType Type
        {
            get
            {
                return ArgsType.ZuoFei;
            }
        }

        public List<DataGridViewRow> DataRowList = new List<DataGridViewRow>();
    }
}
