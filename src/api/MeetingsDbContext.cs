using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using meetingsApi;

public class MeetingContext : DbContext
{
    public DbSet<Meeting> Meetings { get; set; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("MeetingsDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>().Ignore(x => x.Attendees);

        modelBuilder.Entity<Meeting>()
            .Property(e => e.Attendees)
            .IsRequired(false)
            .HasConversion(v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<Meeting>().HasData(new Meeting[]
            {
                new()
                {
                    ID = 3, Attendees = new List<string>() { "Brett" }, Title = "Meeting1", Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = "12:00"
                },
                new()
                {
                    ID = 4, Attendees = new List<string>() { "Nina", "Adam", "Bob" }, Title = "Meeting2", Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = "13:00"
                },
                new()
                {
                    ID = 5, Attendees = null, Title = "Meeting3", Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = "14:00"
                },
                new()
                {
                    ID = 6, Attendees = new List <string>() {}, Title = "Meeting4", Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = "15:00"
                }
            });
        base.OnModelCreating(modelBuilder);
    }
}

