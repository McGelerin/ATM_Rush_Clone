using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Keys;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables

    private float _Money;
    private int _scoreCache = 0;
    private int _atmScoreValue = 0;
    private int _atmScore = 0;

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

    }


    private void UnSubscriptionEvent()
    {
        ScoreSignals.Instance.onSetScore -= OnSetScore;
        ScoreSignals.Instance.onSetAtmScore -= OnSetAtmScore;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onMiniGameStart -= SendFinalScore;
        LevelSignals.Instance.onLevelSuccessful -= RefreshMoney;
        SaveSignals.Instance.onGetMoney -= OnGetMoney;


    }

  

    private void OnDisable()
    {
        UnSubscriptionEvent();
    }

    #endregion

    private void Awake()
    {
        _Money = SetMoney();
    }

    private void Start()
    {
        RefreshMoney();
    }

    public void OnSetScore(int setScore)
    {
        _scoreCache = setScore + _atmScoreValue;
        ScoreSignals.Instance.onSetTotalScore?.Invoke(_scoreCache);
        
    }

    private void OnSetAtmScore(int atmValues)
    {
        _atmScoreValue += atmValues;
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
        return _Money ;
    }
    private void RefreshMoney()
    {
        _Money += _scoreCache * ScoreSignals.Instance.onGetMultiplier();
        ScoreSignals.Instance.onSendMoney?.Invoke(_Money);
    }
    

    private void OnReset()
    {
        _scoreCache = 0;
        _atmScoreValue = 0;
        _atmScore = 0;
    }
}