// 玩家数据类
// 一般情况下不需要访问此文件，请看 FastPlayerData.cs
using System;

public class PlayerData : BaseManager<PlayerData>
{
    #region 数据成员
    // 当前光能
    public int CurrentLightEnergy { get; private set; }

    // 光能上限
    public int MaxLightEnergy { get; private set; }

    // 当前关卡
    public int CurrentLevel { get; private set; }

    // 玩家完成的关卡数量
    public int LevelsCompleted { get; private set; }

    // 玩家收集的余光数量
    public int CollectedResidualLight { get; private set; }

    // 余光的光能转换率
    public int ResidualLightConversionRate { get; private set; }

    // 点亮的星石数量
    public int LittedStarStone { get; private set; }

    // 星石的光能上限转换率
    public int StarStoneConversionRate { get; private set; }

    // 玩家击败的敌人数量
    public int EnemiesDefeated { get; private set; }
    #endregion

    #region 构造函数
    // 构造函数
    public PlayerData()
    {
        CurrentLightEnergy = 3; // 初始光能
        MaxLightEnergy = 3; // 初始光能上限
        CurrentLevel = 1; // 初始关卡
        LevelsCompleted = 0; // 初始完成关卡数量
        CollectedResidualLight = 0; // 初始余光数量
        ResidualLightConversionRate = 1; // 初始余光转换率
        LittedStarStone = 0; // 初始点亮星石数量
        StarStoneConversionRate = 1; // 初始星石转换率
        EnemiesDefeated = 0; // 初始击败敌人数
    }
    #endregion

    #region 公共方法
    /// <summary>
    /// 完成关卡, 记录完成的关卡数量
    /// </summary>
    public void CompleteLevel()
    {
        LevelsCompleted++;
        CurrentLevel++;
        DataChanged();
    }

    /// <summary>
    /// 增加击败敌人的数量
    /// </summary>
    /// <param name="amount">增加的数量</param>
    public void AddEnemieDefeated(int amount = 1)
    {
        EnemiesDefeated += amount;
        DataChanged();
    }

    /// <summary>
    /// 收集余光，附带增加光能
    /// </summary>
    /// <param name="amount">收集的余光数量</param>
    public void CollectResidualLight(int amount = 1)
    {
        CollectedResidualLight += amount;
        AddLightEnergy(amount * ResidualLightConversionRate);
        DataChanged();
    }

    /// <summary>
    /// 点亮星石，附带增加光能上限
    /// </summary>
    /// <param name="amount">点亮的星石数量</param>
    public void LitStarStone(int amount = 1)
    {
        LittedStarStone += amount;
        MaxLightEnergy += amount * StarStoneConversionRate;
        DataChanged();
    }

    /// <summary>
    /// 消耗光能
    /// </summary>
    /// <param name="amount">消耗的光能数量</param>
    /// <returns>是否成功消耗</returns>
    public bool ConsumeLightEnergy(int amount = 1)
    {
        if (CurrentLightEnergy < amount)
            return false;
        CurrentLightEnergy = Math.Max(CurrentLightEnergy - amount, 0);
        DataChanged();
        return true;
    }
    #endregion

    #region 私有方法
    // 方法：增加光能
    private void AddLightEnergy(int amount)
    {
        CurrentLightEnergy = Math.Min(CurrentLightEnergy + amount, MaxLightEnergy);
    }

    // 方法：数据改变
    private void DataChanged()
    {
        // 通知所有监听器数据改变
        EventCenter.Instacne.EventTrigger("OnPlayerDataChanged");
    }
    #endregion
}
