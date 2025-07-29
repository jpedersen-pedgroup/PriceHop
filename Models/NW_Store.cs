using System;
using System.Collections.Generic;

namespace PriceHop.Models;

public partial class NW_Store
{
    public Guid StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Banner { get; set; }

    public string? Address { get; set; }

    public bool ClickAndCollect { get; set; }

    public bool Delivery { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Phone { get; set; }

    public string? LocalPhone { get; set; }

    public string? PhysicalStoreCode { get; set; }

    public string? Region { get; set; }

    public string? SalesOrgId { get; set; }

    public bool OnboardingMode { get; set; }

    public string? DefaultCollectType { get; set; }

    public bool ExpressTimeslots { get; set; }

    public int? ExpressProductLimit { get; set; }

    public string? LiquorLicenseUrl { get; set; }

    public string? PhysicalStreetName { get; set; }

    public string? PhysicalCityName { get; set; }

    public string? PhysicalAdditionalCity { get; set; }

    public string? PhysicalRegionName { get; set; }

    public string? PhysicalDistrictName { get; set; }

    public string? PhysicalPostalCode { get; set; }

    public string? PhysicalCountryCode { get; set; }

    public string? PhysicalCountryName { get; set; }

    public DateTime LastUpdated { get; set; }
}
