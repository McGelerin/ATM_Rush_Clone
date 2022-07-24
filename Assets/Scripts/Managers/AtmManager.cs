using System;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region EventSubcribtions

        private void OnEnable()
        {
            Subscripe();
        }

        private void Subscripe()
        {
       
        }

        private void UnSubscripe()
        {
        }

        private void OnDisable()
        {
            UnSubscripe();
        }

        #endregion
    }
}