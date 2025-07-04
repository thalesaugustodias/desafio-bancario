using Newtonsoft.Json;

namespace Questao2.DTOs
{
    public class FootballMatch
    {
        public string Competition { get; set; }
        public int Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        [JsonProperty("team1goals")]
        public string Team1Goals { get; set; }
        [JsonProperty("team2goals")]
        public string Team2Goals { get; set; }
    }
}
