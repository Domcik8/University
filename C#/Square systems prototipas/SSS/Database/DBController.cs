using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbController
    {
        public bool IsLegalClient(string username, string password, string carnmb)
        {
            using (var db = new SSSModelContainer())
            {
                return db.UserSet.Contains(new User {CarNmb = carnmb, Password = password, Username = username});
                //var result = from usr in db.UserSet
                //    where usr.Username.Equals(username)
                //    where usr.Password.Equals(password)
                //    where usr.CarNmb.Equals(carnmb)
                //    select usr;
                //if(result.Contains())
            }
        }

        public string GetClientCarNmb(string username)
        {
            using (var db = new SSSModelContainer())
            {
                return db.UserSet.Find(username).CarNmb;
            }
        }
    }
}
