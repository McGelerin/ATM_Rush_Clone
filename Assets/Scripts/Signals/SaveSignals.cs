using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveGameData = delegate { };
        public Func<int> onGetLevelID = delegate { return 0; };
        public Func<int> onGetMoney = delegate { return 0; };
    }
}