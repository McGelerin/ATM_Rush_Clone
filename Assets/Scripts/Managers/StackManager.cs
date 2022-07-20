using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        public List<GameObject> CollectableStack = new List<GameObject>();
        public List<GameObject> CollectableStackValues = new List<GameObject>();
        #endregion
        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();

        }
        private void SubscribeEvent()
        {
            StackSignals.Instance.onInteractionCollectable += OnIteractionWithCollectable;
            StackSignals.Instance.onIteractionObstacle += OnIteractionWithObstacle;
            StackSignals.Instance.onInteractionATM += OnIteractionWithATM;

        }

        private void UnSubscribeEvent()
        {
            StackSignals.Instance.onInteractionCollectable -= OnIteractionWithCollectable;
            StackSignals.Instance.onIteractionObstacle -= OnIteractionWithObstacle;
            StackSignals.Instance.onInteractionATM -= OnIteractionWithATM;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
        #endregion

        public void OnIteractionWithATM(GameObject collectableGameObject)
        {

        }
        public void OnIteractionWithCollectable(GameObject collectableGameObject)
        {
            AddStackList(collectableGameObject);
        }
        public void OnIteractionWithObstacle(GameObject collectableGameObject)
        {

        }













        public void AddStackList(GameObject collectableGameObject)
        {
            CollectableStack.Add(collectableGameObject);
        }

        public void RemoveStackList()
        {

        }

        public void StackCollectableRemove()
        {

        }

        public void LerpFunction()
        {

        }

        public void OnReset()
        {

        }
    }
}

