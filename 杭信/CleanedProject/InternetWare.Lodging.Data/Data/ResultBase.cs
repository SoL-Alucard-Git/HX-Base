using InternetWare.Lodging.Data;

namespace InternetWare.Lodging.Data
{
    public class ResultBase
    {
        public BaseArgs Args { get; private set; }
        public object Data { get; private set; }
        public bool HasError { get; private set; } = false;
        public ErrorBase ErrorInfo { get; private set; }

        public ResultBase()
        {
        }

        public ResultBase(BaseArgs args,object data,bool hasError,ErrorBase error = null)
        {
            this.Args = args;
            this.Data = data;
            this.HasError = hasError;
            this.ErrorInfo = error;
        }
    }
}
