using UnityEngine;
using System;
using Enums;
using Extentions;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public SaveGameDataParams SaveGameDataParams;

        #endregion
        #region Private Variables
        private float _money;

        #endregion
        #endregion

        #region EventSubscribtion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData += SaveData;
            ScoreSignals.Instance.onSendMoney += SetMoney;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData -= SaveData;
            ScoreSignals.Instance.onSendMoney -= SetMoney;
            LevelSignals.Instance.onLevelSuccessful -= SaveData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        } 
        #endregion

        private void SetMoney(float value)
        {
            _money = value;
        }

        private void SaveData()
        {
            OnSaveGame(
                new SaveGameDataParams()
                {
                    Money = SaveSignals.Instance.onGetMoney(),
                    Level = SaveSignals.Instance.onGetLevelID()
                }
            );
        }

        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level);
            if (saveDataParams.Money != null) ES3.Save("Money", saveDataParams.Money);
        }
    }
}