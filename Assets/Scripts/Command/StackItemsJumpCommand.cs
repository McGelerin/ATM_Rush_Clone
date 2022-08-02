using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class StackItemsJumpCommand
    {
        #region Self Variables

        #region Private Variables
        private List<GameObject> _collectableStack;
        private StackData _stackData;
        private GameObject _levelHolder;

        #endregion

        #endregion
        public StackItemsJumpCommand(ref List<GameObject> collectableStack,StackData stackData,GameObject levelHolder)
        {
            _collectableStack = collectableStack;
            _stackData = stackData;
            _levelHolder = levelHolder;

        }
        public void ItemsJump(int last, int index)
        {
            for (int i = last; i > index; i--)
            {
                _collectableStack[i].transform.GetChild(1).tag = "Collectable";
                _collectableStack[i].transform.SetParent(_levelHolder.transform.GetChild(0));
                _collectableStack[i].transform.DOJump(
                    new Vector3(
                        Random.Range(-_stackData.JumpItemsClampX, _stackData.JumpItemsClampX + 1), //Ust Sinir Dahil Degil
                        _collectableStack[i].transform.position.y,
                        _collectableStack[i].transform.position.z + Random.Range(10, 15)),
                    _stackData.JumpForce,
                    Random.Range(1, 3), 0.7f
                );
                _collectableStack[i].transform.DOScale(Vector3.one, 0);
                _collectableStack.RemoveAt(i);
                _collectableStack.TrimExcess();
            }
        }
    }
}