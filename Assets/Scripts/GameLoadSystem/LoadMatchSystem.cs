using System.Threading.Tasks;
using GameSave;

public static class LoadMatchSystem
{
    public static async Task LoadMatch(MatchData matchData)
    {
        if (matchData == null) return;

        MatchController.Instance.LoadMatch(matchData.CurrentRound, matchData.TimeToNexRound, matchData.IsInMatch);
        await LoadCreateEnemySystem.LoadCreateEnemy(matchData.SpawnEnemyData);
    }
}