using Microsoft.EntityFrameworkCore;
using User.Model.SpDbContext;

namespace Recipe.Model.RecipeSPContext
{
    public partial class RecipeSpContext : DbContext
    {
        public RecipeSpContext() { }

        public RecipeSpContext(DbContextOptions<RecipeSpContext> options)
            : base(options)
        {
        }

        // These are SP result models
        public virtual DbSet<ExecutreStoreProcedureResult> ExecutreStoreProcedureResult { get; set; }
        public virtual DbSet<ExecutreStoreProcedureResultWithSID> ExecutreStoreProcedureResultWithSID { get; set; }
        public virtual DbSet<ExecuteStoreProcedureResultWithId> ExecuteStoreProcedureResultWithId { get; set; }
        public virtual DbSet<ExecutreStoreProcedureResultList> ExecutreStoreProcedureResultList { get; set; }
        public virtual DbSet<ExecutreStoreProcedureResultWithEntitySID> ExecutreStoreProcedureResultWithEntitySID { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region SP           
            modelBuilder.Entity<ExecutreStoreProcedureResult>().HasNoKey();
            modelBuilder.Entity<ExecutreStoreProcedureResultWithSID>().HasNoKey();
            modelBuilder.Entity<ExecuteStoreProcedureResultWithId>().HasNoKey();
            modelBuilder.Entity<ExecutreStoreProcedureResultList>().HasNoKey();
            modelBuilder.Entity<ExecutreStoreProcedureResultWithEntitySID>().HasNoKey();

            
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
