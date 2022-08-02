using Signals;
using UnityEngine;

namespace Managers
{
    public class FeatureManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _incomeLevel=1;
        private int _stackLevel=1;
        private float _newmoney;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            Subscription();
        }

        private void Subscription()
        {
            CoreGameSignals.Instance.onClickIncome += OnClickIncome;
            CoreGameSignals.Instance.onClickStack += OnClickStack;
            CoreGameSignals.Instance.onGetIncomeLevel += OnGetIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel += OnGetStackLevel;
        }


        private void UnSubscription()
        {
            CoreGameSignals.Instance.onClickIncome -= OnClickIncome;
            CoreGameSignals.Instance.onClickStack -= OnClickStack;
            CoreGameSignals.Instance.onGetIncomeLevel -= OnGetIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel -= OnGetStackLevel;
        }

        private void OnDisable()
        {
            UnSubscription();
        }

        #endregion

        private void Awake()
        {
            _incomeLevel = LoadIncomeData();
            _stackLevel = LoadStackData();
        }

        private int OnGetIncomeLevel() => _incomeLevel;
        private int OnGetStackLevel() => _stackLevel;

        private int LoadIncomeData()
        {
            if (!ES3.FileExists()) return 1;
            return ES3.KeyExists("IncomeLevel") ? ES3.Load<int>("IncomeLevel") : 1;
        } 

        private int LoadStackData()
        {
            if (!ES3.FileExists()) return 1;
            return ES3.KeyExists("StackLevel") ? ES3.Load<int>("StackLevel") : 1;
        }

        private void OnClickIncome()
        {
            _newmoney = (SaveSignals.Instance.onGetMoney()-((Mathf.Pow(2,_incomeLevel)*100)));
            _incomeLevel+=1;
            ScoreSignals.Instance.onSendMoney?.Invoke(_newmoney);
            SaveFeatureData();
        }  

        private void OnClickStack()
        {
            _newmoney = (SaveSignals.Instance.onGetMoney()-((Mathf.Pow(2,_stackLevel) * 100)));
            _stackLevel+=1;
            ScoreSignals.Instance.onSendMoney?.Invoke(_newmoney);
            SaveFeatureData();
        }

        private void SaveFeatureData()
        {
            SaveSignals.Instance.onSaveGameData?.Invoke();
        }
    }
}