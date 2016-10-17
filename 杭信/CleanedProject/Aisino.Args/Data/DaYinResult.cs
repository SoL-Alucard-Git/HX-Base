using Newtonsoft.Json;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class DaYinResult : BaseResult
    {
        /// <summary>图片需要先转换成Base64字符串再传输 </summary>
        public string ImgBase64Str { get; set; }

        public DaYinResult(BaseArgs args, string imgBase64Str, ErrorBase error = null) : base(args, error)
        {
            ImgBase64Str = imgBase64Str;
        }

        public DaYinResult()
        {
        }
    }
}
