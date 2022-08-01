using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public UnityAction onConveyor = delegate { };
        public UnityAction<GameObject> onAtmTouched = delegate { };
        public UnityAction onMiniGameStart = delegate { };
        
        public UnityAction onClickIncome=delegate{  };
        public UnityAction onClickStack=delegate{  };
        
        public Func<int> onGetIncomeLevel= delegate { return 0;};
        public Func<int> onGetStackLevel= delegate { return 0;};
    }
}