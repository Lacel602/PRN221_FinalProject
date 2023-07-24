using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN221_FinalProject.DataAccess;

public partial class Cart
{
    public int ProductId { get; set; }

    public int AccountId { get; set; }

    public int? Quantity { get; set; }
    [JsonIgnore]
    public virtual Account Account { get; set; } = null!;
    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
