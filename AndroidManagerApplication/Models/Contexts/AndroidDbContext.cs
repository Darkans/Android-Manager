using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.Contexts
{
    public class AndroidDbContext : DbContext
    {
        public DbSet<Android> AndroidList { get; set; }
        public DbSet<Job> JobList { get; set; }
        public DbSet<Skill> SkillList { get; set; }
        public DbSet<Image> ImageList { get; set; }

        public AndroidDbContext(): base("AndroidDBConnection")
        {
            Database.SetInitializer(new AndroidDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Androids)
                .WithOptional(a => a.CurrentJob);

            modelBuilder.Entity<Android>()
                .HasMany(a => a.Skills)
                .WithMany(s => s.Androids);

            modelBuilder.Entity<Android>()
                .HasRequired(a => a.Avatar)
                .WithMany(i => i.Androids);

            base.OnModelCreating(modelBuilder);
        }

        // Implementing Singleton pattern
        private static AndroidDbContext _instance;

        // Provide the only instance of context
        public static AndroidDbContext Instance()
        {
            if (_instance == null) _instance = new AndroidDbContext();
            return _instance;
        }
    }

    public class AndroidDbInitializer : CreateDatabaseIfNotExists<AndroidDbContext>
    {
        protected override void Seed(AndroidDbContext context)
        {
            byte[] defaultImageData;
            byte[] robotImageData1;
            byte[] robotImageData2;
            byte[] robotImageData3;

            using (var stream = new MemoryStream())
            {
                Resources.DefaultImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                defaultImageData = stream.ToArray();
            }

            using (var stream = new MemoryStream())
            {
                Resources.Robot1.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                robotImageData1 = stream.ToArray();
            }

            using (var stream = new MemoryStream())
            {
                Resources.Robot2.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                robotImageData2 = stream.ToArray();
            }

            using (var stream = new MemoryStream())
            {
                Resources.Robot3.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                robotImageData3 = stream.ToArray();
            }

            var defaultImage = new Image() { Id = 1, ImageData = defaultImageData };
            var robotImage1 = new Image() { Id = 2, ImageData = robotImageData1 };
            var robotImage2 = new Image() { Id = 3, ImageData = robotImageData2 };
            var robotImage3 = new Image() { Id = 4, ImageData = robotImageData3 };

            context.ImageList.Add(defaultImage);
            context.ImageList.Add(robotImage1);
            context.ImageList.Add(robotImage2);
            context.ImageList.Add(robotImage3);

            var job1 = new Job() { Id = 1, Name = "Builder", Describtion = "Build, repair and modernize buildings.", Complexity = 10 };
            var job2 = new Job() { Id = 2, Name = "Taxi driver", Describtion = "Accept orders and deliver people.", Complexity = 20 };
            var job3 = new Job() { Id = 3, Name = "Kittens rescuer", Describtion = "Remove kittens from trees.", Complexity = 30 };

            context.JobList.Add(job1);
            context.JobList.Add(job2);
            context.JobList.Add(job3);

            var skill1 = new Skill() { Id = 1, Name = "Fast" };
            var skill2 = new Skill() { Id = 2, Name = "Blind" };
            var skill3 = new Skill() { Id = 3, Name = "Cruel" };

            context.SkillList.Add(skill1);
            context.SkillList.Add(skill2);
            context.SkillList.Add(skill3);

            var android1 = new Android()
            {
                Name = "Bob",
                Skills = new List<Skill>() { skill1, skill2 },
                CurrentJob = job1,
                Avatar = robotImage1,
                Reliability = 10
            };
            var android2 = new Android()
            {
                Name = "Alice",
                Skills = new List<Skill>() { skill2, skill3 },
                CurrentJob = job2,
                Avatar = robotImage2,
                Reliability = 10
            };
            var android3 = new Android()
            {
                Name = "ANDROID 9000",
                Skills = new List<Skill>() { skill1, skill3 },
                CurrentJob = job3,
                Avatar = robotImage3,
                Reliability = 10
            };

            context.AndroidList.Add(android1);
            context.AndroidList.Add(android2);
            context.AndroidList.Add(android3);

            base.Seed(context);
        }
    }
}