using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_Facet
{
    public string ProductId { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string ItemDesc { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public virtual PNS_Product Product { get; set; } = null!;
}
