using System;
using System.Collections.Generic;
using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [Space (15),Header("Data")]

        // [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private TextMeshProUGUI money;
        [SerializeField] private List<GameObject> panels;
        [SerializeField] private TextMeshProUGUI levelText;
        [Space (15),Header("Income")]
        [SerializeField] private TextMeshProUGUI incomeLvlText;
        [SerializeField] private Button incomeLvlButton;
        [SerializeField] private TextMeshProUGUI incomeValue;
        [Space (15),Header("Stack")]
        [SerializeField] private Button stackLvlButton;
        [SerializeField] private TextMeshProUGUI stackLvlText;
        [SerializeField] private TextMeshProUGUI stackValue;
        #endregion

        #region Private Variables

        private UIPanelController uiPanelController;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            ScoreSignals.Instance.onSendMoney += SetMoneyText;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            ScoreSignals.Instance.onSendMoney -= SetMoneyText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
        {
            uiPanelController = new UIPanelController();
        }

        private void Start()
        {
            SyncShopUi();
        }

      

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam, panels);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam, panels);
        }

        private void SetMoneyText(float value)
        {
            money.text = ((int)value).ToString();
             SyncShopUi();
        }

        private void OnSetLevelText(int value)
        {
            levelText.text = "Level " + (value + 1);
        }

        private void SetIncomeLvlText()
        {
            incomeLvlText.text = "Income lvl\n"+CoreGameSignals.Instance.onGetIncomeLevel();
            incomeValue.text = (250 + (CoreGameSignals.Instance.onGetIncomeLevel() * 100)).ToString();
        }

        private void SetStackLvlText()
        {
            stackLvlText.text ="Stack lvl\n"+CoreGameSignals.Instance.onGetStackLevel();
            stackValue.text = (250 + (CoreGameSignals.Instance.onGetStackLevel() * 100)).ToString();
            
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.Shop);

        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            LevelSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.Shop);
        }

        public void RestartLevel()
        {
            LevelSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.Shop);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        private void SyncShopUi()
        {
            SetIncomeLvlText();
            SetStackLvlText();
            ChangesIncomeIntaractable();
            ChangesStackIntaractable();
        }
        public void IncomeUpdate()
        {
            CoreGameSignals.Instance.onClickIncome?.Invoke();
            SetIncomeLvlText();
        }

        public void StackUpdate()
        {
            CoreGameSignals.Instance.onClickStack?.Invoke();
            SetStackLvlText();
        }
        
        private void ChangesIncomeIntaractable()
        {
            if (int.Parse(money.text)<int.Parse(incomeValue.text) && CoreGameSignals.Instance.onGetIncomeLevel()<=99)
            {
                incomeLvlButton.interactable=false;
            }

            else
            {
                incomeLvlButton.interactable=true;

            }
            
        }
        private void ChangesStackIntaractable()
        {
            if (int.Parse(money.text) < int.Parse(stackValue.text) && CoreGameSignals.Instance.onGetStackLevel()<=15)
            {
                stackLvlButton.interactable = false;
            }
            else
            {
                stackLvlButton.interactable = true;
            }
        }
    }
}