namespace Aisino.Framework.PubData.DataType
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class UserCollection : CollectionBase
    {
        public bool Add(User user)
        {
            if (this.Find(user.UserID) != null)
            {
                base.InnerList.Remove(user);
            }
            base.InnerList.Add(user);
            return true;
        }

        public User Find(string userID)
        {
            foreach (User user in this)
            {
                if (string.Compare(userID, user.UserID, true) == 0)
                {
                    return user;
                }
            }
            return null;
        }

        public void Remove(User user)
        {
            base.InnerList.Remove(user);
        }

        public User this[int index]
        {
            get
            {
                return (User) base.InnerList[index];
            }
        }
    }
}

