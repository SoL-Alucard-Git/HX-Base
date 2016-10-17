using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class BaseResult
    {
        public BaseArgs Args { get; set; }
        public ErrorBase ErrorInfo { get; set; }

        public BaseResult()
        {
            this.Args = new BaseArgs();
            this.ErrorInfo = new ErrorBase();
        }

        public BaseResult(BaseArgs args, ErrorBase error = null)
        {
            this.Args = args;
            this.ErrorInfo = error == null ? new ErrorBase() : error;
        }
    }
}
