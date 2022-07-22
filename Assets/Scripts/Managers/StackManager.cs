using System;
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

        private void OnIteractionWithATM(GameObject collectableGameObject)
        {
        }

        private void OnIteractionWithCollectable(GameObject collectableGameObject)
        {
            AddStackList(collectableGameObject);
            StartCoroutine(StackListShackAnim());
        }

        private void OnIteractionWithObstacle(GameObject collectableGameObject)
        {
            RemoveStackList(collectableGameObject);
        }


        private void AddStackList(GameObject collectableGameObject)
        {
            if (CollectableStack.Count == 0)
            {
                CollectableStack.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(transform);
                collectableGameObject.transform.localPosition =
                    new Vector3(transform.position.x, transform.position.y, 4f);
            }
            else
            {
                collectableGameObject.transform.SetParent(transform);
                Vector3 newPos = CollectableStack[CollectableStack.Count - 1].transform.localPosition;
                newPos.z += 1;
                collectableGameObject.transform.localPosition = newPos;
                CollectableStack.Add(collectableGameObject);
            }
        }


        public void RemoveStackList(GameObject collectableGameObject)
        {
            int index = CollectableStack.IndexOf(collectableGameObject);

            for (int i = index; i < CollectableStack.Count; i++)
            {
                StackSignals.Instance.onRemoveFromStack(CollectableStack[i]);
                CollectableStack[i].transform.DOPunchPosition(new Vector3(5f, 5f, 5f), 0.01f, 1, 1);
                // CollectableStack[i].transform.DOJump(new Vector3(5f,5f,5f), 0.01f, 1, 1);
                // CollectableStack[i].transform.SetParent(null);
                // CollectableStack[i].tag="Collectable";
                CollectableStack.Remove(CollectableStack[i]);
            }

            CollectableStack.TrimExcess();
        }

        public void StackCollectableRemove()
        {
        }

        private void LerpFunction(Vector2 direction)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 4f);
            if (gameObject.transform.childCount > 0)
            {
                float direct = Mathf.Lerp(CollectableStack[0].transform.localPosition.x, direction.x, 0.10f);
                CollectableStack[0].transform.localPosition = new Vector3(direct, 0, 0);
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
                float direct = Mathf.Lerp(CollectableStack[i].transform.localPosition.x, pos.x, 0.1f);
                CollectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
            }
        }

        IEnumerator StackListShackAnim()
        {
            for (int i = CollectableStack.Count - 1; i >= 0; i--)
            {
                int index = i;
                CollectableStack[index].transform.DOScale(new Vector3(2f, 2f, 2f), 0.15f).OnComplete(() =>
                    CollectableStack[index].transform.DOScale(Vector3.one, 0.15f)
                );
                yield return new WaitForSeconds(0.07f);
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