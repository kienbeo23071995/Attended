using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class User
    {
        public User()
        {
            Attendeds = new HashSet<Attended>();
            Sessions = new HashSet<Session>();
            UserClasses = new HashSet<UserClass>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public bool Gender { get; set; }
        public bool IsSuper { get; set; }

        public virtual ICollection<Attended> Attendeds { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<UserClass> UserClasses { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public bool CheckGroup(string? group)
        {
            if(group == null) return false;
            foreach(UserGroup userGroup in UserGroups)
            {
                if(userGroup.Group.Name.ToLower() == group.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
