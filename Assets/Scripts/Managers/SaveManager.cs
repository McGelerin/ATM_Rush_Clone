using UnityEngine;
using Keys;
using Signals;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region EventSubscribtion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData += SaveData;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData -= SaveData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        } 
        #endregion

        private void SaveData()
        {
            OnSaveGame(
                new SaveGameDataParams()
                {
                    Money = SaveSignals.Instance.onGetMoney(),
                    Level = SaveSignals.Instance.onGetLevelID(),
                    IncomeLevel = CoreGameSignals.Instance.onGetIncomeLevel(),
                    StackLevel = CoreGameSignals.Instance.onGetStackLevel() 
                }
            );
        }

        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level);
            if (saveDataParams.Money != null) ES3.Save("Money", saveDataParams.Money);
            if (saveDataParams.IncomeLevel != null) ES3.Save("IncomeLevel", saveDataParams.IncomeLevel);
            if (saveDataParams.StackLevel != null) ES3.Save("StackLevel", saveDataParams.StackLevel);
        }
    }
}