using Microsoft.EntityFrameworkCore;
using CityAPI.Entities;

namespace CityAPI.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Place> Places { get; set; } = null!;

        //Way Two : Using Constructor
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        //Way One
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlite("");
        //    base.OnConfiguring(optionBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New Delhi")
                {
                    Id = 1,
                    CityDescription = "India's Capital"
                },
                new City("Mumbai")
                {
                    Id = 2,
                    CityDescription = "India's Finance Hub"
                },
                new City("Jamnagar")
                {
                    Id = 3,
                    CityDescription = "India's Largest Exporter"
                });

            modelBuilder.Entity<Place>()
                .HasData(
                new Place("Parliament House")
                {
                    PlaceId = 1,
                    CityId = 1,
                    PlaceDescription = "President, Lok Sabha and Rajya Sabha"
                },
                new Place("Qutub Minar")
                {
                    PlaceId = 2,
                    CityId = 1,
                    PlaceDescription = "It is UNESCO World Heritage Site."
                },
                new Place("Hotel Taj Mahel")
                {
                    PlaceId = 3,
                    CityId = 2,
                    PlaceDescription = "Five Star Hotel in Mumbai"
                },
                new Place("Reliance Industries Limited")
                {
                    PlaceId = 4,
                    CityId = 3,
                    PlaceDescription = "World's Largest Oil Refinary"
                },
                new Place("Nyara Energy")
                {
                    PlaceId = 5,
                    CityId = 3,
                    PlaceDescription = "Formerly Known as Essar Oil and Essar Power Limited"
                });
        }

    }
}
