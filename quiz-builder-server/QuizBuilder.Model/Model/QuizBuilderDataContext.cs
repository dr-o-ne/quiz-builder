using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Model.Model
{
    public class QuizBuilderDataContext : DbContext
    {
        public QuizBuilderDataContext(DbContextOptions<QuizBuilderDataContext> options)
            : base(options)
        { }

        public DbSet<Quiz> Quizzes { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IAuditableEntity entity)
                {
                    string identityName = Thread.CurrentPrincipal?.Identity?.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }
    }
}
