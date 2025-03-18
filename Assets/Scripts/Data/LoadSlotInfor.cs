using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class LoadSlotInfor
    {
        public string Name;
        public string Date;
        public bool IsCloud;

        public void SetInfor(string name, string date, bool isCloud)
        {
            Name = name;
            Date = date;
            IsCloud = isCloud;
        }
    }
}