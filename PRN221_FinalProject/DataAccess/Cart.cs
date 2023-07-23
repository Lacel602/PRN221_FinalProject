using System;
using System.Collections.Generic;

namespace PRN221_FinalProject.DataAccess;

public partial class Cart
{
    public int ProductId { get; set; }

    public int AccountId { get; set; }

    public int? Quantity { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
