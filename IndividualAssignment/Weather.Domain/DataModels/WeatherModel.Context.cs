﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weather.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WP13_mg222cd_WeatherEntities : DbContext
    {
        public WP13_mg222cd_WeatherEntities()
            : base("name=WP13_mg222cd_WeatherEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Forecast> Forecasts { get; set; }
        public virtual DbSet<Geoname> Geonames { get; set; }
    }
}
