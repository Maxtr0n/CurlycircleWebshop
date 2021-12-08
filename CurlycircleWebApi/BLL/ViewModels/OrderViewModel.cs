﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace BLL.ViewModels
{
  public class OrderViewModel
  {
    public int Id { get; set; }

    public DateTime OrderDateTime { get; set; }

    public List<OrderItemViewModel> OrderItems { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string City { get; set; }

    public int ZipCode { get; set; }

    public string Address { get; set; }

    public double Total { get; set; }

    public ShippingMethod ShippingMethod { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public string PhoneNumber { get; set; }

    public string Note { get; set; }
  }
}