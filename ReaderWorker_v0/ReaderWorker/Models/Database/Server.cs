using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class Server
{
    public int ServerId { get; set; }

    public string? Macaddress { get; set; }

    public virtual ICollection<Pitreader> Pitreaders { get; set; } = new List<Pitreader>();
}
