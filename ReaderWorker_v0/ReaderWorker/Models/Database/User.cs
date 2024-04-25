using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Role { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Company { get; set; }

    public string? Description { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Transponder> Transponders { get; set; } = new List<Transponder>();
}
