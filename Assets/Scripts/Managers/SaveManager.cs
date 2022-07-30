﻿using UnityEngine;
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

        public SaveGameDataParams saveGameDataParams;

        #endregion

        #region Private Variables

        private int _money;
        private int _levelId;

        #endregion

        #endregion


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
        
        private void SetMoney(int value)
        {
            _money = value;
        }

        private void SaveData()
        {
            OnSaveGame(new SaveGameDataParams()
            {
                Money = SaveSignals.Instance.onGetMoney(),
                Level = SaveSignals.Instance.onGetLevelID()
            });
        }


        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level);
            if (saveDataParams.Money != null) ES3.Save("Money", saveDataParams.Money);
        }
    }
}