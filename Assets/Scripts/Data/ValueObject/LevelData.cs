using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public List<StageData> StageList = new List<StageData>(3);
    }
}