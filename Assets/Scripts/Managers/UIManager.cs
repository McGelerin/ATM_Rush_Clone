using System.Collections.Generic;
using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [Space (15),Header("Data")]

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

        private UIPanelController _uiPanelController;
        private ShopControllerController _shopControllerController;

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
            _uiPanelController = new UIPanelController();
            _shopControllerController =
                new ShopControllerController(ref money, ref incomeLvlText, ref incomeValue,ref incomeLvlButton,ref stackLvlText,ref stackValue,ref stackLvlButton);
        }

        private void Start()
        {
            SyncShopUi();
        }

        private void OnOpenPanel(UIPanels panelParam)
        {
            _uiPanelController.OpenPanel(panelParam, panels);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            _uiPanelController.ClosePanel(panelParam, panels);
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


        public void IncomeUpdate()
        {
            CoreGameSignals.Instance.onClickIncome?.Invoke();
            _shopControllerController.SetIncomeLvlText();
        }

        public void StackUpdate()
        {
            CoreGameSignals.Instance.onClickStack?.Invoke();
            _shopControllerController.SetStackLvlText();
        }

        private void SyncShopUi()
        {
            _shopControllerController.SetIncomeLvlText();
            _shopControllerController.SetStackLvlText();
            _shopControllerController.ChangesIncomeIntaractable();
            _shopControllerController.ChangesStackIntaractable();
        }
    }
}