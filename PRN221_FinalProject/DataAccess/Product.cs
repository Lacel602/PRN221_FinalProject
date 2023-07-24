using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN221_FinalProject.DataAccess;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? QuantityInStock { get; set; }

    public string? ProductImage { get; set; }

    public string? Description { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
