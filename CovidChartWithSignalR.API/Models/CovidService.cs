﻿using CovidChartWithSignalR.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CovidChartWithSignalR.API.Models
{
	public class CovidService
	{
		private readonly AppDbContext _context;
		private readonly IHubContext<CovidHub> _hubContext;
		public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}

		public IQueryable<Covid> GetList()
		{
			return _context.Covids.AsQueryable();
		}

		public async Task SavedCovid(Covid covid)
		{
			await _context.Covids.AddAsync(covid);
			await _context.SaveChangesAsync();
			await _hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidChartList());
		}

		public List<CovidChart> GetCovidChartList()
		{
			List<CovidChart> covidCharts= new();

			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "select tarih,[1],[2],[3],[4],[5] from (select [City],[Count],Cast([CovidDate] as date) as tarih from Covids) as covidT PIVOT (SUM(Count) For City IN([1],[2],[3],[4],[5])) as ptable order by tarih asc";

				command.CommandType = System.Data.CommandType.Text;

				_context.Database.OpenConnection();

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						CovidChart cc = new CovidChart();
						cc.CovidDate = reader.GetDateTime(0).ToShortDateString();
						Enumerable.Range(1, 5).ToList().ForEach(x =>
						{
							if (System.DBNull.Value.Equals(reader[x]))
							{
								cc.Counts.Add(0);
							}
							else
							{
								cc.Counts.Add(reader.GetInt32(x));
							}
						});

						covidCharts.Add(cc);
					}
				}

				_context.Database.CloseConnection();

				return covidCharts;
			}
		}
	}
}
