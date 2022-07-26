using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers
{
    [Serializable]
    public class LevelPanelController
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;
   

        #endregion

        #endregion


        public void SetLevelText(int value)
        {
            levelText.text ="Level "+value;
         
        }

    
    }
}