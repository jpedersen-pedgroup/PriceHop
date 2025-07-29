using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class NW_MarketingInitiative
{
    public string ProductId { get; set; } = null!;

    public string ThemeCode { get; set; } = null!;

    public string InitiativeCode { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public virtual NW_Product Product { get; set; } = null!;
}
