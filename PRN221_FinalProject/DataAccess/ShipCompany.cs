using System;
using System.Collections.Generic;

namespace PRN221_FinalProject.DataAccess;

public partial class ShipCompany
{
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
