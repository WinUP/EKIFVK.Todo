using System;
using System.Collections.Generic;

namespace EKIFVK.Todo.API.Models
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            Task = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Usergroup { get; set; }
        public string AccessToken { get; set; }
        public DateTime? LastActiveTime { get; set; }
        public string LastAccessIp { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<Task> Task { get; set; }
        public virtual SystemUsergroup UsergroupNavigation { get; set; }
    }
}
