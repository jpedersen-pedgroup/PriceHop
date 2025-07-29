using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class NW_Product
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

    public virtual ICollection<NW_CategoryTree> CategoryTrees { get; set; } = new List<NW_CategoryTree>();

    public virtual ICollection<NW_Facet> Facets { get; set; } = new List<NW_Facet>();

    public virtual NW_MarketingInitiative? MarketingInitiative { get; set; }

    public virtual NW_Pricing? Pricing { get; set; }

    public virtual ICollection<NW_Promotion> Promotions { get; set; } = new List<NW_Promotion>();

    public virtual NW_VariableWeight? VariableWeight { get; set; }
}
