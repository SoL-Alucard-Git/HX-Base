using InternetWare.Lodging.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetWare.Form
{
    public partial class LodgingWindow
    {
        private void HZByer_ydk_Click(object sender, EventArgs e)
        {
            HZTKArgs args = new HZTKArgs();
            args.hztype = HZType.GFYDK;
            DataService.DoService(args);
        }

        private void HZByer_wdk_Click(object sender, EventArgs e)
        {
            HZTKArgs args = new HZTKArgs();
            args.hztype = HZType.GFWDK;
            DataService.DoService(args);
        }
        
        private void HZSells_Click(object sender, EventArgs e)
        {
            HZTKArgs args = new HZTKArgs();
            args.hztype = HZType.XF;
            DataService.DoService(args);
        }
    }

}
