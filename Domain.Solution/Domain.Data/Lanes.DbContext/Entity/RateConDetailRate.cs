﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace MF.Libraries.Data.Lanes.Entity;

public partial class RateConDetailRate
{
    public int Pkey { get; set; }

    public int RateConInfoPkey { get; set; }

    public string RateDescription { get; set; }

    public decimal? Rate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual RateConInfo RateConInfoPkeyNavigation { get; set; }
}