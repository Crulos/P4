using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P4.Entities;

namespace P4.Models
{
    public class classPro1
    {

        public object rooms { get; set; }
        public object inRooms { get; set; }
        public object users { get; set; }


    }

    public partial class rooms
    {
        public int cID1 { get; set; }
        public string cName1 { get; set; }
        public string cPass1 { get; set; }
        public Nullable<int> cUser1 { get; set; }
        public string cAd1 { get; set; }
        public string NamePhoto1 { get; set; }
    }

    public partial class inRooms
    {
        public int cID1 { get; set; }
        public string userName1 { get; set; }
    }
    public partial class users
    {

        public string userName1 { get; set; }
        public string userPass1 { get; set; }
    }
}