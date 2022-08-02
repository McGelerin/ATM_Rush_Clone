using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ShopControllerController
    {
         private TextMeshProUGUI _incomeLvlText;
         private TextMeshProUGUI _money;
         private Button _incomeLvlButton;
         private TextMeshProUGUI _incomeValue;
         private Button _stackLvlButton;
         private TextMeshProUGUI _stackLvlText;
         private TextMeshProUGUI _stackValue;
         
        public ShopControllerController(TextMeshProUGUI money,TextMeshProUGUI incomeLvlText,TextMeshProUGUI incomeValue,Button incomeLvlButton,
            TextMeshProUGUI stackLvlText,TextMeshProUGUI stackValue,Button stackLvlButton)
        {
            _money = money;
            _incomeValue = incomeValue;
            _incomeLvlText = incomeLvlText;
            _incomeLvlButton = incomeLvlButton;
            _stackValue = stackValue;
            _stackLvlText = stackLvlText;
            _stackLvlButton = stackLvlButton;

        }
        
        public void ChangesIncomeIntaractable()
        {
            if (int.Parse(_money.text)<int.Parse(_incomeValue.text) || CoreGameSignals.Instance.onGetIncomeLevel() >= 30)
            {
                _incomeLvlButton.interactable=false;
            }

            else
            {
                _incomeLvlButton.interactable=true;
            }
        }

        public void ChangesStackIntaractable()
        {
            if (int.Parse(_money.text) < int.Parse(_stackValue.text) || CoreGameSignals.Instance.onGetStackLevel() >= 15)
            {
                _stackLvlButton.interactable = false;
            }
            else
            {
                _stackLvlButton.interactable = true;
            }
        }

        public void SetIncomeLvlText()
        {
            _incomeLvlText.text = "Income lvl\n"+CoreGameSignals.Instance.onGetIncomeLevel();
            _incomeValue.text = (Mathf.Pow(2,CoreGameSignals.Instance.onGetIncomeLevel()) * 100).ToString();
        }

        public void SetStackLvlText()
        {
            _stackLvlText.text ="Stack lvl\n"+CoreGameSignals.Instance.onGetStackLevel();
            _stackValue.text = (Mathf.Pow(2,CoreGameSignals.Instance.onGetStackLevel()) * 100).ToString();
        }
    }
}