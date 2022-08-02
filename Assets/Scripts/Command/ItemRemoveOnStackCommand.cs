using System.Collections.Generic;
using Data.ValueObject;
using Managers;
using UnityEngine;

namespace Command
{
    public class ItemRemoveOnStackCommand
    {
        #region Self Variables

        #region Private Variables
        private List<GameObject> _collectableStack;
        private GameObject _levelHolder;
        private StackManager _manager;

        #endregion

        #endregion
        
        public ItemRemoveOnStackCommand(ref List<GameObject> CollectableStack,GameObject levelHolger,StackManager manager)
        {
            _collectableStack = CollectableStack;
            _levelHolder = levelHolger;
            _manager = manager;
        }
        public void RemoveStackListItems(GameObject collectableGameObject)
        {
            int index = _collectableStack.IndexOf(collectableGameObject);
            int last = _collectableStack.Count - 1;
            collectableGameObject.transform.SetParent(_levelHolder.transform.GetChild(0));
            collectableGameObject.SetActive(false);
            _manager.StackItemsJumpCommand.ItemsJump(last, index);
            _collectableStack.RemoveAt(index);
            _collectableStack.TrimExcess();
            _manager.StackValueUpdateCommand.StackValuesUpdate();
        }
    }
}