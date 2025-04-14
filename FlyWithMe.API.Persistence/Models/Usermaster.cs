using System;
using System.Collections.Generic;

namespace FlyWithMe.API.Persistence.Models;

public partial class Usermaster
{
    public long Userid { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Fullname { get; set; }

    public string Emailid { get; set; }

    public DateTime? Createdon { get; set; }

    public DateTime? Lastlogindate { get; set; }

    public virtual ICollection<Userchatdetail> Userchatdetails { get; set; } = new List<Userchatdetail>();
}
