using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_VariableWeight
{
    public string ProductId { get; set; } = null!;

    public string UoM { get; set; } = null!;

    public int? AvgWeight { get; set; }

    public int? MinOrderQty { get; set; }

    public int? StepSize { get; set; }

    public string? StepUoM { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual PNS_Product Product { get; set; } = null!;
}
