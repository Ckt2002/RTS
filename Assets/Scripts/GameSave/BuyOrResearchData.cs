using System;

namespace GameSave
{
    [Serializable]
    public class BuyOrResearchData
    {
        public ObjectData Obj;
        public bool IsBuy;
        public CoroutineProgressData Coroutine;
    }
}