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
        //public bool CollectableInStack;
        #endregion
        #region Serializefield Variables
        [SerializeField] private CollectableManager manager;
        #endregion
        #region Private Variables
        #endregion
        #endregion
        //private void Awake()
        //{
        //    CollectableInStack = false;
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                
                other.tag = "Collected";
                manager.IteractionWithCollectable(other.transform.parent.gameObject);
            }

            #region useless
            // if (other.CompareTag("Player"))
            // {
            //     if (!CollectableInStack)
            //     {
            //         CollectableInStack = true;
            //         this.tag = "Collected";
            //         collectableManager.OnIteractionWithCollectable(this.transform.parent.gameObject);
            //     }
            // } 
            #endregion

            if (other.CompareTag("CollectableUpdater"))
            {
                manager.CollectableMeshUpdater();
            }

            if (other.CompareTag("ATM"))
            {
                manager.IteractionWithATM(this.transform.parent.gameObject);
            }

            if (other.CompareTag("Obstacle")&& CompareTag("Collected"))
            {

                manager.IteractionWithObstacle(this.transform.parent.gameObject);
            }
        }
    }
}

