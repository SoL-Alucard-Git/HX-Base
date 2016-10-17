using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ErrorBase
    {
        public ErrorBase()
        {
            HasError = false;
            ErrorDescription = string.Empty;
        }

        public ErrorBase(bool hasError = false)
        {
            HasError = hasError;
            ErrorDescription = string.Empty;
        }

        public ErrorBase(string ErrorMsg)
        {
            HasError = true;
            ErrorDescription = ErrorMsg;
        }

        public ErrorBase(bool hasError, string errorDescription)
        {
            HasError = hasError;
            ErrorDescription = errorDescription;
        }

        public bool HasError { get; set; } = false;
        public string ErrorDescription { get; set; } = string.Empty;
    }
}
