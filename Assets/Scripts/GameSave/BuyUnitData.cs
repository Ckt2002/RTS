using System;
using System.Collections.Generic;
using GameSave;

[Serializable]
public class BuyUnitData
{
    public string unitName;
    public float buyTime;
    public List<ObjectData> buildingQueueLst;
}