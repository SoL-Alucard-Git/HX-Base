using Newtonsoft.Json;
using System.Collections.Generic;

namespace InternetWare.Lodging.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TianKaiQueRenResult : BaseResult
    {
        public List<TianKaiQueRenResultEntity> EntityList { get; set; }

        public TianKaiQueRenResult(BaseArgs args, List<TianKaiQueRenResultEntity> list, ErrorBase error = null) : base(args, error)
        {
            EntityList = list;
        }

        public TianKaiQueRenResult()
        {
        }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class TianKaiQueRenResultEntity
    {
        public string ReceiptType { get; set; }
        public string TypeCode { get; set; }
        public string StartCode { get; set; }
        public int ReceiptCount { get; set; }

        public TianKaiQueRenResultEntity(string receiptType, string typeCode, string startCode, int receiptCount)
        {
            ReceiptType = receiptType;
            TypeCode = typeCode;
            StartCode = startCode;
            ReceiptCount = receiptCount;
        }
    }
}
