using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

namespace Command
{
    public class StackInteractionWithConveyorCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _collectableStack;
        private GameObject _levelHolder;
        private StackManager _manager;

        #endregion

        #endregion

        public StackInteractionWithConveyorCommand(ref List<GameObject> collectableStack, GameObject levelHolder,
            StackManager manager)
        {
            _collectableStack = collectableStack;
            _levelHolder = levelHolder;
            _manager = manager;
        }

        public void OnInteractionWithConveyor()
        {
            _manager.LastCheck = true;
            int i = _collectableStack.Count - 1;
            _collectableStack[i].transform.SetParent(_levelHolder.transform.GetChild(0));
            _collectableStack[i].transform.DOScale(Vector3.zero, 2.5f);
            _collectableStack[i].transform.DOMove(new Vector3(-10, 2, _collectableStack[i].transform.position.z), 1.5f);
            _collectableStack.RemoveAt(i);
            _collectableStack.TrimExcess();
        }
    }
}