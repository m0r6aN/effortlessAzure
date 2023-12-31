﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MF.Libraries.Data.Tableau.Context;
using MF.Libraries.Data.Tableau.Entity;
using System;
using System.Collections.Generic;

namespace MF.Libraries.Data.Tableau.Context.Configurations
{
    public partial class EquipmentCategoriesConfiguration : IEntityTypeConfiguration<EquipmentCategories>
    {
        public void Configure(EntityTypeBuilder<EquipmentCategories> entity)
        {
            entity.HasKey(e => e.PKey).HasName("PK_LaneMatching.EquipmentCategories");

            entity.ToTable("EquipmentCategories", "LaneMatching");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<EquipmentCategories> entity);
    }
}
