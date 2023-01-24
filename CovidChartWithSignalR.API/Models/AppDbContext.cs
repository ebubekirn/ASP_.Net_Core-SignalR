using Microsoft.EntityFrameworkCore;

namespace CovidChartWithSignalR.API.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
		{

		}
		public DbSet<Covid> Covids { get; set; }
	}
}
