using OMNext.Models;
using Microsoft.EntityFrameworkCore;

namespace OMNext.Data
{
    public class OM2018Context : DbContext
    {
        public OM2018Context()
        {
        }

        public OM2018Context(DbContextOptions<OM2018Context> options) : base(options)
        {
        }

        public DbSet<Connection> Connections { get; set; }
        public DbSet<RunningMission> RunningMissions { get; set; }
        public DbSet<DataDrive> DataDrives { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<CLCCenter> CLCCenters { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<DataDriveData> DataDriveDatas { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().ToTable("Connection");
            modelBuilder.Entity<RunningMission>().ToTable("RunningMission");
            modelBuilder.Entity<DataDrive>().ToTable("DataDrive");
            modelBuilder.Entity<Script>().ToTable("Script");
            modelBuilder.Entity<CLCCenter>().ToTable("CLCCenter");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Chat>().ToTable("Chat");
            modelBuilder.Entity<DataDriveData>().ToTable("DataDriveData");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
        }
    }
}
