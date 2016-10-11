using System.Collections.Generic;
using System.Windows.Forms;

namespace InternetWare.Lodging.Data
{
    public class ZuoFeiArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.ZuoFei;
            }
        }

        public List<DataGridViewRow> list = new List<DataGridViewRow>();

    }
}
