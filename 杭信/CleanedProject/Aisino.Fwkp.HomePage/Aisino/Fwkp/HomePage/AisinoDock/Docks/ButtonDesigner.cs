namespace Aisino.Fwkp.HomePage.AisinoDock.Docks
{
    using System.Windows.Forms.Design;

    internal class ButtonDesigner : ControlDesigner
    {
        public override System.Windows.Forms.Design.SelectionRules SelectionRules
        {
            get
            {
                return System.Windows.Forms.Design.SelectionRules.Moveable;
            }
        }
    }
}

