namespace Aisino.Fwkp.Publish.Entry
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.PubClient;
    using Aisino.Framework.PubData.Message_S2C;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Publish.BLL;
    using Aisino.Fwkp.Publish.Forms;
    using log4net;
    using System;
    using System.Threading;
    using Framework.Plugin.Core;
    public class PubClient
    {
        private MyUdpClient client;
        public bool hasExit;
        private static PubClient instance;
        private ILog log = LogUtil.GetLogger<PubClient>();
        internal int retrySec = 10;
        public static string status = "未连接";
        private static TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private string userID = (taxCard.TaxCode + "." + taxCard.Machine.ToString());
        private string userName = taxCard.Corporation;

        public void AutoRun()
        {
            if (Config.connOnLoad.Equals("1"))
            {
                this.ConnectServer();
            }
        }

        public void CloseClient()
        {
            if (this.client != null)
            {
                this.client.Close(this.client.IsConnected);
                this.client = null;
            }
            this.hasExit = true;
        }

        internal void ConnectServer()
        {
            try
            {
                if (((this.client == null) || !this.client.ServerPoint.Address.ToString().Equals(Config.serverIP)) || (this.client.ServerPoint.Port != int.Parse(Config.serverPort)))
                {
                    this.client = new MyUdpClient(Config.serverIP, int.Parse(Config.serverPort), int.Parse(Config.clientPort));
                    this.client.MaxConnWaitTime = int.Parse(Config.maxConnWaitTime);
                    this.client.LoginOKEvent += Login;
                    this.client.ReceiveMsgEvent += getMess;
                    this.client.StopEvent += stopMess;
                    this.client.ReConnectEvent += ReConnectServer;
                }
                if (!this.client.IsConnected)
                {
                    this.client.Connect(this.userName, this.userID + "." + MD5_Crypt.GetHashString32(this.userID + "saxiTAO"), Config.maxMessID);
                }
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("登录公告服务器异常：{0}", exception.ToString());
            }
        }

        private void getMess(HtmlMessage mess)
        {
            try
            {
                IPubManager manager = new PubManager();
                if (manager.SavePub(mess))
                {
                    string id = mess.Id;
                    if (id.PadLeft(0x19, '0').CompareTo(Config.maxMessID.PadLeft(0x19, '0')) > 0)
                    {
                        Config.maxMessID = id;
                        PropertyUtil.SetValue("PUB_MAX_MESS_ID", id);
                    }
                }
                new MessageForm(this, mess).ShowDialog();
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("接收公告信息异常：{0}", exception.ToString());
            }
        }

        private void Login(LoginOKMessage mess)
        {
            try
            {
                if (!mess.Code.Equals("0000"))
                {
                    status = string.Format("[{0}]连接公告服务器失败({1},{2})", DateTime.Now.ToLocalTime(), mess.Code, mess.Mess);
                    Thread.Sleep(0x3e8);
                    this.ReConnectServer();
                }
                else
                {
                    status = string.Format("[{0}]连接公告服务器成功({1}:{2})", DateTime.Now.ToLocalTime(), this.client.ServerPoint.Address.ToString(), this.client.ServerPoint.Port);
                    HtmlMessage message = new HtmlMessage("000000", "已连接到公告服务器！", "", "", 3);
                    new MessageForm(this, message).ShowDialog();
                }
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("接收登录反馈信息异常：{0}", exception.ToString());
            }
        }

        private void ReConnectServer()
        {
            if ((this.client == null) || !this.client.IsConnected)
            {
                this.ConnectServer();
            }
        }

        private void stopMess(StopSrvMessage mess)
        {
            try
            {
                status = string.Format("[{0}]公告服务器停止({1}:{2})", DateTime.Now.ToLocalTime(), this.client.ServerPoint.Address.ToString(), this.client.ServerPoint.Port);
                if (this.client != null)
                {
                    this.client.Close(false);
                }
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("接收服务器停止信息异常：{0}", exception.ToString());
            }
        }

        public static PubClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PubClient();
                }
                return instance;
            }
        }

        public bool PubCLientConnected
        {
            get
            {
                return ((this.client != null) && this.client.IsConnected);
            }
        }
    }
}

