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
    private int _score=0;
    private int _scoreCache = 0;
    private int _atmScoreValue = 0;
    private int _atmScore=0;

    
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
    }

    private void UnSubscriptionEvent()
    {

        ScoreSignals.Instance.onSetScore -= OnSetScore;
        ScoreSignals.Instance.onSetAtmScore -= OnSetAtmScore;

    }
    
    private void OnDisable()
    {
        UnSubscriptionEvent();
    }

    #endregion

    public void OnSetScore(int setScore)
    {

        _score = setScore + _atmScoreValue;
        ScoreSignals.Instance.onSetPlayerScoreText?.Invoke(_score);


    }
    private void OnSetAtmScore(int atmValues)
    {
        _atmScoreValue += atmValues;
        ScoreSignals.Instance.onSetAtmScoreText?.Invoke(_atmScoreValue);
    }
    
    
}
