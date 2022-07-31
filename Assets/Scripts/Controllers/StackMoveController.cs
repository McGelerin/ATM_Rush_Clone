using System;
using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Controllers
{
    public class StackMoveController
    {
        #region Self Variables

        #region Private Veriables

        private StackData _stackData;
        #endregion
        #endregion

        public void InisializedController(StackData Stackdata)
        {
            _stackData = Stackdata;
        }

        public void StackItemsMoveOrigin(float directionX, List<GameObject> _collectableStack)
        {
            float direct = Mathf.Lerp(_collectableStack[0].transform.localPosition.x, directionX,
                _stackData.LerpSpeed);
            _collectableStack[0].transform.localPosition = new Vector3(direct, 0, 0);
            StackItemsLerpMove(_collectableStack);
        }

        public void StackItemsLerpMove(List<GameObject> _collectableStack)
        {
            for (int i = 1; i < _collectableStack.Count; i++)
            {
                Vector3 pos = _collectableStack[i].transform.localPosition;
                pos.x = _collectableStack[i - 1].transform.localPosition.x;
                float direct = Mathf.Lerp(_collectableStack[i].transform.localPosition.x, pos.x, _stackData.LerpSpeed);
                _collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
            }
        }
    }
}