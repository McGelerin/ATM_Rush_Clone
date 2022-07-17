using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_CollectableData", menuName = "ATM_Rush/CD_CollectableData", order = 0)]
public class CD_CollectableData : ScriptableObject
{
    public List<CollectableMeshData> Meshs = new List<CollectableMeshData>();
}
}
