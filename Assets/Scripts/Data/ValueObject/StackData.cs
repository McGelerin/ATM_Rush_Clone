using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public float CollectableOffsetInStack = 1;
        [Range(0.1f, 0.4f)] public float LerpSpeed = 0.25f;
        [Range(0, 0.2f)] public float ShackAnimDuraction = 0.12f;
        [Range(1f, 10f)] public float JumpForce = 7f;
        public float JumpItemsClampX = 5f;
    }
}