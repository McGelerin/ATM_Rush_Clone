using System;
using Cinemachine;
using DG.Tweening;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

       public CinemachineVirtualCamera virtualCamera;

        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        private CameraStates _cameraState = CameraStates.InitializeCamera;
        private Animator _camAnimator;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            // virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _camAnimator = GetComponent<Animator>();
            
            GetInitialPosition();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onMiniGameStart += OnMiniGame;
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGame;
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

     

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void SetCameraStates(CameraStates cameraStates)
        {
            if (cameraStates == CameraStates.InitializeCamera)
            {
                _camAnimator.Play("PlayerCamera");
                cameraStates = CameraStates.PlayerCamera;
            } 
            else if (cameraStates == CameraStates.PlayerCamera)
            {
                _camAnimator.Play("MiniGameCamera");
                cameraStates = CameraStates.MiniGameCamera;
            }  
            else if (cameraStates == CameraStates.MiniGameCamera)
            {
                _camAnimator.Play(CameraStates.InitializeCamera.ToString());
            }
            
        }
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
            SetCameraStates(_cameraState);

        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
           
            virtualCamera.Follow = playerManager;
            
         
        }
        private void OnMiniGame()
        {

        }
        private void OnReset()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }
    }
}