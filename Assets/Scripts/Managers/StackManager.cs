using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Signals;
using DG.Tweening;
using Sirenix.OdinInspector;
using Data.UnityObject;
using Data.ValueObject;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public StackData StackData;
        public List<GameObject> CollectableStack = new List<GameObject>();

        #endregion

        #region Private Variables

        [ShowInInspector] private int _totalListScore;
        private StackMoveController _stackMoveController;
        private bool _lastCheck;

        #endregion

        #region Seralized Veriables

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private GameObject money;

        #endregion

        #endregion
        private void Awake()
        {
            StackData = GetStackData();
            _stackMoveController = new StackMoveController();
            _stackMoveController.InisializedController(StackData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle += OnInteractionWithObstacle;
            StackSignals.Instance.onInteractionATM += OnInteractionWithATM;
            StackSignals.Instance.onStackFollowPlayer += OnStackMove;
            StackSignals.Instance.onUpdateType += StackValuesUpdate;
            StackSignals.Instance.onInteractionConveyor += OnInteractionWithConveyor;
        }

        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
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

        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_StackData").StackData;

        private void OnInteractionWithATM(GameObject collectableGameObject)
        {
            ScoreSignals.Instance.onSetAtmScore?.Invoke((int)collectableGameObject.GetComponent<CollectableManager>()
                .CollectableTypeValue + 1);
            if (_lastCheck == false)
            {
                RemoveStackListItems(collectableGameObject);
            }
            else
            {
                collectableGameObject.SetActive(false);
            }
        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            AddStackList(collectableGameObject);
            StartCoroutine(StackItemsShackAnim());
            StackValuesUpdate();
        }

        private void OnInteractionWithObstacle(GameObject collectableGameObject)
        {
            RemoveStackListItems(collectableGameObject);
        }
        private void AddStackList(GameObject collectableGameObject)
        {
            if (CollectableStack.Count == 0)
            {
                CollectableStack.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(transform);
                collectableGameObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                collectableGameObject.transform.SetParent(transform);
                Vector3 newPos = CollectableStack[CollectableStack.Count - 1].transform.localPosition;
                newPos.z += StackData.CollectableOffsetInStack;
                collectableGameObject.transform.localPosition = newPos;
                CollectableStack.Add(collectableGameObject);
            }
        }
        IEnumerator StackItemsShackAnim()
        {
            for (int i = 0; i <= CollectableStack.Count - 1; i++)
            {
                int index = (CollectableStack.Count - 1) - i;
                CollectableStack[index].transform.DOScale(new Vector3(2, 2, 2), 0.14f).SetEase(Ease.Flash);
                CollectableStack[index].transform.DOScale(Vector3.one, 0.14f).SetDelay(0.14f).SetEase(Ease.Flash);
                yield return new WaitForSeconds(0.05f);
            }
        }
        private void RemoveStackListItems(GameObject collectableGameObject)
        {
            int index = CollectableStack.IndexOf(collectableGameObject);
            int last = CollectableStack.Count - 1;
            collectableGameObject.transform.SetParent(levelHolder.transform.GetChild(0));
            collectableGameObject.SetActive(false);
            ItemsJump(last, index);
            CollectableStack.RemoveAt(index);
            CollectableStack.TrimExcess();
            StackValuesUpdate();
        }

        private void ItemsJump(int last, int index)
        {
            for (int i = last; i > index; i--)
            {
                CollectableStack[i].transform.GetChild(1).tag = "Collectable";
                CollectableStack[i].transform.SetParent(levelHolder.transform.GetChild(0));
                CollectableStack[i].transform.DOJump(
                    new Vector3(
                        Random.Range(-StackData.JumpItemsClampX, StackData.JumpItemsClampX + 1), //Ust Sinir Dahil Degil
                        CollectableStack[i].transform.position.y,
                        CollectableStack[i].transform.position.z + Random.Range(10, 15)),
                    StackData.JumpForce,
                    Random.Range(1, 3), 0.7f
                );
                CollectableStack[i].transform.DOScale(Vector3.one, 0);
                CollectableStack.RemoveAt(i);
                CollectableStack.TrimExcess();
            }
        }

        private void OnStackMove(Vector2 direction)
        {
            transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 2f);
            if (gameObject.transform.childCount > 0)
            {
                _stackMoveController.StackItemsMoveOrigin(direction.x, CollectableStack);
            }
        }

        private void StackValuesUpdate()
        {
            _totalListScore = 0;
            foreach (var Items in CollectableStack)
            {
                _totalListScore += (int)Items.GetComponent<CollectableManager>().CollectableTypeValue + 1;
            }
            ScoreSignals.Instance.onSetScore?.Invoke(_totalListScore);
        }

        private void OnInteractionWithConveyor()
        {
            _lastCheck = true;
            int i = CollectableStack.Count - 1;
            CollectableStack[i].transform.SetParent(levelHolder.transform.GetChild(0));
            CollectableStack[i].transform.DOScale(Vector3.zero, 2.5f);
            CollectableStack[i].transform.DOMove(new Vector3(-10, 2, CollectableStack[i].transform.position.z), 1.5f);
            CollectableStack.RemoveAt(i);
            CollectableStack.TrimExcess();
        }

        private void InitialzeStack()
        {
            for (int i = 1; i < CoreGameSignals.Instance.onGetStackLevel(); i++)
            {
                GameObject obj= Instantiate(money);
                AddStackList(obj);
            }
            StackValuesUpdate();
        }

        private void OnPlay()
        {
            _lastCheck = false;
            InitialzeStack();
            
        }

        private void OnReset()
        {
            foreach (Transform childs in transform)
            {
                Destroy(childs.gameObject);
            }
            CollectableStack.Clear();
        }
    }
}