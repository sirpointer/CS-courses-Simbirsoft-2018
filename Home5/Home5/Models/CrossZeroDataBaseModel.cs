namespace Home5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CrossZeroDataBaseModel : DbContext
    {
        public CrossZeroDataBaseModel()
            : base("name=CrossZeroDataBaseModel")
        {
        }

        public virtual DbSet<GameResult> GameResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
