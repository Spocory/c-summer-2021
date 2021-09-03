using System;

namespace UserManage.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public DateTime DateAdded { get; set; }

    }



}