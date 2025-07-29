using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_MarketingInitiative
{
    public string ProductId { get; set; } = null!;

    public string ThemeCode { get; set; } = null!;

    public string InitiativeCode { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public virtual PNS_Product Product { get; set; } = null!;
}
