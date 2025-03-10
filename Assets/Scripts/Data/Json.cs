using System;
using System.Collections.Generic;
using GameSave;

[Serializable]
public class BuildingQueueJSON
{
    public string key;
    public int value;
}

[Serializable]
public class BuildingQueueJSONList
{
    public List<ObjectData> list;
}