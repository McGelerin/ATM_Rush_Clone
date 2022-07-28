using System;
using Cinemachine;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            GetInitialPosition();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void GetInitialPosition()
        {
            _initialPosition = transform.position;
        }

        private void OnMoveToInitialPosition()
        {
            transform.position = _initialPosition;
        }

        private void SetCameraTarget()
        {
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
           
            virtualCamera.Follow = playerManager;
            
         
        }

        private void OnReset()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }
    }
}