using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class NW_Promotion
{
    public string ProductId { get; set; } = null!;

    public string PromoId { get; set; } = null!;

    public int? RewardValue { get; set; }

    public string? RewardType { get; set; }

    public int? Threshold { get; set; }

    public int? LimitPerCart { get; set; }

    public bool BestPromo { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual NW_Product Product { get; set; } = null!;
}
