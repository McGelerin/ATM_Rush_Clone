using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Signals;
using Enums;
using Data.UnityObject;
using Data.ValueObject;
using Sirenix.Serialization;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("Data")] public CollectableMeshData MeshData;
    //[Space]
    //public bool IsCollectable;

    //Type degisme durumu

    public CollectableType CollectableTypeValue
    {
        get => _collectableType;
        private set
        {
            
            _collectableType = value;
            SendCollectableMeshDataToMeshController();
            StackSignals.Instance.onUpdateType?.Invoke();
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
        // CollectableTypeValue = CollectableType.Money;
    }
    private CollectableMeshData GetMeshData() =>
        Resources.Load<CD_CollectableData>("Data/CD_CollectableData").CollectableMeshData;

    //update mesh data
    private void MeshDataInitializeToMeshController()
    {
        collactableMeshController.MeshDataInitialize(MeshData);
    }

    private void SendCollectableMeshDataToMeshController()
    {
        collactableMeshController.SetMeshData(CollectableTypeValue);
    }


    // private void RemoveStack(GameObject ContVal)
    // {
    //     if (ContVal == this.gameObject)
    //     {
    //         // Destroy(gameObject);
    //         transform.SetParent(null);
    //         transform.GetChild(1).tag = "Collectable";
    //     }
    // }

    public void InteractionWithCollectable(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionCollectable?.Invoke(collectableGameObject);
    }

    public void InteractionWithATM(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionATM?.Invoke(collectableGameObject);
    }

    public void InteractionWithObstacle(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
    }

    public void CollectableMeshUpdater()
    {
        if ((int)CollectableTypeValue < 2)
        {
            CollectableTypeValue++;
        }
    }

    public void InteractionWithConveyor()
    {
        StackSignals.Instance.onInteractionConveyor?.Invoke();
    }
}