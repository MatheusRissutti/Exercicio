using Questao2;
using System.Net.Http.Json;
using System.Web;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;

        var totalGoals = GetTotalScoredGoals(teamName, year).GetAwaiter().GetResult();

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;

        totalGoals = GetTotalScoredGoals(teamName, year).GetAwaiter().GetResult();

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);
    }


    public static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        var goals = await GetGoalsCount(year, team, "team1");

        goals += await GetGoalsCount(year, team, "team2");

        return goals;
    }

    public static async Task<int> GetGoalsCount(int year, string team, string playSide)
    {
        int page = 1;
        int goalsCount = 0;
        var uriBuilder = new UriBuilder(Constants.JsonMockUrl);
        var content = new JsonMockFootball();

        using (var client = new HttpClient())
        {
            do
            {
                var url = HttpUtility.ParseQueryString(string.Empty);
                url["year"] = year.ToString();
                url[playSide] = team;
                url["page"] = page.ToString();
                uriBuilder.Query = url.ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
                HttpResponseMessage response = await client.SendAsync(request);
                content = await response.Content.ReadFromJsonAsync<JsonMockFootball>();

                foreach (var partida in content.Data)
                {
                    if(playSide.Equals("team1"))
                        goalsCount += int.Parse(partida.Team1Goals);
                    else
                        goalsCount += int.Parse(partida.Team2Goals);
                }

                page++;
            }
            while (page - 1 <= content.Total_Pages);
        }

        return goalsCount;
    }
}