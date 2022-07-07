
namespace CivilizationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<PlayerPrize> PlayersPrizes { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<PlayerMission> PlayersMissions { get; set; }

        public DbSet<MissionType> MissionTypes { get; set; }

        public DbSet<Wheel> Wheels { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Era> Eras { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.ID);

            modelBuilder.Entity<Prize>().HasKey(p => p.ID);
            modelBuilder.Entity<PlayerPrize>().HasKey(pp => pp.ID);

            modelBuilder.Entity<Mission>().HasKey(m => m.ID);
            modelBuilder.Entity<PlayerMission>().HasKey(pm => pm.ID);

            modelBuilder.Entity<Wheel>().HasKey(w => new { w.ID, w.PrizeID });

            modelBuilder.Entity<Unit>().HasKey(u => u.ID);
            modelBuilder.Entity<District>().HasKey(d => d.ID);

            modelBuilder.Entity<Era>().HasKey(e => e.ID);

            modelBuilder.Entity<Log>().HasKey(l => l.ID);



            modelBuilder.Entity<PlayerPrize>()
                .HasOne(pp => pp.Player)
                .WithMany(p => p.PlayerPrizes)
                .HasForeignKey(pp => pp.PlayerID);

            modelBuilder.Entity<PlayerPrize>()
                .HasOne(pp => pp.Prize)
                .WithMany(p => p.PlayerPrizes)
                .HasForeignKey(pp => pp.PrizeID);

            modelBuilder.Entity<Wheel>()
                .HasOne(w => w.Prize)
                .WithMany(p => p.Wheels)
                .HasForeignKey(w => w.PrizeID);

            modelBuilder.Entity<Unit>()
                .HasMany(u => u.Districts)
                .WithOne(d => d.Unit)
                .HasForeignKey(d => d.UnitID);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.District)
                .WithOne(d => d.Player)
                .HasForeignKey<Player>(p => p.DistrictID);

            modelBuilder.Entity<District>()
                .HasOne(d => d.Era)
                .WithMany(e => e.Districts)
                .HasForeignKey(d => d.EraID);

            modelBuilder.Entity<PlayerMission>()
                .HasOne(pm => pm.Player)
                .WithMany(p => p.Missions)
                .HasForeignKey(pm => pm.MissionID);

            modelBuilder.Entity<PlayerMission>()
                .HasOne(pm => pm.Mission)
                .WithMany(p => p.Players)
                .HasForeignKey(pm => pm.PlayerID);


            modelBuilder.Entity<MissionType>()
                .HasData(
                new MissionType() 
                { 
                    ID = 1, 
                    Name = "Стандарт"
                },
                new MissionType()
                {
                    ID = 2,
                    Name = "Дуэль"
                }
            );
        }
    }
}
