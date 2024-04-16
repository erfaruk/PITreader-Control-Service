using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class KeyGroup
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public string? SecurityId { get; set; }

    public int? Permission { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual Group? Group { get; set; }
}
