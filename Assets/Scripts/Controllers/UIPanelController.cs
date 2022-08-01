using System;
using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController 
    {
       
        public void OpenPanel(UIPanels panelParam,List<GameObject> panels)
        {
            panels[(int) panelParam].SetActive(true);
        }

        public void ClosePanel(UIPanels panelParam,List<GameObject> panels)
        {
            panels[(int) panelParam].SetActive(false);
        }
    }
}