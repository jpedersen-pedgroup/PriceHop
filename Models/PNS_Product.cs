using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_Product
{
    public string ProductId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string SaleType { get; set; } = null!;

    public bool RestrictedFlag { get; set; }

    public bool LiquorFlag { get; set; }

    public bool TobaccoFlag { get; set; }

    public bool OriginRegulated { get; set; }

    public bool CateredFlag { get; set; }

    public string? Brand { get; set; }

    public string? OriginStatement { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual ICollection<PNS_CategoryTree> CategoryTree1s { get; set; } = new List<PNS_CategoryTree>();

    public virtual ICollection<PNS_Facet> Facet1s { get; set; } = new List<PNS_Facet>();

    public virtual PNS_MarketingInitiative? MarketingInitiative1 { get; set; }

    public virtual PNS_Pricing? Pricing1 { get; set; }

    public virtual ICollection<PNS_Promotion> Promotion1s { get; set; } = new List<PNS_Promotion>();

    public virtual PNS_VariableWeight? VariableWeight1 { get; set; }
}
