using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Training102.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {

        public ApplicationDbContext(DbContextOptions contextOption) :base (contextOption)
        {
            
        }

        public DbSet<Training> Training { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }

        public DbSet<TrainingAssignToUser> TrainingAssignToUser { get; set; }
        public DbSet<QuizCompletion> QuizCompletion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships for QuizCompletion
            modelBuilder.Entity<QuizCompletion>()
                .HasOne(qc => qc.User)
                .WithMany(u => u.QuizCompletions)
                .HasForeignKey(qc => qc.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<QuizCompletion>()
                .HasOne(qc => qc.Quiz)
                .WithMany()
                .HasForeignKey(qc => qc.QuizId)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            // Other configurations for your model

            // Ensure no multiple cascade paths
            modelBuilder.Entity<TrainingAssignToUser>()
                .HasOne(tau => tau.User)
                .WithMany(u => u.AssignedTrainings)
                .HasForeignKey(tau => tau.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<TrainingAssignToUser>()
                .HasOne(tau => tau.Training)
                .WithMany()
                .HasForeignKey(tau => tau.TrainingId)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION
        }


    }
}
