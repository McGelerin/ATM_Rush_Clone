using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using DG.Tweening;
using UnityEngine.UI;


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
            StackSignals.Instance.onStackFollowPlayer += LerpFunction;
        }

        private void UnSubscribeEvent()
        {
            StackSignals.Instance.onInteractionCollectable -= OnIteractionWithCollectable;
            StackSignals.Instance.onIteractionObstacle -= OnIteractionWithObstacle;
            StackSignals.Instance.onInteractionATM -= OnIteractionWithATM;
            StackSignals.Instance.onStackFollowPlayer -= LerpFunction;
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


            if (CollectableStack.Count==0)
            {
                
                CollectableStack.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(transform);
                collectableGameObject.transform.localPosition =new Vector3(transform.position.x,transform.position.y,4f);

            }

            else
            {
                collectableGameObject.transform.SetParent(transform);
              
              
                    Vector3 newPos = CollectableStack[CollectableStack.Count-1].transform.localPosition;
                    newPos.z += 1;
                    collectableGameObject.transform.localPosition=newPos;

             
                
                CollectableStack.Add(collectableGameObject);
            }
                
            
       
        }

        public void deleytest()
        {
            
        }
        public void RemoveStackList()
        {

        }

        public void StackCollectableRemove()
        {

        }

        public void LerpFunction(Vector2 direction)
        {
            gameObject.transform.position = new Vector3(0,gameObject.transform.position.y,direction.y);
            if(gameObject.transform.childCount > 0)
            {
                CollectableStack[0].transform.DOLocalMoveX(direction.x, 0.1f);
                // MoveOrigin();
                MoveListElements(); 
            }
        }

        private void MoveListElements()
        {
            
            for (int i = 1; i < CollectableStack.Count; i++)
            {
                Vector3 pos = CollectableStack[i].transform.localPosition;
                pos.x = CollectableStack[i - 1].transform.localPosition.x;
                CollectableStack[i].transform.DOLocalMove(pos, 0.25f);
            }
        }

        #region useless

        // private void MoveOrigin()
        // {
        //     for (int i = 1; i < CollectableStack.Count; i++)
        //     {
        //         Vector3 pos = CollectableStack[i].transform.localPosition;
        //         pos.x = CollectableStack[0].transform.localPosition.x;
        //         CollectableStack[i].transform.DOLocalMove(pos, 0.5f);
        //     }
        //   
        // }

        #endregion







        public void OnReset()
        {

        }
    }
}

