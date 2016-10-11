namespace InternetWare.Lodging.Data
{
    public class CountableResult :  ResultBase
    {
        public int Total { get; private set;}
        public int Succeed { get; private set; }
        public int Failed { get; private set; }

        public CountableResult(BaseArgs args, object data, bool hasError, ErrorBase error,int total,int succeed,int failed) : base(args, data, hasError, error)
        {
            Total = total;
            Succeed = succeed;
            Failed = failed;
        }
    }
}
