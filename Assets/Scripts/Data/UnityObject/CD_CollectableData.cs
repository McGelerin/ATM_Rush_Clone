using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;
//using Enums;

namespace Data.UnityObject
{
    
    [CreateAssetMenu(fileName = "CD_CollectableData", menuName = "ATM_Rush/CD_CollectableData", order = 0)]
    public class CD_CollectableData : ScriptableObject
    {
        //public CollectableType CollectableType;
        public CollectableMeshData CollectableMeshData;
    }
}
