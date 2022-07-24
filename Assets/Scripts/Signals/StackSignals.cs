using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onInteractionATM = delegate { };
        public UnityAction<GameObject> onIteractionObstacle = delegate { };
        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction<Vector2> onStackFollowPlayer = delegate { };
        public UnityAction onUpdateType=delegate { };
        // public UnityAction<GameObject> onRemoveFromStack=delegate {  };
        
    }
}