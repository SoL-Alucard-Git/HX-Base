namespace Aisino.FTaxBase
{
    using System;
    using System.IO;
    using System.Text;

    public class TestIdea
    {
        private string string_0;

        public TestIdea(string string_1)
        {
            
            this.string_0 = "";
            this.string_0 = string_1;
        }

        public int FileCrypto(int int_0, string string_1, string string_2, int int_1)
        {
            byte[] buffer = new byte[int_1];
            byte[] buffer2 = new byte[int_1];
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes("1234567890abcdef");
            this.string_0 = this.string_0.PadRight(0x10, ' ');
            if (int_0 < 3)
            {
                byte[] buffer4 = Encoding.GetEncoding("GBK").GetBytes(this.string_0);
                for (int i = 0; i < 15; i++)
                {
                    bytes[i % 8] = (byte) (bytes[i % 8] ^ buffer4[i]);
                }
            }
            IDEA idea = new IDEA();
            idea.GetEncryptoKey(bytes);
            idea.MakeDecryptoKey();
            using (FileStream stream = File.Open(string_1, FileMode.Open))
            {
                using (FileStream stream2 = new FileStream(string_2, FileMode.Create, FileAccess.Write))
                {
                    for (int j = stream.Read(buffer, 0, int_1); j > 0; j = stream.Read(buffer, 0, int_1))
                    {
                        if ((int_0 % 2) == 0)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                buffer2[m] = (byte) (~buffer[m] ^ bytes[m]);
                            }
                            for (int n = 0; n < 8; n++)
                            {
                                bytes[n + 8] = buffer[n];
                            }
                            idea.GetEncryptoKey(bytes);
                            int num14 = (j - 8) / 8;
                            for (int num10 = 0; num10 < num14; num10++)
                            {
                                if (int_0 < 3)
                                {
                                    for (int num11 = 0; num11 < 8; num11++)
                                    {
                                        buffer[((num10 * 8) + 8) + num11] = (byte) (buffer[((num10 * 8) + 8) + num11] ^ ((byte) ((num10 * 8) + num11)));
                                    }
                                }
                                byte[] buffer7 = CommonTool.GetSubArray(buffer, 8 + (num10 * 8), 8);
                                byte[] buffer8 = CommonTool.GetSubArray(buffer2, 8 + (num10 * 8), 8);
                                idea.Encrypto(buffer7, buffer8);
                                CommonTool.SetInBytes(buffer2, buffer8, 8 + (num10 * 8));
                            }
                        }
                        else
                        {
                            for (int num9 = 0; num9 < 8; num9++)
                            {
                                buffer2[num9] = (byte) ~(buffer[num9] ^ bytes[num9]);
                            }
                            for (int num13 = 0; num13 < 8; num13++)
                            {
                                bytes[num13 + 8] = buffer2[num13];
                            }
                            idea.GetEncryptoKey(bytes);
                            idea.MakeDecryptoKey();
                            int num15 = (j - 8) / 8;
                            for (int num12 = 0; num12 < num15; num12++)
                            {
                                byte[] buffer5 = CommonTool.GetSubArray(buffer, 8 + (num12 * 8), 8);
                                byte[] buffer6 = new byte[8];
                                idea.Decrypto(buffer5, buffer6);
                                CommonTool.SetInBytes(buffer2, buffer6, 8 + (num12 * 8));
                                if (int_0 < 3)
                                {
                                    for (int num6 = 0; num6 < 8; num6++)
                                    {
                                        buffer2[((num12 * 8) + 8) + num6] = (byte) (buffer2[((num12 * 8) + 8) + num6] ^ ((byte) ((num12 * 8) + num6)));
                                    }
                                }
                            }
                        }
                        int num7 = (j / 8) * 8;
                        for (int k = num7; k < j; k++)
                        {
                            buffer2[k] = buffer[k];
                        }
                        stream2.Write(buffer2, 0, j);
                    }
                }
            }
            return 0;
        }
    }
}

