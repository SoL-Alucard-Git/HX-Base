namespace InternetWare.Lodging.Data
{
    public class ErrorBase
    {

        public ErrorBase() {; }

        public ErrorBase(string errorDescription)
        {
            ErrorDescription = errorDescription;
        }
        public string ErrorDescription { get; set; } = string.Empty;
    }
}
