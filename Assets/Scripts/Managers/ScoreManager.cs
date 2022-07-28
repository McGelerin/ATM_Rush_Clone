using System;
using System.Collections;
using System.Collections.Generic;
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

    private int _score = 0;
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
        CoreGameSignals.Instance.onReset += OnPlay;
        CoreGameSignals.Instance.onReset += OnReset;
    }


    private void UnSubscriptionEvent()
    {
        ScoreSignals.Instance.onSetScore -= OnSetScore;
        ScoreSignals.Instance.onSetAtmScore -= OnSetAtmScore;
        CoreGameSignals.Instance.onReset -= OnPlay;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscriptionEvent();
    }

    #endregion

    private void Awake()
    {
        OnPlay();
        OnReset();
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

    private void OnPlay()
    {
        if (!ES3.FileExists()) _score = 0;
        else
        {
            _score = ES3.KeyExists("Coin") ? ES3.Load<int>("Coin") : 0;
        }
    }

    private void OnReset()
    {
        _scoreCache = 0;
        _atmScoreValue = 0;
        _atmScore = 0;
    }
}