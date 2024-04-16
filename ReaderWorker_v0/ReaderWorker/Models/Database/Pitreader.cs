using System;
using System.Collections.Generic;

namespace ReaderWorker.Models.Database;

public partial class Pitreader
{
    public int ReaderId { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string Apitoken { get; set; } = null!;

    public int? BlockListId { get; set; }

    public int? Port { get; set; }

    public string? Fingerprint { get; set; }
}
