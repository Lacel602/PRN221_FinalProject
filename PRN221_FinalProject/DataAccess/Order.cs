using System;
using System.Collections.Generic;

namespace PRN221_FinalProject.DataAccess;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? ShipAddress { get; set; }

    public string? Phone { get; set; }

    public string? TotalOrderMoney { get; set; }

    public DateTime? ShipDate { get; set; }

    public string? Status { get; set; }

    public int? ShipCompanyId { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ShipCompany? ShipCompany { get; set; }
}
