using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Signals;
using Enums;
using Data.UnityObject;
using Data.ValueObject;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    [Header("Data")] public CollectableMeshData MeshData;
    //[Space]
    //public bool IsCollectable;
    
    //Type degisme durumu

    public CollectableType CollectableTypeValue {
        get=> _collectableType;
        private set
        {
            _collectableType = value;
            SendCollectableMeshDataToMeshController();
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
        //IsCollectable = true;
        MeshData = GetMeshData();
        MeshDataInitializeToMeshController();
        CollectableTypeValue = CollectableType.Money;
    }
    private CollectableMeshData GetMeshData() => Resources.Load<CD_CollectableData>("Data/CD_CollectableData").CollectableMeshData;

    //update mesh data
    private void MeshDataInitializeToMeshController()
    {
        collactableMeshController.MeshDataInitialize(MeshData);
    }

    private void SendCollectableMeshDataToMeshController()
    {
        collactableMeshController.SetMeshData(CollectableTypeValue);
    }

    //#region Event Subscription
    //private void OnEnable()
    //{
    //    SubscribeEvent();

    //}
    //private void SubscribeEvent()
    //{
    //    StackSignals.Instance.onInteractionCollectable += OnIteractionWithCollectable;
    //    StackSignals.Instance.onIteractionObstacle += OnIteractionWithObstacle;
    //    StackSignals.Instance.onInteractionATM += OnIteractionWithATM;
        
    //}    
    
    //private void UnSubscribeEvent()
    //{
    //    StackSignals.Instance.onInteractionCollectable -= OnIteractionWithCollectable;
    //    StackSignals.Instance.onIteractionObstacle -= OnIteractionWithObstacle;
    //    StackSignals.Instance.onInteractionATM -= OnIteractionWithATM;
    //}

    //private void OnDisable()
    //{
    //    UnSubscribeEvent();
    //}
    //#endregion

    public void IteractionWithCollectable(GameObject collectableGameObject)
    {
        collectableGameObject.tag = "Collected";
        StackSignals.Instance.onInteractionCollectable?.Invoke(collectableGameObject);
    }

    public void IteractionWithATM(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionATM?.Invoke(collectableGameObject);
    }

    public void IteractionWithObstacle(GameObject collectableGameObject)
    {
        StackSignals.Instance.onIteractionObstacle?.Invoke(collectableGameObject);
    }

    public void CollectableMeshUpdater()
    {
        if ((int)CollectableTypeValue < 2)
        {
            CollectableTypeValue++;
        }
    }
}
