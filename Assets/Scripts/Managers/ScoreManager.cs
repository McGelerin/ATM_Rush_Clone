using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    public int onSetScore()
    {
        return 0;
    }
    public void onScoreUp()
    {
        
    }
    public void onScoreDown()
    {
        
    }
    public void Reset()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
