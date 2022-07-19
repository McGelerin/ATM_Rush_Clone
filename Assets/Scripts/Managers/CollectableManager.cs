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

    //Type deðime durumu
    public CollectableType CollectableMeshType {
        get=> _collectableType;
        private set
        {
            _collectableType = value;
            SendCollectableMeshDataToControllers();
        }
            }

    #endregion
    #region SerializeField Variables
    [SerializeField] private CollactableMeshController collactableMeshController;
    #endregion
    #region Private Variables
    private CollectableType _collectableType;
    #endregion
    #endregion

    private void Awake()
    {
        MeshData = GetMeshData();
        CollectableMeshType = CollectableType.Money;
    }

    private CollectableMeshData GetMeshData() => Resources.Load<CD_CollectableData>("Data/CD_CollectableData").CollectableMeshData;

    private void SendCollectableMeshDataToControllers()
    {
        collactableMeshController.SetMeshData(MeshData,CollectableMeshType);
    }


    //Test
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CollectableMeshType = CollectableType.Diamond;
        }
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
