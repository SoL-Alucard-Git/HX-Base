using InternetWare.Lodging.Data;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace InternetWare.Form
{
    public partial class LodgingWindow : System.Windows.Forms.Form
    {
        public LodgingWindow()
        {
            InitializeComponent();
        }

        private void LodgingWindow_Load(object sender, EventArgs e)
        {
            Init_ChaXunPage();
            Init_DaYinPage();
            InitWeiKaiPage();
        }

        public string Base64Encode(string jsonparam)
        {
            jsonparam = Encoding.UTF8.GetString(Convert.FromBase64String(jsonparam));
            return jsonparam;
        }

        public BaseResult GetData(string Data)
        {
            string serviceUrl = "http://localhost:8099/AisinoService/DaYinMethod";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";

            //转成网络流
            byte[] buf = UnicodeEncoding.UTF8.GetBytes(Data);
            //byte[] buf = System.Text.Encoding.UTF8.GetBytes("{\"value\":10");
            //设置

            request.ContentLength = buf.Length;

            // 发送请求
            Stream newStream = request.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            // 获得接口返回值
            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            string ReturnXml = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            BaseResult rb = jsonSerializer.Deserialize<BaseResult>(ReturnXml);
            return rb;
        }

    }    
}
