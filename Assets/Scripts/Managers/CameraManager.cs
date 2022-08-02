using Cinemachine;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        #region Public variables
        public CameraStates CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }

        #endregion
        #region Serialized Variables

        [SerializeField]private CinemachineVirtualCamera virtualCamera;
        [SerializeField]private GameObject fakePlayer;
        #endregion
        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.InitializeCamera;
        private Animator _camAnimator;

        #endregion
        #endregion

        private void Awake()
        {
            virtualCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            _camAnimator = GetComponent<Animator>();
            GetInitialPosition();
        }

        #region Event Subscriptions
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
            if (CameraStateController == CameraStates.InitializeCamera)
            {
                _camAnimator.Play("InitializeCamera");
            }
            else if (CameraStateController == CameraStates.PlayerCamera)
            {
                _camAnimator.Play("PlayerCamera");
            }
            else if (CameraStateController == CameraStates.MiniGameCamera)
            {
                virtualCamera = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
                virtualCamera.Follow = fakePlayer.transform;
                _camAnimator.Play("MiniGameCamera");
            }
        }

        private void GetInitialPosition()
        {
            _initialPosition = virtualCamera.transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            virtualCamera.transform.localPosition = _initialPosition;
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            virtualCamera.Follow = playerManager;
            CameraStateController = CameraStates.PlayerCamera;
        }

        private void OnMiniGame()
        {
            CameraStateController = CameraStates.MiniGameCamera;
        }

        private void OnNextLevel()
        {
            CameraStateController = CameraStates.InitializeCamera;
        }

        private void OnReset()
        {
            CameraStateController = CameraStates.InitializeCamera;
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            virtualCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            OnMoveToInitialPosition();
        }
    }
}