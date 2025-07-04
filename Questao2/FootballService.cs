using Newtonsoft.Json;
using Questao2.DTOs;

namespace Questao2
{
    public class FootballService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "https://jsonmock.hackerrank.com/api/football_matches";

        public FootballService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> CalculateGoalsForTeam(string teamName, int year)
        {
            int totalGoals = 0;

            totalGoals += await CalculateGoalsAsTeam1(teamName, year);

            totalGoals += await CalculateGoalsAsTeam2(teamName, year);

            return totalGoals;
        }

        private async Task<int> CalculateGoalsAsTeam1(string teamName, int year)
        {
            int totalGoals = 0;
            int currentPage = 1;
            bool hasMorePages = true;

            while (hasMorePages)
            {
                string url = $"{BASE_URL}?year={year}&team1={teamName}&page={currentPage}";
                var response = await _httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<FootballResponse>(response);

                if (data?.Data != null)
                {
                    foreach (var match in data.Data)
                    {
                        if (int.TryParse(match.Team1Goals, out int goals))
                        {
                            totalGoals += goals;
                        }
                    }
                }

                if (data?.Data?.Count < data?.PerPage || currentPage >= data?.TotalPages)
                {
                    hasMorePages = false;
                }
                else
                {
                    currentPage++;
                }
            }

            return totalGoals;
        }

        private async Task<int> CalculateGoalsAsTeam2(string teamName, int year)
        {
            int totalGoals = 0;
            int currentPage = 1;
            bool hasMorePages = true;

            while (hasMorePages)
            {
                string url = $"{BASE_URL}?year={year}&team2={teamName}&page={currentPage}";
                var response = await _httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<FootballResponse>(response);

                if (data?.Data != null)
                {
                    foreach (var match in data.Data)
                    {
                        if (int.TryParse(match.Team2Goals, out int goals))
                        {
                            totalGoals += goals;
                        }
                    }
                }

                if (data?.Data?.Count < data?.PerPage || currentPage >= data?.TotalPages)
                {
                    hasMorePages = false;
                }
                else
                {
                    currentPage++;
                }
            }

            return totalGoals;
        }
    }    
}