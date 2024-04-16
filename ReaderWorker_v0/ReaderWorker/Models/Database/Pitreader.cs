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

    public int Port { get; set; }

    public string? Fingerprint { get; set; }

    public int? ServerId { get; set; }

    public bool? Status { get; set; }

    public int? KeyId { get; set; }

    public int? Permission { get; set; }

    public virtual Transponder? Key { get; set; }

    public virtual Server? Server { get; set; }
}
