using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using DG.Tweening;
using Sirenix.OdinInspector;
using Data.UnityObject;
using Data.ValueObject;
using Random = UnityEngine.Random;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public StackData StackData;

        #endregion

        #region Private Veriables

        [ShowInInspector] public List<GameObject> _collectableStack=new List<GameObject>();
        //[ShowInInspector] private List<GameObject> _collectableStackValues=new List<GameObject>();
        [ShowInInspector] private int _totalListScore;
        private bool _lastCheck=false;

        #endregion

        #region Seralized Veriables

        [SerializeField] private GameObject levelHolder;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            StackSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle += OnInteractionWithObstacle;
            StackSignals.Instance.onInteractionATM += OnInteractionWithATM;
            StackSignals.Instance.onStackFollowPlayer += OnStackMove;
            StackSignals.Instance.onUpdateType += StackValuesUpdate;
            StackSignals.Instance.onInteractionConveyor += OnInteractionWithConveyor;
        }

        private void UnSubscribeEvent()
        {
            StackSignals.Instance.onInteractionCollectable -= OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle -= OnInteractionWithObstacle;
            StackSignals.Instance.onInteractionATM -= OnInteractionWithATM;
            StackSignals.Instance.onStackFollowPlayer -= OnStackMove;
            StackSignals.Instance.onUpdateType -= StackValuesUpdate;
            StackSignals.Instance.onInteractionConveyor -= OnInteractionWithConveyor;

        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Awake()
        {
            StackData = GetStackData();
        }

        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_StackData").StackData;

        private void OnInteractionWithATM(GameObject collectableGameObject)
        {
            ScoreSignals.Instance.onSetAtmScore?.Invoke((int)collectableGameObject.GetComponent<CollectableManager>().CollectableTypeValue+1);
            if (_lastCheck == false)
            {
                RemoveStackListItems(collectableGameObject);
            }
            else
            {
                collectableGameObject.SetActive(false);
                _lastCheck=true ;
            }

            
        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            AddStackList(collectableGameObject);
            StartCoroutine(StackItemsShackAnim());
            //StackItemsShackAnim();
            StackValuesUpdate();
        }

        private void OnInteractionWithObstacle(GameObject collectableGameObject)
        {
            RemoveStackListItems(collectableGameObject);
            //StopAllCoroutines();
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
                newPos.z += StackData.CollectableOffsetInStack;
                collectableGameObject.transform.localPosition = newPos;
                _collectableStack.Add(collectableGameObject);
            }
        }
        //private void StackItemsShackAnim()
        IEnumerator StackItemsShackAnim()
        {
            for (int i = 0; i <= _collectableStack.Count - 1; i++)
            {
                int index = (_collectableStack.Count - 1) - i;
                _collectableStack[index].transform.DOScale(new Vector3(4, 4, 4), 0.20f);
                _collectableStack[index].transform.DOScale(Vector3.one, 0.4f);
                yield return new WaitForSeconds(0.05f);
            
            }
        }

        private void RemoveStackListItems(GameObject collectableGameObject)
        {
            int index = _collectableStack.IndexOf(collectableGameObject);
            int last = _collectableStack.Count - 1;
            collectableGameObject.transform.SetParent(levelHolder.transform.GetChild(0));
            collectableGameObject.SetActive(false);
            //Destroy(collectableGameObject);
            //StopAllCoroutines();
            for (int i = last; i > index; i--)
            {
                // StackSignals.Instance.onRemoveFromStack?.Invoke(_collectableStack[i]);
                _collectableStack[i].transform.GetChild(1).tag = "Collectable";
                _collectableStack[i].transform.SetParent(levelHolder.transform.GetChild(0));
                _collectableStack[i].transform.DOJump(
                    new Vector3(
                        Random.Range(-StackData.JumpItemsClampX, StackData.JumpItemsClampX + 1), //Ust Sinir Dahil Degil
                        _collectableStack[i].transform.position.y,
                        _collectableStack[i].transform.position.z + Random.Range(10, 15)),
                    StackData.JumpForce,
                    Random.Range(1, 3), 0.7f
                );
                _collectableStack[i].transform.DOScale(Vector3.one, 0);
                _collectableStack.RemoveAt(i);
                _collectableStack.TrimExcess();
            }
            _collectableStack.RemoveAt(index);
            _collectableStack.TrimExcess();
            StackValuesUpdate();
        }
        private void OnStackMove(Vector2 direction)
        {
            transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 4f);
            StackItemsMoveOrigin(direction.x);
        }
        private void StackItemsMoveOrigin(float directionX)
        {
            if (gameObject.transform.childCount > 0)
            {
                float direct = Mathf.Lerp(_collectableStack[0].transform.localPosition.x, directionX,
                    StackData.LerpSpeed);
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
                float direct = Mathf.Lerp(_collectableStack[i].transform.localPosition.x, pos.x, StackData.LerpSpeed);
                _collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
            }
        }

        private void StackValuesUpdate()
        {
            _totalListScore = 0;
            foreach (var Items in _collectableStack)
            {
                _totalListScore += (int)Items.GetComponent<CollectableManager>().CollectableTypeValue+1;
            }
            ScoreSignals.Instance.onSetScore?.Invoke(_totalListScore);
        }

        private void OnInteractionWithConveyor()
        {
            _lastCheck = true;
            int i = _collectableStack.Count - 1;
            _collectableStack[i].transform.SetParent(levelHolder.transform.GetChild(0));
            _collectableStack[i].transform.DOScale(Vector3.zero,2.5f);
            _collectableStack[i].transform.DOMoveX(-10f, 1f, false);
            _collectableStack.RemoveAt(i);
            _collectableStack.TrimExcess();
        }
        
        public void OnReset()
        {
        }
    }
}