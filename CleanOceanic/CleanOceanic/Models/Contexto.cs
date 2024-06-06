using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CleanOceanic.Models;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {

    }

    public DbSet<Volunteer> Volunteer { get; set; }

}
