using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Enums;
using Data.UnityObject;
using Data.ValueObject;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    [Header("Data")] public CollectableMeshData MeshData;
    [Header("DataType")] public CollectableType CollectableMeshType;
    #endregion
    #region SerializeField Variables
    [SerializeField] private CollactableMeshController collactableMeshController;
    #endregion
    #endregion

    private void Awake()
    {
        CollectableMeshType = GetMeshTypeData();
        MeshData = GetMeshData();
        SendCollectableMeshDataToControllers();
    }

    private CollectableMeshData GetMeshData() => Resources.Load<CD_CollectableData>("Data/CD_CollectableData").CollectableMeshData;
    private CollectableType GetMeshTypeData() => Resources.Load<CD_CollectableData>("Data/CD_CollectableData").CollectableType;

    private void SendCollectableMeshDataToControllers()
    {
        collactableMeshController.SetMeshData(MeshData,CollectableMeshType);
    }


    public void onIteractionWithATM()
    {
        
    }
    public void onIteractionWithCollectable()
    {
        
    }

    public void CollectableMeshUpdater()
    {
        
    }
}
