using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class PNS_Category
{
    public int CategoryId { get; set; }

    public Guid StoreId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual PNS_Category? Category1Navigation { get; set; }

    public virtual ICollection<PNS_Category> InverseCategory1Navigation { get; set; } = new List<PNS_Category>();
}
