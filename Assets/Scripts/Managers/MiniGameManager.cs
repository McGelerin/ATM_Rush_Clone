using System;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Veriables

        #region Private Veriables

        #endregion

        #region Public Veriables

        #endregion

        #region Serialized Veriables

        #endregion

        #endregion

        #region Event Subscribtion

        private void OnEnable()
        {
            Subcribtion();
        }

        private void Subcribtion()
        {
            CoreGameSignals.Instance.onMiniGameStart += OnMiniGameStart;
        }

        private void UnSubcribtion()
        {
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGameStart;
        }

        private void OnDisable()
        {
            UnSubcribtion();
        }

        #endregion


        private void OnMiniGameStart()
        {
                
        }
    }
}