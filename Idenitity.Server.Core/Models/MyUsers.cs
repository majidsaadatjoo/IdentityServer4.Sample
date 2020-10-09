using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Idenitity.Server.Core.Models
{
    public class MyUsers : IdentityUser<int>
    {
        // impelement your Custmize
        //public int Sample { get; set; }
        public bool IsActive { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName { get => FName + " " + LName; }
    }
}
