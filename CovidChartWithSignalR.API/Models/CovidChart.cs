namespace CovidChartWithSignalR.API.Models
{
	public class CovidChart
	{
		public CovidChart()
		{
			Counts = new List<int>();
		}
		public string CovidDate { get; set; }
		public List<int> Counts { get; set; }
	}
}
