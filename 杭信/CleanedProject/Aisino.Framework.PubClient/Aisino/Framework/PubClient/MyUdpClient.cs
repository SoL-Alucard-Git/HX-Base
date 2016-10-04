namespace Aisino.Framework.PubClient
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.PubData.Message_C2S;
    using Aisino.Framework.PubData.Message_S2C;
    using Aisino.Framework.PubData.Tool;
    using log4net;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MyUdpClient
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private DateTime dateTime_0;
        private ILog ilog_0;
        private int int_0;
        private int int_1;
        private IPEndPoint ipendPoint_0;
        private IPEndPoint ipendPoint_1;
        private string string_0;
        private string string_1;
        private Thread thread_0;
        private UdpClient udpClient_0;

        public event LoginOKDelegate LoginOKEvent;

        public event ReceiveMsgDelegate ReceiveMsgEvent;

        public event ReConnectDelegate ReConnectEvent;

        public event StopDelegate StopEvent;

        public MyUdpClient(string string_2, int int_2, int int_3)
        {
            
            this.int_0 = 5;
            this.ilog_0 = LogUtil.GetLogger<MyUdpClient>();
            this.ipendPoint_0 = new IPEndPoint(IPAddress.Parse(string_2), int_2);
            this.int_1 = int_3;
        }

        public void Close(bool bool_4)
        {
            this.bool_2 = true;
            if (this.udpClient_0 != null)
            {
                try
                {
                    if (bool_4 && this.bool_0)
                    {
                        LogoutMessage message = new LogoutMessage(this.string_0, this.string_1);
                        byte[] dgram = SerializeTool.Serialize(message);
                        this.udpClient_0.Send(dgram, dgram.Length, this.ipendPoint_0);
                    }
                    this.bool_0 = false;
                    this.bool_1 = false;
                    this.udpClient_0.Close();
                    this.udpClient_0 = null;
                    Thread.Sleep(0x3e8);
                    this.thread_0 = null;
                }
                catch (Exception exception)
                {
                    this.ilog_0.ErrorFormat("关闭连接异常：{0}", exception.ToString());
                }
            }
        }

        public bool Connect(string string_2, string string_3, string string_4)
        {
            if (this.bool_0)
            {
                return true;
            }
            try
            {
                if (this.int_1 == 0)
                {
                    this.udpClient_0 = new UdpClient();
                }
                else
                {
                    this.udpClient_0 = new UdpClient(this.int_1);
                }
                this.string_0 = string_2;
                this.string_1 = string_3;
                this.bool_2 = false;
                LoginMessage message = new LoginMessage(string_2, string_3, string_4);
                byte[] dgram = SerializeTool.Serialize(message);
                this.udpClient_0.Send(dgram, dgram.Length, this.ipendPoint_0);
                this.dateTime_0 = DateTime.Now;
                if (this.thread_0 == null)
                {
                    this.thread_0 = new Thread(new ThreadStart(this.method_0));
                    this.thread_0.IsBackground = true;
                    this.thread_0.SetApartmentState(ApartmentState.STA);
                }
                if (!this.thread_0.IsAlive)
                {
                    this.thread_0.Start();
                }
                return true;
            }
            catch (Exception exception)
            {
                this.ilog_0.ErrorFormat("连接请求失败：{0}", exception.ToString());
                return false;
            }
        }

        private void method_0()
        {
            while (!this.bool_2)
            {
                if (this.bool_3)
                {
                    break;
                }
                if ((this.udpClient_0 != null) && ((this.udpClient_0.Available > 5) || this.udpClient_0.Client.Connected))
                {
                    byte[] buffer;
                    try
                    {
                        buffer = this.udpClient_0.Receive(ref this.ipendPoint_0);
                    }
                    catch (Exception exception)
                    {
                        this.ilog_0.ErrorFormat("接收数据异常：{0}", exception.ToString());
                        Thread.Sleep(500);
                        continue;
                    }
                    object obj2 = SerializeTool.Deserialize(buffer);
                    Type type = obj2.GetType();
                    if (type == typeof(LoginOKMessage))
                    {
                        LoginOKMessage message = (LoginOKMessage) obj2;
                        this.bool_1 = true;
                        if (message.Code.Equals("0000"))
                        {
                            this.bool_0 = true;
                        }
                        this.ipendPoint_1 = message.EndPoint;
                        this.method_2(message);
                    }
                    else if (type == typeof(HtmlMessage))
                    {
                        HtmlMessage message2 = (HtmlMessage) obj2;
                        ReceiveOKMessage message3 = new ReceiveOKMessage(this.string_0, this.string_1, message2.Id);
                        buffer = SerializeTool.Serialize(message3);
                        this.udpClient_0.Send(buffer, buffer.Length, this.ipendPoint_0);
                        this.method_1(message2);
                    }
                    else if (type == typeof(StopSrvMessage))
                    {
                        this.bool_3 = true;
                        StopSrvMessage message4 = (StopSrvMessage) obj2;
                        this.method_3(message4);
                    }
                    else
                    {
                        this.ilog_0.ErrorFormat("接收到的消息格式错误：{0}", type.ToString());
                    }
                }
                else if (!this.bool_0 && (DateTime.Now.Subtract(this.dateTime_0).TotalSeconds > this.MaxConnWaitTime))
                {
                    this.dateTime_0 = DateTime.Now.AddYears(1);
                    LoginOKMessage message5 = new LoginOKMessage("Z999", new IPEndPoint(IPAddress.Any, 0)) {
                        Mess = string.Concat(new object[] { "登录超时[", this.ipendPoint_0.Address, ":", this.ipendPoint_0.Port, "]" })
                    };
                    this.method_2(message5);
                }
                Thread.Sleep(500);
            }
            bool flag = this.bool_3;
            this.bool_3 = false;
            this.bool_2 = false;
            if (flag && (this.ReConnectEvent != null))
            {
                this.ReConnectEvent();
            }
        }

        private void method_1(object object_0)
        {
            if (this.ReceiveMsgEvent != null)
            {
                this.ReceiveMsgEvent((HtmlMessage) object_0);
            }
        }

        private void method_2(object object_0)
        {
            if (this.LoginOKEvent != null)
            {
                this.LoginOKEvent((LoginOKMessage) object_0);
            }
        }

        private void method_3(object object_0)
        {
            if (this.StopEvent != null)
            {
                this.StopEvent((StopSrvMessage) object_0);
            }
        }

        public bool IsConnect
        {
            get
            {
                return this.bool_1;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.bool_0;
            }
        }

        public int MaxConnWaitTime
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public IPEndPoint ServerPoint
        {
            get
            {
                return this.ipendPoint_0;
            }
        }

        public delegate void LoginOKDelegate(LoginOKMessage mess);

        public delegate void ReceiveMsgDelegate(HtmlMessage mess);

        public delegate void ReConnectDelegate();

        public delegate void StopDelegate(StopSrvMessage mess);
    }
}

