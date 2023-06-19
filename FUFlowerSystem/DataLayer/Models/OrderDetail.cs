using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int FlowerBouquetId { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public virtual FlowerBouquet FlowerBouquet { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
