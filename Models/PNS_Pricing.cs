using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_Pricing
{
    public string ProductId { get; set; } = null!;

    public int Price { get; set; }

    public string? PromoId { get; set; }

    public int? CompPricePerUnit { get; set; }

    public int? CompUnitQuantity { get; set; }

    public string? CompUnitQuantityUoM { get; set; }

    public string? CompMeasureDescription { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual PNS_Product Product { get; set; } = null!;
}
