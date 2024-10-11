public static class FastPlayerData
{
    private static readonly PlayerData playerDataInstance = PlayerData.Instacne;

    #region 数据读取
    // 当前光能
    public static int GetCurrentLightEnergy() => playerDataInstance.CurrentLightEnergy;

    // 光能上限
    public static int GetMaxLightEnergy() => playerDataInstance.MaxLightEnergy;

    // 当前关卡
    public static int GetCurrentLevel() => playerDataInstance.CurrentLevel;

    // 完成的关卡数
    public static int GetLevelsCompleted() => playerDataInstance.LevelsCompleted;

    // 已收集的余光总数
    public static int GetCollectedResidualLight() => playerDataInstance.CollectedResidualLight;

    // 已点亮的星石总数
    public static int GetLittedStarStone() => playerDataInstance.LittedStarStone;

    // 已击杀的敌人总数
    public static int GetEnemiesDefeated() => playerDataInstance.EnemiesDefeated;
    #endregion

    #region 数据写入
    // 完成关卡时调用
    public static void CompleteLevel() => playerDataInstance.CompleteLevel();

    // 击杀敌人时调用
    public static void AddEnemieDefeated(int amount = 1) =>
        playerDataInstance.AddEnemieDefeated(amount);

    // 收集余光时调用
    public static void CollectResidualLight(int amount = 1) =>
        playerDataInstance.CollectResidualLight(amount);

    // 点亮星石时调用
    public static void LitStarStone(int amount = 1) => playerDataInstance.LitStarStone(amount);

    // 消耗光能时调用
    public static bool ConsumeLightEnergy(int amount = 1) =>
        playerDataInstance.ConsumeLightEnergy(amount);
    #endregion
}
