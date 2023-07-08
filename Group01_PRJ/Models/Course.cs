using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class Course
    {
        public Course()
        {
            Sessions = new HashSet<Session>();
            UserClasses = new HashSet<UserClass>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<UserClass> UserClasses { get; set; }
    }
}
