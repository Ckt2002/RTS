using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class LoadSlotInfor
    {
        public string Name;
        public string Date;
        public bool IsCloud;
        public byte MapType;

        public void SetInfor(string name, string date, bool isCloud, byte mapType = 1)
        {
            Name = name;
            Date = date;
            IsCloud = isCloud;
            MapType = mapType;
        }
    }
}