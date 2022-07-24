using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onScoreUp = delegate { };
        public UnityAction<int> onScoreDown = delegate { };
        public UnityAction<int> onSetScore = delegate { };
        public UnityAction<int> onSetAtmScore = delegate { };
        public UnityAction<int> onSetAllScore = delegate { };
    }
}