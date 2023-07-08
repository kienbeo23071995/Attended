using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class Session
    {
        public Session()
        {
            Attendeds = new HashSet<Attended>();
        }

        public int Roomid { get; set; }
        public int Slotid { get; set; }
        public DateTime Date { get; set; }
        public int Courseid { get; set; }
        public int Userid { get; set; }
        public int Classid { get; set; }

        public virtual Class Class { get; set; }
        public virtual Course Course { get; set; }
        public virtual Room Room { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Attended> Attendeds { get; set; }
        
        public bool checkAttend(int userId)
        {
            foreach(Attended at in Attendeds)
            {
                if(at.Userid == userId && at.Status == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckStudent(int userId)
        {
            foreach (UserClass userClass in Class.UserClasses)
            {
                if (userClass.Userid == userId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
