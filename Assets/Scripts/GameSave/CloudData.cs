using System;

namespace GameSave
{
    [Serializable]
    public class CloudData
    {
        public GameSaveData gameData;
        public string saveTime;
    }
}