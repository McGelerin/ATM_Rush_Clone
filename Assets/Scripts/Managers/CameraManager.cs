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

        public CinemachineVirtualCamera VirtualCamera;
        public GameObject FakePlayer;

        public CameraStates CameraController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }

        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.InitializeCamera;
        private Animator _camAnimator;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            VirtualCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
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
            CoreGameSignals.Instance.onPlay += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGame;
            CoreGameSignals.Instance.onPlay -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void SetCameraStates()
        {
            if (CameraController == CameraStates.InitializeCamera)
            {
                _camAnimator.Play("InitializeCamera");
            }
            else if (CameraController == CameraStates.PlayerCamera)
            {
                _camAnimator.Play("PlayerCamera");
            }
            else if (CameraController == CameraStates.MiniGameCamera)
            {
                VirtualCamera = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
                VirtualCamera.Follow = FakePlayer.transform;
                _camAnimator.Play("MiniGameCamera");
            }
        }

        private void GetInitialPosition()
        {
            _initialPosition = VirtualCamera.transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            VirtualCamera.transform.localPosition = _initialPosition;
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            VirtualCamera.Follow = playerManager;
            CameraController = CameraStates.PlayerCamera;
        }

        private void OnMiniGame()
        {
            CameraController = CameraStates.MiniGameCamera;
        }

        private void OnNextLevel()
        {
            CameraController = CameraStates.InitializeCamera;
        }

        private void OnReset()
        {
            CameraController = CameraStates.InitializeCamera;
            VirtualCamera.Follow = null;
            VirtualCamera.LookAt = null;
            VirtualCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            OnMoveToInitialPosition();
        }
    }
}