using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Private Veriables

        [ShowInInspector] private List<GameObject> _collectableStack = new List<GameObject>();
        private List<GameObject> _collectableStackValues = new List<GameObject>();

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
            StackSignals.Instance.onStackFollowPlayer += OnStackMove;
        }

        private void UnSubscribeEvent()
        {
            StackSignals.Instance.onInteractionCollectable -= OnIteractionWithCollectable;
            StackSignals.Instance.onIteractionObstacle -= OnIteractionWithObstacle;
            StackSignals.Instance.onInteractionATM -= OnIteractionWithATM;
            StackSignals.Instance.onStackFollowPlayer -= OnStackMove;
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
            StartCoroutine(StackItemsShackAnim());
        }

        private void OnIteractionWithObstacle(GameObject collectableGameObject)
        {
            RemoveStackList(collectableGameObject);
        }

        private void OnStackMove(Vector2 direction)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 4f);
            StackItemsMoveOrigin(direction.x);
        }

        private void AddStackList(GameObject collectableGameObject)
        {
            if (_collectableStack.Count == 0)
            {
                _collectableStack.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(transform);
                collectableGameObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                collectableGameObject.transform.SetParent(transform);
                Vector3 newPos = _collectableStack[_collectableStack.Count - 1].transform.localPosition;
                newPos.z += 1;
                collectableGameObject.transform.localPosition = newPos;
                _collectableStack.Add(collectableGameObject);
            }
        }

        private void RemoveStackList(GameObject collectableGameObject)
        {
            int index = _collectableStack.IndexOf(collectableGameObject);
            int last = _collectableStack.Count - 1;
            Destroy(collectableGameObject);
            for (int i = last; i > index; i--)
            {
                StackSignals.Instance.onRemoveFromStack?.Invoke(_collectableStack[i]);
                _collectableStack[i].transform.DOJump(
                    new Vector3(_collectableStack[i].transform.position.x + Random.Range(-4, 4),
                        _collectableStack[i].transform.position.y,
                        _collectableStack[i].transform.position.z + Random.Range(10, 15)),
                    7f,
                    Random.Range(1, 3),
                    0.7f,
                    false);
                _collectableStack.RemoveAt(i);
            }
            _collectableStack.RemoveAt(index);
            _collectableStack.TrimExcess();
        }

        private void StackItemsMoveOrigin(float directionX)
        {
            if (gameObject.transform.childCount > 0)
            {
                float direct = Mathf.Lerp(_collectableStack[0].transform.localPosition.x, directionX, 0.25f);
                _collectableStack[0].transform.localPosition = new Vector3(direct, 0, 0);
                StackItemsLerpMove();
            }
        }

        private void StackItemsLerpMove()
        {
            for (int i = 1; i < _collectableStack.Count; i++)
            {
                Vector3 pos = _collectableStack[i].transform.localPosition;
                pos.x = _collectableStack[i - 1].transform.localPosition.x;
                float direct = Mathf.Lerp(_collectableStack[i].transform.localPosition.x, pos.x, 0.25f);
                _collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
            }
        }

        IEnumerator StackItemsShackAnim()
        {
            for (int i = _collectableStack.Count - 1; i >= 0; i--)
            {
                int index = i;
                _collectableStack[index].transform.DOScale(new Vector3(2f, 2f, 2f), 0.12f).OnComplete(() =>
                    _collectableStack[index].transform.DOScale(Vector3.one, 0.12f)
                );
                yield return new WaitForSeconds(0.04f);
            }
        }

        public void OnReset()
        {
        }
    }
}