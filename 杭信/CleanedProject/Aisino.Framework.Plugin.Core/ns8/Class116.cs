namespace ns8
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class Class116
    {
        private DataTable dataTable_0;
        private Dictionary<char, List<Class117>> dictionary_0;

        public Class116(DataTable dataTable_1)
        {
            
            this.dictionary_0 = new Dictionary<char, List<Class117>>();
            this.dataTable_0 = dataTable_1;
            this.method_0();
        }

        public void method_0()
        {
            if (this.dataTable_0 != null)
            {
                for (int i = 0; i < this.dataTable_0.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dataTable_0.Columns.Count; j++)
                    {
                        string str = this.dataTable_0.Rows[i][j].ToString();
                        for (int k = 0; k < str.Length; k++)
                        {
                            char key = str[k];
                            if (key != ' ')
                            {
                                Class117 item = new Class117 {
                                    int_2 = k,
                                    int_1 = j,
                                    int_0 = i,
                                    float_0 = 0.1f
                                };
                                if (this.dictionary_0.ContainsKey(key))
                                {
                                    this.dictionary_0[key].Add(item);
                                }
                                else
                                {
                                    List<Class117> list = new List<Class117> {
                                        item
                                    };
                                    this.dictionary_0.Add(key, list);
                                }
                            }
                        }
                    }
                }
            }
        }

        public DataTable method_1(string string_0)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                return this.dataTable_0;
            }
            Dictionary<int, Class118> dictionary = new Dictionary<int, Class118>();
            foreach (char ch in string_0)
            {
                if ((ch != ' ') && this.dictionary_0.ContainsKey(ch))
                {
                    foreach (Class117 class5 in this.dictionary_0[ch])
                    {
                        if (dictionary.ContainsKey(class5.int_0))
                        {
                            dictionary[class5.int_0].list_0.Add(class5);
                        }
                        else
                        {
                            Class118 class7 = new Class118 {
                                int_0 = class5.int_0,
                                float_0 = 0f
                            };
                            class7.list_0.Add(class5);
                            dictionary.Add(class5.int_0, class7);
                        }
                    }
                }
            }
            List<Class118> list = new List<Class118>();
            foreach (KeyValuePair<int, Class118> pair in dictionary)
            {
                Class118 item = pair.Value;
                float num = 0f;
                for (int i = 0; i < item.list_0.Count; i++)
                {
                    num += item.list_0[i].float_0;
                    for (int j = i; j < item.list_0.Count; j++)
                    {
                        Class117 class3 = item.list_0[i];
                        Class117 class4 = item.list_0[j];
                        if ((class3.int_0 == class4.int_0) && (class3.int_1 == class4.int_1))
                        {
                            num += (float) Math.Pow(0.7, (double) Math.Abs((int) (class3.int_2 - class4.int_2)));
                        }
                    }
                    item.float_0 = num;
                }
                if (list.Count == 0)
                {
                    list.Add(item);
                    continue;
                }
                bool flag = true;
                int index = 0;
                while (index < list.Count)
                {
                    if (list[index].float_0 < item.float_0)
                    {
                        goto Label_0231;
                    }
                    index++;
                }
                goto Label_023E;
            Label_0231:
                list.Insert(index, item);
                flag = false;
            Label_023E:
                if (flag)
                {
                    list.Add(item);
                }
            }
            DataTable table = new DataTable();
            table = this.dataTable_0.Copy();
            table.Clear();
            foreach (Class118 class8 in list)
            {
                DataRow row = this.dataTable_0.Rows[class8.int_0];
                table.ImportRow(row);
            }
            return table;
        }

        public class Class117
        {
            public float float_0;
            public int int_0;
            public int int_1;
            public int int_2;

            public Class117()
            {
                
            }
        }

        public class Class118
        {
            public float float_0;
            public int int_0;
            public List<Class116.Class117> list_0;

            public Class118()
            {
                
                this.list_0 = new List<Class116.Class117>();
            }
        }
    }
}

