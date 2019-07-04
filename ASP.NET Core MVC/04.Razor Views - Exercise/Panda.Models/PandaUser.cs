using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Models
{
    public class PandaUser : IdentityUser
    {
        public List<Package> Packages { get; set; }

        public List<Receipt> Receipts { get; set; }

        public PandaUser()
        {
            this.Packages = new List<Package>();
            this.Receipts = new List<Receipt>();
        }
    }
}
