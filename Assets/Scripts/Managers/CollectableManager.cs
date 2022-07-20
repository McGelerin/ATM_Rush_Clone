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

    [Space]
    public bool IsCollectable;
    
    //Type de�ime durumu
    public CollectableType CollectableTypeValue {
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
        IsCollectable = true;
        MeshData = GetMeshData();
        CollectableTypeValue = CollectableType.Money;
    }
    private CollectableMeshData GetMeshData() => Resources.Load<CD_CollectableData>("Data/CD_CollectableData").CollectableMeshData;
    private void SendCollectableMeshDataToControllers()
    {
        collactableMeshController.SetMeshData(MeshData,CollectableTypeValue);
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

    public void OnIteractionWithCollectable(GameObject collectableGameObject)
    {
        collectableGameObject.tag = "Collected";
        StackSignals.Instance.onInteractionCollectable?.Invoke(collectableGameObject);
    }

    public void OnIteractionWithATM(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionATM?.Invoke(collectableGameObject);
    }

    public void OnIteractionWithObstacle(GameObject collectableGameObject)
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
