using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class Transponder
{
    public int KeyId { get; set; }

    public string SecurityId { get; set; } = null!;

    public string? SerialNo { get; set; }

    public string? OrderNo { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Pitreader> Pitreaders { get; set; } = new List<Pitreader>();

    public virtual User? User { get; set; }
}
