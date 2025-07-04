using Questao2;

var footballService = new FootballService();

try
{
    Console.WriteLine("Calculando gols dos times...");

    int goalsPSG2013 = await footballService.CalculateGoalsForTeam("Paris Saint-Germain", 2013);
    Console.WriteLine($"Team Paris Saint-Germain scored {goalsPSG2013} goals in 2013");

    int goalsChelsea2014 = await footballService.CalculateGoalsForTeam("Chelsea", 2014);
    Console.WriteLine($"Team Chelsea scored {goalsChelsea2014} goals in 2014");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao calcular gols: {ex.Message}");
}
