using System;
using UnityEngine;
using Signals;
using DG.Tweening;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private DOTweenAnimation _obAnim;

        #endregion

        
        #endregion
        
        #region Event Subscriptions

        private void OnEnable()
                {
                    SubscribeEvents();
                }
                private void SubscribeEvents()
                {
                    CoreGameSignals.Instance.onPlay += OnObstacleAnimationStart;
                }
        
                private void UnsubscribeEvents()
                {
                    CoreGameSignals.Instance.onPlay -= OnObstacleAnimationStart;
                }
        
                private void OnDisable()
                {
                    UnsubscribeEvents();
                }
        

        #endregion
        
        
        
        private void Awake()
        {
            _obAnim = this.GetComponent<DOTweenAnimation>();
        }

        private void OnObstacleAnimationStart()
        {
            Debug.Log("Running");
            _obAnim.DOPlay();
        }  
    }
}