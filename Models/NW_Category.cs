using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class NW_Category
{
    public int CategoryId { get; set; }

    public Guid StoreId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual NW_Category? CategoryNavigation { get; set; }

    public virtual ICollection<NW_Category> InverseCategoryNavigation { get; set; } = new List<NW_Category>();
}
