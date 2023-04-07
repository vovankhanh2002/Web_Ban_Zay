using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class mapUser
    {
        public List<User> User()
        {
            Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
            var ds = db.Users.ToList();
            return ds;
        }
    }
}