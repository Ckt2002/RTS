using System;

[Serializable]
public class RoundEnemyTypeConfig
{
    public string enemyPrefabName; // Tên prefab trong pool
    public int firstAppearRound; // Round bắt đầu xuất hiện
    public int baseCount; // Số lượng ban đầu khi xuất hiện
    public int incrementPerRound; // Số lượng tăng thêm sau mỗi round tiếp theo
}