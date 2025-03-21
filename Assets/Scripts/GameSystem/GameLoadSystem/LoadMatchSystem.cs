using System.Threading.Tasks;
using GameSave;

public static class LoadMatchSystem
{
    public static async Task LoadMatch(MatchData matchData)
    {
        if (matchData == null) return;

        MatchController.Instance.LoadMatch(matchData.CurrentRound, matchData.RoundTimer, matchData.IsSpawning,
            matchData.WaitingForNextRound, matchData.SpawnEnemyData);
        await LoadCreateEnemySystem.LoadCreateEnemy(matchData.SpawnEnemyData);
    }
}