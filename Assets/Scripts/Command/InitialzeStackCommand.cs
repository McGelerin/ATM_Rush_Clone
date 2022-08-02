using UnityEngine;
using Managers;
using Signals;


namespace Command
{
    public class InitialzeStackCommand
    {
        #region Self Variables

        #region Private Variables

        private StackManager _manager;
        private GameObject _money;
        #endregion
        #endregion

        public InitialzeStackCommand(GameObject money,StackManager Manager)
        {
            _money = money;
            _manager = Manager;
        }
        public void InitialzeStack()
        {
            for (int i = 1; i < CoreGameSignals.Instance.onGetStackLevel(); i++)
            {
                GameObject obj = Object.Instantiate(_money);
                _manager.ItemAddOnStackCommand.AddStackList(obj);
            }
            _manager.StackValueUpdateCommand.StackValuesUpdate();
        }
    }
}