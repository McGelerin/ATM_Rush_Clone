using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
       
        public UnityAction<int> onSetScore = delegate { };
        public UnityAction<int> onSetAtmScore = delegate { };
        public UnityAction<int> onSetTotalScore = delegate { };
        public UnityAction<int> onSetAtmScoreText = delegate { };
        public UnityAction<int> onSendFinalScore = delegate { };
        public UnityAction<float> onSendMoney = delegate { };
        
        public Func<float> onGetMultiplier= delegate { return 0;};
    }
}