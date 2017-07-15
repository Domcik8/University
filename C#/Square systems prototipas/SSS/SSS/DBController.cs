using System.Linq;

namespace SSS
{
    public static class DbController
    {
        public static bool IsLegalClient(string username, string password)
        {
            username = username.ToLower();
            using (var db = new SSSEntities())
            {
                /*var user = db.UserSet.Find(username);
                return user != null && (user.Password.Equals(password));*/
                if(username == "test" && password == "test")
                {
                    return true;
                }
                return false;
            }
        }

        public static string GetClientCarNmb(string username)
        {
            using (var db = new SSSEntities())
            {
                //return db.UserSet.Find(username).CarNmb;
                return "test";
            }
        }
        public static string GetUsername(string carNmb)
        {
            /*using (var db = new SSSEntities())
            {
                carNmb = carNmb.ToLower();
                var result = from usr in db.UserSet
                             where usr.CarNmb.Equals(carNmb)
                             select usr;
                foreach (var r in result)
                {
                    return r.Username;
                }
                return null;
            }*/
            if (carNmb == "test")
                return "test";
            return null;
        }
        public static bool IsLegalCar(string carNmb)
        {
            carNmb = carNmb.ToLower();
            /*using (var db = new SSSEntities())
            {
                var result = from usr in db.UserSet
                    where usr.CarNmb.Equals(carNmb)
                    select usr;

                return result.Count() == 1;
            }*/
            if (carNmb == "test")
                return true;
            return false;
        }
    }
}
