using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRegister.Models.DashBoard
{
    public class DashBoardViewModel
    {
        public UserProfile userProfile { get; set; }
        public List<Country> countrylists { get; set; }
    }
}