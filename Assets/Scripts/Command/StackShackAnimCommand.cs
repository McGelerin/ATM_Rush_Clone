using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Command
{
    public class StackShackAnimCommand
    {
        #region Self Variables

        #region Private Variables
        private List<GameObject> _collectableStack;
        private StackData _stackData;

        #endregion

        #endregion
        public StackShackAnimCommand(ref List<GameObject> collectableStack,StackData stackdata)
        {
            _collectableStack = collectableStack;
            _stackData = stackdata;
        }
        
        public  IEnumerator StackItemsShackAnim()
        {
            for (int i = 0; i <= _collectableStack.Count - 1; i++)
            {
                int index = (_collectableStack.Count - 1) - i;
                _collectableStack[index].transform.DOScale(new Vector3(_stackData.ShackScaleValue, _stackData.ShackScaleValue, _stackData.ShackScaleValue), _stackData.ShackAnimDuraction).SetEase(Ease.Flash);
                _collectableStack[index].transform.DOScale(Vector3.one, _stackData.ShackAnimDuraction).SetDelay(_stackData.ShackAnimDuraction).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_stackData.ShackAnimDuraction/3);
            }
        }
    }
}