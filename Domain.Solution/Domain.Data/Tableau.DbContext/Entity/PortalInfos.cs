﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MF.Libraries.Data.Tableau.Entity;

public partial class PortalInfos
{
    public int PKey { get; set; }

    public int? FHCustomerPKey { get; set; }

    public string CustomerName { get; set; }

    public string Url { get; set; }

    public string UserName { get; set; }

    public string PortalPassword { get; set; }

    public DateTime UpdatedDate { get; set; }
}