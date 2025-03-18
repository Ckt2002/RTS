using System.Threading.Tasks;
using GameSave;

public class LoadMatchSystem
{
    public static async Task LoadMatch(MatchData matchData)
    {
        if (matchData == null) return;

        MatchController.Instance.LoadMatch(matchData.CurrentRound, matchData.TimeToNexRound);
        await LoadCreateEnemySystem.LoadCreateEnemy(matchData.SpawnEnemyData);
    }
}