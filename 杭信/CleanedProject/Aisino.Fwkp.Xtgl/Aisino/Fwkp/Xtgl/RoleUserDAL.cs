namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RoleUserDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<RoleUserDAL>();

        public void DeleteRole(string roleId)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", roleId);
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteRole", dictionary);
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteRoleFuncByJSDM", dictionary);
                this.baseDAO.Commit();
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-131402");
                this.loger.Error(exception.Message, exception);
            }
        }

        public void DeleteUser(string userCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", userCode);
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteUser", dictionary);
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteUserRoleByYHDM", dictionary);
                this.baseDAO.Commit();
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-132402");
                this.loger.Error(exception.Message, exception);
            }
        }

        public bool ExistRoleName(string name)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("MC", name);
                if (this.baseDAO.queryValueSQL<int>("Aisino.Fwkp.Xtgl.ExistRoleName", dictionary) == 1)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
                throw;
            }
            return false;
        }

        public bool ExistUserName(string name)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("ZSXM", name);
                if (this.baseDAO.queryValueSQL<int>("Aisino.Fwkp.Xtgl.ExistUserName", dictionary) == 1)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132301");
                this.loger.Error(exception.Message, exception);
            }
            return false;
        }

        public void InsertRole(Role role)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", role.ID);
            dictionary.Add("MC", role.Name);
            dictionary.Add("BZ", role.Description);
            dictionary.Add("CJSJ", role.CreateDate);
            dictionary.Add("CJYH", role.CreatorName);
            string funcID = "";
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertRole", dictionary);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (PermFunc func in role.PermFuncList)
                {
                    funcID = func.FuncID;
                    dictionary2.Clear();
                    dictionary2.Add("JSXX_DM", role.ID);
                    dictionary2.Add("GNXX_DM", func.FuncID);
                    dictionary2.Add("CJSJ", func.CreateDate);
                    dictionary2.Add("CJYH", func.CreatorName);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertRoleFunc", dictionary2);
                }
                this.baseDAO.Commit();
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-131102");
                this.loger.Error(exception.Message + funcID, exception);
            }
        }

        public void InsertUser(User user)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", user.Code);
            dictionary.Add("MM", user.Password);
            dictionary.Add("ZSXM", user.RealName);
            dictionary.Add("LXDH", user.Telephone);
            dictionary.Add("BZ", user.Description);
            dictionary.Add("ISADMIN", user.IsAdmin);
            dictionary.Add("CJSJ", user.CreateDate);
            dictionary.Add("CJYH", user.CreatorName);
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertUser", dictionary);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (Role role in user.RoleList)
                {
                    dictionary2.Clear();
                    dictionary2.Add("YHXX_DM", user.Code);
                    dictionary2.Add("JSXX_DM", role.ID);
                    dictionary2.Add("CJSJ", DateTime.Now);
                    dictionary2.Add("CJYH", user.CreatorName);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertUserRole", dictionary2);
                }
                this.baseDAO.Commit();
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-132104");
                this.loger.Error(exception.Message, exception);
            }
        }

        public bool IsExistUsrBelongToRole(string roleCode)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("JSXX_DM", roleCode);
            try
            {
                DataTable table = this.baseDAO.querySQLDataTable("Aisino.Fwkp.Xtgl.QueryUsersBelongRole", dictionary);
                return ((table != null) && (table.Rows.Count > 0));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public List<Role> SelectAllRoleInfo(string name)
        {
            List<Role> list = new List<Role>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("CJYH", name);
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectRole", dictionary))
                {
                    Dictionary<string, object> dictionary2 = obj2 as Dictionary<string, object>;
                    if (dictionary2 != null)
                    {
                        Role item = new Role {
                            ID = dictionary2["DM"].ToString(),
                            Name = dictionary2["MC"].ToString(),
                            Description = dictionary2["BZ"].ToString(),
                            CreatorName = dictionary2["CJYH"].ToString(),
                            CreateDate = Convert.ToDateTime(dictionary2["CJSJ"].ToString())
                        };
                        list.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-131301");
                this.loger.Error(exception.Message, exception);
            }
            return list;
        }

        public List<string> SelectAllUserNames()
        {
            List<string> list = new List<string>();
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectAllUser", null))
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    if (dictionary != null)
                    {
                        list.Add(dictionary["ZSXM"].ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132301");
                this.loger.Error(exception.Message, exception);
            }
            return list;
        }

        public Dictionary<string, List<PermFunc>> SelectRoleFuncs()
        {
            Dictionary<string, List<PermFunc>> dictionary = new Dictionary<string, List<PermFunc>>();
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectRoleFuncs", null))
                {
                    Dictionary<string, object> dictionary2 = obj2 as Dictionary<string, object>;
                    if (dictionary2 != null)
                    {
                        string key = dictionary2["JSXX_DM"].ToString();
                        PermFunc item = new PermFunc(dictionary2["GNXX_DM"].ToString());
                        if (!dictionary.ContainsKey(key))
                        {
                            dictionary.Add(key, new List<PermFunc>());
                        }
                        dictionary[key].Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-131301");
                this.loger.Error(exception.Message, exception);
            }
            return dictionary;
        }

        public List<Role> SelectRoles(string name)
        {
            List<Role> list = new List<Role>();
            Dictionary<string, List<PermFunc>> dictionary = this.SelectRoleFuncs();
            try
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("CJYH", name);
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectRole", dictionary2))
                {
                    Dictionary<string, object> dictionary3 = obj2 as Dictionary<string, object>;
                    if (dictionary3 != null)
                    {
                        Role item = new Role {
                            ID = dictionary3["DM"].ToString(),
                            Name = dictionary3["MC"].ToString(),
                            Description = dictionary3["BZ"].ToString(),
                            CreatorName = dictionary3["CJYH"].ToString(),
                            CreateDate = Convert.ToDateTime(dictionary3["CJSJ"].ToString())
                        };
                        if ((item.ID != null) && dictionary.ContainsKey(item.ID))
                        {
                            item.PermFuncList.AddRange(dictionary[item.ID]);
                        }
                        list.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-131301");
                this.loger.Error(exception.Message, exception);
            }
            return list;
        }

        public User SelectUserByDM(string DM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", DM);
            DataTable table = this.baseDAO.querySQLDataTable("Aisino.Fwkp.Xtgl.SelectUserByDM", dictionary);
            if (table.Rows.Count == 1)
            {
                return new User { Code = table.Rows[0]["DM"].ToString(), Password = table.Rows[0]["MM"].ToString(), RealName = table.Rows[0]["ZSXM"].ToString(), Telephone = table.Rows[0]["LXDH"].ToString(), IsAdmin = Convert.ToBoolean(table.Rows[0]["ISADMIN"]), Description = table.Rows[0]["BZ"].ToString(), CreatorName = table.Rows[0]["CJYH"].ToString(), CreateDate = Convert.ToDateTime(table.Rows[0]["CJSJ"].ToString()) };
            }
            return null;
        }

        public string SelectUserPwd(string userName)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("ZSXM", userName);
                return this.baseDAO.queryValueSQL<string>("Aisino.Fwkp.Xtgl.SelectPwdByName", dictionary);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132301");
                this.loger.Error(exception.Message, exception);
                return null;
            }
        }

        public Dictionary<string, List<Role>> SelectUserRoles()
        {
            Dictionary<string, List<Role>> dictionary = new Dictionary<string, List<Role>>();
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectUserRoles", null))
                {
                    Dictionary<string, object> dictionary2 = obj2 as Dictionary<string, object>;
                    if (dictionary2 != null)
                    {
                        string key = dictionary2["YHXX_DM"].ToString();
                        string id = dictionary2["JSXX_DM"].ToString();
                        string name = dictionary2["MC"].ToString();
                        Role item = new Role(id, name) {
                            Description = dictionary2["BZ"].ToString(),
                            CreatorName = dictionary2["CJYH"].ToString(),
                            CreateDate = Convert.ToDateTime(dictionary2["CJSJ"].ToString())
                        };
                        if (!dictionary.ContainsKey(key))
                        {
                            dictionary.Add(key, new List<Role>());
                        }
                        dictionary[key].Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132301");
                this.loger.Error(exception.Message, exception);
            }
            return dictionary;
        }

        public List<User> SelectUsers(string name)
        {
            List<User> list = new List<User>();
            Dictionary<string, List<Role>> dictionary = this.SelectUserRoles();
            try
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("CJYH", name);
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectUser", dictionary2))
                {
                    Dictionary<string, object> dictionary3 = obj2 as Dictionary<string, object>;
                    if (dictionary3 != null)
                    {
                        User item = new User {
                            Code = dictionary3["DM"].ToString(),
                            Password = dictionary3["MM"].ToString(),
                            RealName = dictionary3["ZSXM"].ToString(),
                            Telephone = dictionary3["LXDH"].ToString(),
                            IsAdmin = Convert.ToBoolean(dictionary3["ISADMIN"]),
                            Description = dictionary3["BZ"].ToString(),
                            CreatorName = dictionary3["CJYH"].ToString(),
                            CreateDate = Convert.ToDateTime(dictionary3["CJSJ"].ToString())
                        };
                        if ((item.Code != null) && dictionary.ContainsKey(item.Code))
                        {
                            item.RoleList.AddRange(dictionary[item.Code]);
                        }
                        list.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132301");
                this.loger.Error(exception.Message, exception);
            }
            return list;
        }

        public void UpdateRole(Role role)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", role.ID);
            dictionary.Add("MC", role.Name);
            dictionary.Add("BZ", role.Description);
            string funcID = "";
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateRole", dictionary);
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteRoleFuncByJSDM", dictionary);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (PermFunc func in role.PermFuncList)
                {
                    funcID = func.FuncID;
                    dictionary2.Clear();
                    dictionary2.Add("JSXX_DM", role.ID);
                    dictionary2.Add("GNXX_DM", func.FuncID);
                    dictionary2.Add("CJSJ", func.CreateDate);
                    dictionary2.Add("CJYH", func.CreatorName);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertRoleFunc", dictionary2);
                }
                this.baseDAO.Commit();
                MessageManager.ShowMsgBox("INP-131203");
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-131202");
                this.loger.Error(exception.Message + funcID, exception);
            }
        }

        public void UpdateRoleBase(Role role)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", role.ID);
            dictionary.Add("MC", role.Name);
            dictionary.Add("BZ", role.Description);
            try
            {
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateRole", dictionary) > 0)
                {
                    MessageManager.ShowMsgBox("INP-131203");
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-131202");
                this.loger.Error(exception.Message, exception);
            }
        }

        public void UpdateRoleFuncs(Role role)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", role.ID);
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteRoleFuncByJSDM", dictionary);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (PermFunc func in role.PermFuncList)
                {
                    dictionary2.Clear();
                    dictionary2.Add("JSXX_DM", role.ID);
                    dictionary2.Add("GNXX_DM", func.FuncID);
                    dictionary2.Add("CJSJ", func.CreateDate);
                    dictionary2.Add("CJYH", func.CreatorName);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertRoleFunc", dictionary2);
                }
                this.baseDAO.Commit();
                MessageManager.ShowMsgBox("INP-131203");
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-131202");
                this.loger.Error(exception.Message, exception);
            }
        }

        public void UpdateUser(User user)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", user.Code);
            dictionary.Add("ZSXM", user.RealName);
            dictionary.Add("LXDH", user.Telephone);
            dictionary.Add("BZ", user.Description);
            dictionary.Add("ISADMIN", user.IsAdmin);
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateUser", dictionary);
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteUserRoleByYHDM", dictionary);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (Role role in user.RoleList)
                {
                    dictionary2.Clear();
                    dictionary2.Add("YHXX_DM", user.Code);
                    dictionary2.Add("JSXX_DM", role.ID);
                    dictionary2.Add("CJSJ", DateTime.Now);
                    dictionary2.Add("CJYH", user.CreatorName);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertUserRole", dictionary2);
                }
                this.baseDAO.Commit();
                MessageManager.ShowMsgBox("INP-131203");
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-132205");
                this.loger.Error(exception.Message, exception);
            }
        }

        public void UpdateUserBase(User user)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", user.Code);
            dictionary.Add("ZSXM", user.RealName);
            dictionary.Add("LXDH", user.Telephone);
            dictionary.Add("BZ", user.Description);
            dictionary.Add("ISADMIN", user.IsAdmin);
            try
            {
                if (this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateUser", dictionary) > 0)
                {
                    MessageManager.ShowMsgBox("INP-131203");
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132205");
                this.loger.Error(exception.Message, exception);
            }
        }

        public int UpdateUserNamePwd(string userCode, string userName, string password)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", userCode);
            dictionary.Add("ZSXM", userName);
            dictionary.Add("MM", password);
            try
            {
                return this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateUserNamePwd", dictionary);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132205");
                this.loger.Error(exception.Message, exception);
                return -1;
            }
        }

        public int UpdateUserPwd(string userCode, string password)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", userCode);
            dictionary.Add("MM", password);
            try
            {
                return this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateUserPwd", dictionary);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-132202");
                this.loger.Error(exception.Message, exception);
                return -1;
            }
        }

        public void UpdateUserRole(User user)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", user.Code);
            try
            {
                this.baseDAO.Open();
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.DeleteUserRoleByYHDM", dictionary);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (Role role in user.RoleList)
                {
                    dictionary2.Clear();
                    dictionary2.Add("YHXX_DM", user.Code);
                    dictionary2.Add("JSXX_DM", role.ID);
                    dictionary2.Add("CJSJ", DateTime.Now);
                    dictionary2.Add("CJYH", user.CreatorName);
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.InsertUserRole", dictionary2);
                }
                this.baseDAO.Commit();
                MessageManager.ShowMsgBox("INP-131203");
            }
            catch (Exception exception)
            {
                this.baseDAO.RollBack();
                MessageManager.ShowMsgBox("INP-132205");
                this.loger.Error(exception.Message, exception);
            }
        }
    }
}

