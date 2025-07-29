using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_StagingProductsRaw
{
    public string ProductJson { get; set; } = null!;

    public DateTime RetrievedAt { get; set; }
}
