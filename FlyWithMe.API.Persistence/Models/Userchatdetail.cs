using System;
using System.Collections.Generic;

namespace FlyWithMe.API.Persistence.Models;

public partial class Userchatdetail
{
    public long Chatid { get; set; }

    public long Userid { get; set; }

    public string Userchatrequest { get; set; }

    public string Userchatresponse { get; set; }

    public DateTime? Createddate { get; set; }

    public virtual Usermaster User { get; set; }
}
