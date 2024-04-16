using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class Group
{
    public int GroupId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<KeyGroup> KeyGroups { get; set; } = new List<KeyGroup>();
}
