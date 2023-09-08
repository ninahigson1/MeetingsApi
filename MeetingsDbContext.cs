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
        modelBuilder.Entity<Meeting>().HasData(new Meeting[]
        {
            new()
            {
                ID = 3, Attendees = new[] { "Brett" }, Title = "Meeting1", Date = DateOnly.FromDateTime(DateTime.Now),
                Time = "12:00"
            },
            new()
            {
                ID = 4, Attendees = new[] { "Nina" }, Title = "Meeting2", Date = DateOnly.FromDateTime(DateTime.Now),
                Time = "13:00"
            },
            new()
            {
                ID = 5, Attendees = new[] { "Bob" }, Title = "Meeting3", Date = DateOnly.FromDateTime(DateTime.Now),
                Time = "14:00"
            },
        });
        base.OnModelCreating(modelBuilder);
    }
}

