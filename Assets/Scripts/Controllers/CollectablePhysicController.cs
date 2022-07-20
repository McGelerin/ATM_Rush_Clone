using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Sirenix.OdinInspector;

public class CollectablePhysicController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    public bool CollectableInStack = false;
    #endregion
    #region Serializefield Variables
    [SerializeField] private CollectableManager collectableManager;
    #endregion
    #region Private Variables
    #endregion
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            if (!collectableManager.IsCollectable && CollectableInStack)
            {
                CollectableInStack = true;
                collectableManager.OnIteractionWithCollectable(this.transform.parent.gameObject);
            }
        }

        else if (other.CompareTag("Player"))
        {
            if (collectableManager.IsCollectable)
            {
                CollectableInStack = true;
                Debug.Log(CollectableInStack);
                collectableManager.OnIteractionWithCollectable(this.transform.parent.gameObject);
            }
        }

        else if (other.CompareTag("CollectableUpdater"))
        {
            collectableManager.CollectableMeshUpdater();
        }

        else if (other.CompareTag("ATM"))
        {
            collectableManager.OnIteractionWithATM(this.transform.parent.gameObject);
        }

        else if (other.CompareTag("Obstacle"))
        {
            CollectableInStack = false;
            collectableManager.OnIteractionWithObstacle(this.transform.parent.gameObject);
        }
    }
}
