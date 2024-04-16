using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class Transponder
{
    public string SecurityId { get; set; } = null!;

    public string? SerialNo { get; set; }

    public string? OrderNo { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
