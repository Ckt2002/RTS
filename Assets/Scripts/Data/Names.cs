using System.Collections.Generic;

public static class Names
{
    // Key word
    public const string Player = "Player";
    public const string Enemy = "Enemy";

    // Player objects
    public const string PlayerHeadquarters = "Player Headquarters";
    public const string PlayerFactory = "Player Factory";
    public const string PlayerResearchLab = "Player Research Lab";
    public const string PlayerInfantry = "Player Infantry";
    public const string PlayerLightTank = "Player Light Tank";
    public const string PlayerMediumTank = "Player Medium Tank";
    public const string PlayerHeavyTank = "Player Heavy Tank";

    // Enemy objects
    public const string EnemyInfantry = "Enemy Infantry";
    public const string EnemyLightTank = "Enemy Light Tank";
    public const string EnemyMediumTank = "Enemy Medium Tank";
    public const string EnemyHeavyTank = "Enemy Heavy Tank";

    // Bullets
    public const string PlayerGroundBullet = "Player Ground Bullet";
    public const string EnemyGroundBullet = "Enemy Ground Bullet";

    // List Enemy
    public static readonly List<string> enemyUnitNameLst = new()
    {
        EnemyInfantry,
        EnemyLightTank,
        EnemyMediumTank,
        EnemyHeavyTank
    };
}