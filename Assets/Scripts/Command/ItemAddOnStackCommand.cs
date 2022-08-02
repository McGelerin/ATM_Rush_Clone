using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Command
{
    public class ItemAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

   
        private List<GameObject> _collectableStack;
        private Transform _transform;
        private StackData _stackData;

        #endregion

        #endregion
        
        public ItemAddOnStackCommand(ref List<GameObject> CollectableStack,Transform transform,StackData stackData)
        {
            _collectableStack = CollectableStack;
            _transform = transform;
            _stackData = stackData;
        }
        
        public void AddStackList(GameObject _collectableGameObject)
        {
            if (_collectableStack.Count == 0)
            {
                _collectableStack.Add(_collectableGameObject);
                _collectableGameObject.transform.SetParent(_transform);
                _collectableGameObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                _collectableGameObject.transform.SetParent(_transform);
                Vector3 newPos = _collectableStack[_collectableStack.Count - 1].transform.localPosition;
                newPos.z += _stackData.CollectableOffsetInStack;
                _collectableGameObject.transform.localPosition = newPos;
                _collectableStack.Add(_collectableGameObject);
            }
        }
    }
}