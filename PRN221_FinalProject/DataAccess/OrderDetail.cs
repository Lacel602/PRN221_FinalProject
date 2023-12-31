﻿using System;
using System.Collections.Generic;

namespace PRN221_FinalProject.DataAccess;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalMoney { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
