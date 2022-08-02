using System;
using Signals;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Private Variables

    private float _money;
    private int _stackValueMultiplier;
    private int _scoreCache = 0;
    private int _atmScoreValue = 0;

    #endregion

    #endregion

    #region EventSubscribtion

    private void OnEnable()
    {
        SubscriptionEvent();
    }

    private void SubscriptionEvent()
    {
        ScoreSignals.Instance.onSetScore += OnSetScore;
        ScoreSignals.Instance.onSetAtmScore += OnSetAtmScore;
        CoreGameSignals.Instance.onReset += OnReset;
        CoreGameSignals.Instance.onMiniGameStart += SendFinalScore;
        LevelSignals.Instance.onLevelSuccessful += RefreshMoney;
        SaveSignals.Instance.onGetMoney += OnGetMoney;
        ScoreSignals.Instance.onSendMoney += FeatureSetMoney;
        CoreGameSignals.Instance.onClickIncome += SetValueMultipler;
    }

    private void UnSubscriptionEvent()
    {
        ScoreSignals.Instance.onSetScore -= OnSetScore;
        ScoreSignals.Instance.onSetAtmScore -= OnSetAtmScore;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onMiniGameStart -= SendFinalScore;
        LevelSignals.Instance.onLevelSuccessful -= RefreshMoney;
        SaveSignals.Instance.onGetMoney -= OnGetMoney;
        ScoreSignals.Instance.onSendMoney -= FeatureSetMoney;
        CoreGameSignals.Instance.onClickIncome -= SetValueMultipler;
    }

    private void OnDisable()
    {
        UnSubscriptionEvent();
    }

    #endregion
    private void Awake()
    {
        _money = SetMoney();
        SetValueMultipler();
        RefreshMoney();
    }
    public void OnSetScore(int setScore)
    {
        _scoreCache = (setScore * _stackValueMultiplier) + _atmScoreValue;
        ScoreSignals.Instance.onSetTotalScore?.Invoke(_scoreCache);
    }

    private void OnSetAtmScore(int atmValues)
    {
        _atmScoreValue += atmValues * _stackValueMultiplier;
        ScoreSignals.Instance.onSetAtmScoreText?.Invoke(_atmScoreValue);
    }

    private void SendFinalScore()
    {
        ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
    }

    private float SetMoney()
    {
        if (!ES3.FileExists()) return 0;
        return ES3.KeyExists("Money") ? ES3.Load<float>("Money") : 0;
    }
    private float OnGetMoney()
    {
        return _money;
    }
    private void RefreshMoney()
    {
        _money += _scoreCache * ScoreSignals.Instance.onGetMultiplier();
        ScoreSignals.Instance.onSendMoney?.Invoke(_money);
    }
    private void FeatureSetMoney(float money)
    {
        _money = money;
    }

    private void SetValueMultipler()
    {
        _stackValueMultiplier = CoreGameSignals.Instance.onGetIncomeLevel();
    }
    private void OnReset()
    {
        _scoreCache = 0;
        _atmScoreValue = 0;
    }
}