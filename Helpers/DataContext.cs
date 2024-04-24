namespace LessonMvc.Helpers;

using Microsoft.EntityFrameworkCore;
using LessonMvc.Models;

public class DataContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
        
    }


    public DbSet<Book> Book { get; set; }
    public DbSet<Category> Category { get; set; }
}