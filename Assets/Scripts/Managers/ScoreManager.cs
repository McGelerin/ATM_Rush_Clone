using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class ScoreManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public int Score=0;
    public int AtmScore=0;
    
    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables

    private int _scoreCache = 0;
    
    #endregion

    #endregion

    #region EventSubscribtion

    private void OnEnable()
    {
        SubscriptionEvent();
    }

    private void SubscriptionEvent()
    {
        ScoreSignals.Instance.onScoreUp += OnScoreUp;
        ScoreSignals.Instance.onScoreDown += OnScoreUp;
        ScoreSignals.Instance.onSetScore += OnSetScore;
    }

    private void UnSubscriptionEvent()
    {
        ScoreSignals.Instance.onScoreUp -= OnScoreUp;
        ScoreSignals.Instance.onScoreDown -= OnScoreUp;
        ScoreSignals.Instance.onSetScore -= OnSetScore;
    }
    
    private void OnDisable()
    {
        UnSubscriptionEvent();
    }

    #endregion

    public void OnSetScore(int setScore)
    {
        Score += setScore;
        
    }
    public void OnScoreUp(int scoreValue)
    {
        _scoreCache += scoreValue;
        
    }
    public void OnScoreDown(int scoreValue)
    {
        _scoreCache -= scoreValue;
    }
    public void OnReset()
    {
        
    }
}
