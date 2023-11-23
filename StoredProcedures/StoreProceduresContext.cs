using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scheduler_API.Model;
using Scheduler_API.ViewModel;

namespace Scheduler_API.StoreProcedures
{
    public partial class StoreProceduresContext : DbContext
    {
        public StoreProceduresContext()
        {
        }
        public StoreProceduresContext(DbContextOptions<StoreProceduresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProviderScheduleTeamplateData> sp_GetProviderScheduleTeamplateData { get; set; }

        public virtual DbSet<ScheduleTemplatePeriodData> sp_GetScheduleTemplatePeriodData { get; set; }
        public virtual DbSet<ProviderScheduleTeamplateData> sp_ValidationOfEffictiveandExpiryDate { get; set; }

        public virtual DbSet<ListofDates> sp_GetProviderScheduleDates { get; set; }

        public virtual DbSet<ListofDates> sp_GetProviderBlockedScheduleDates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=10.10.20.25;Database=bhishak_app_db; User=root;Password=root@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProviderScheduleTeamplateData>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<ScheduleTemplatePeriodData>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<ListofDates>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
