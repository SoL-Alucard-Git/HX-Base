namespace InternetWare.Lodging.Data
{
    public class WeiKaiChaXunArgs : BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.WeiKaiChaXun;
            }
        }

        /// <summary>发票类型</summary>
        public FaPiaoTypes FpType { get; set; }
    }
}
