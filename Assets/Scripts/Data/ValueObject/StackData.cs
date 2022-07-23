using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public float CollectableOffsetInStack = 1;
        [Range(0.1f,0.4f)]
        public float LerpSpeed = 0.25f;
        [Range(0, 0.2f)]
        public float ShackAnimDuraction = 0.12f;
    }
}