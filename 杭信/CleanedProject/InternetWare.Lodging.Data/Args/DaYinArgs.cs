namespace InternetWare.Lodging.Data
{
    public class DaYinArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.DaYin;
            }
        }

        public string FPZL { get; set; }
        public string FPDM { get; set; }
        public int FPHM { get; set; }
        public string Printer { get; set; }
    }
}
