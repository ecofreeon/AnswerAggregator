﻿using System.Data.Entity;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Infrastructure;

namespace AnswerAggregator.Domain.Contexts
{
    [DbConfigurationType(typeof(CustomDbConfiguration))]
    public class ApplicationContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; } 
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; } 
        public DbSet<University> Universities { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<GroupSubject> GroupSubjects { get; set; }

        public ApplicationContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new SubjectConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());
            modelBuilder.Configurations.Add(new UserMessageConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new DepratmentConfiguration());
        }
    }
}
