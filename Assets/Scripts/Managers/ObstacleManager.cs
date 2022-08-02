using UnityEngine;
using Signals;
using DG.Tweening;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private DOTweenAnimation _obstacleAnim;

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
            _obstacleAnim = this.GetComponent<DOTweenAnimation>();
        }

        private void OnObstacleAnimationStart()
        {
            
            _obstacleAnim.DOPlay();
        }  
    }
}