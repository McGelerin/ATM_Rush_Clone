using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;
using Enums;

namespace Controllers {
    public class CollactableMeshController : MonoBehaviour
    {
        #region Self Variables
        #region Serializefield Variables

        [SerializeField] private MeshFilter collectableMeshFilter;
        #endregion
        #region Private Variables

        [Header("Data")]private CollectableMeshData _collectableMeshData;
        #endregion
        #endregion
        
        public void MeshDataInitialize(CollectableMeshData dataMeshData)
        {
            _collectableMeshData = dataMeshData;
        }

        public void SetMeshData(CollectableType type)
        {
            collectableMeshFilter.mesh = _collectableMeshData.meshdatas[(int)type];
        }
    }
}