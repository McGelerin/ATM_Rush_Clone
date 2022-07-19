using System;
using Enums;
using Extentions;
using Keys;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

   

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
       
        CoreGameSignals.Instance.onSaveGameData += onSaveGame;
    }

    private void UnsubscribeEvents()
    {
     
        CoreGameSignals.Instance.onSaveGameData -= onSaveGame;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    

    private void onSaveGame(SaveGameDataParams saveDataParams)
    {
        if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level);
        if (saveDataParams.Coin != null) ES3.Save("Coin", saveDataParams.Coin);
        if (saveDataParams.SFX != null) ES3.Save("SFX", saveDataParams.SFX);
        if (saveDataParams.VFX != null) ES3.Save("VFX", saveDataParams.VFX);
        if (saveDataParams.Haptic != null) ES3.Save("Haptic", saveDataParams.Haptic);
    }
}