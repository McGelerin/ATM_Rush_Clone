using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Sirenix.OdinInspector;

namespace Controllers
{
    public class CollectablePhysicController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        public bool CollectableInStack;
        #endregion
        #region Serializefield Variables
        [SerializeField] private CollectableManager collectableManager;
        #endregion
        #region Private Variables
        #endregion
        #endregion
        private void Awake()
        {
            CollectableInStack = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && this.CompareTag("Collected"))
            {
                
                other.tag = "Collected";
                collectableManager.OnIteractionWithCollectable(other.transform.parent.gameObject);
            }

            // if (other.CompareTag("Player"))
            // {
            //     if (!CollectableInStack)
            //     {
            //         CollectableInStack = true;
            //         this.tag = "Collected";
            //         collectableManager.OnIteractionWithCollectable(this.transform.parent.gameObject);
            //     }
            // }

            if (other.CompareTag("CollectableUpdater"))
            {
                collectableManager.CollectableMeshUpdater();
            }

            if (other.CompareTag("ATM"))
            {
                collectableManager.OnIteractionWithATM(this.transform.parent.gameObject);
            }

            if (other.CompareTag("Obstacle"))
            {

                collectableManager.OnIteractionWithObstacle(this.transform.parent.gameObject);
            }
        }
    }
}

