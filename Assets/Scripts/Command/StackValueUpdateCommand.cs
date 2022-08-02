using System.Collections.Generic;
using Signals;
using UnityEngine;
namespace Command
{
   
    

    
    public class StackValueUpdateCommand
    {
        #region Self Variables

        #region Private Variables

        private int _totalListScore=0;
        private List<GameObject> _collectableStack;
        #endregion

        #endregion
        public StackValueUpdateCommand(ref List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;

        }
        public void StackValuesUpdate()
        {
            _totalListScore = 0;
            foreach (var Items in _collectableStack)
            {
                _totalListScore += (int)Items.GetComponent<CollectableManager>().CollectableTypeValue + 1;
            }

            ScoreSignals.Instance.onSetScore?.Invoke(_totalListScore);
        }
    }
}