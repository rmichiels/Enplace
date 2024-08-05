using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class DeltaStore
{
    public int Id { get; set; }

    public int Target { get; set; }

    public int? Command { get; set; }
}
