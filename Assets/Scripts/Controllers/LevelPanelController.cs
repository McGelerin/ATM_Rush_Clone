using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelTextLeft, levelTextRight;
        [SerializeField] private List<Image> stageImages;

        #endregion

        #endregion


        public void SetLevelText(int value)
        {
            levelTextLeft.text = value.ToString();
            levelTextRight.text = (value + 1).ToString();
        }

        public void UpdateStageData(int value)
        {
            stageImages[value].DOColor(Color.green, .5f).SetEase(Ease.Linear);
        }
    }
}