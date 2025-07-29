using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_CategoryTree
{
    public string ProductId { get; set; } = null!;

    public string Level0 { get; set; } = null!;

    public string Level1 { get; set; } = null!;

    public string Level2 { get; set; } = null!;

    public string? WebBadgeUrl { get; set; }

    public string? AppBadgeUrl { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual PNS_Product Product { get; set; } = null!;
}
