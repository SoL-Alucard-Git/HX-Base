namespace ns0
{
    using Aisino.Framework.PubClient;
    using Aisino.Framework.PubData.Message_S2C;
    using System;
    using Aisino.Framework.PubData.Message_S2C;
    internal class Class1
    {
        private static MyUdpClient myUdpClient_0;

        public Class1()
        {
            
        }

        public static void smethod_0()
        {
            myUdpClient_0 = new MyUdpClient("192.168.5.126", 0x8f0, 0xe10);
            myUdpClient_0.LoginOKEvent += new MyUdpClient.LoginOKDelegate(Class1.smethod_2);
            myUdpClient_0.ReceiveMsgEvent += new MyUdpClient.ReceiveMsgDelegate(Class1.smethod_3);
            myUdpClient_0.StopEvent += new MyUdpClient.StopDelegate(Class1.smethod_4);
            myUdpClient_0.Connect("myname2", "1234", "0");
            Console.ReadLine();
            myUdpClient_0.Close(true);
        }

        public static void smethod_1(CommonMessage commonMessage_0)
        {
            Console.WriteLine("网络异常:");
            Console.WriteLine("CODE:{0},MESS:{1}", commonMessage_0.Code, commonMessage_0.Mess);
        }

        public static void smethod_2(CommonMessage commonMessage_0)
        {
            Console.WriteLine("LOGIN:");
            Console.WriteLine("CODE:{0},MESS:{1}", commonMessage_0.Code, commonMessage_0.Mess);
        }

        public static void smethod_3(HtmlMessage htmlMessage_0)
        {
            Console.WriteLine("MESSAGE:");
            Console.WriteLine("MESS:{0}", htmlMessage_0.Message);
        }

        public static void smethod_4(object object_0)
        {
            Console.WriteLine("服务器停止");
            Environment.Exit(0);
        }
    }
}

