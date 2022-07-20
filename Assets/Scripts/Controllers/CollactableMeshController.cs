using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
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
        [Header("Data")] [ShowInInspector]private CollectableMeshData _collectableMeshData;
        [ShowInInspector]private CollectableType _collectabletype;
        #endregion
        #endregion

        public void SetMeshData(CollectableMeshData dataMeshData,CollectableType type)
        {
            _collectabletype = type;
            _collectableMeshData = dataMeshData;
            collectableMeshFilter.mesh = _collectableMeshData.meshdatas[(int)_collectabletype];
        }
    }
}

