using System;
using System.Collections;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
          #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private TextMeshPro scoreText;
        
        #endregion

        #endregion


        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
           
          
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            ScoreSignals.Instance.onSetTotalScore += OnSetScoreText;
            CoreGameSignals.Instance.onConveyor += OnConveyor;

        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            ScoreSignals.Instance.onSetTotalScore -= OnSetScoreText;
            CoreGameSignals.Instance.onConveyor -= OnConveyor;

        
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Movement Controller

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
            
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
           
        }

        #endregion

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
            animationController.Playanim(PlayerAnimationStates.Run);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);

        }
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        public void SetStackPosition()
        {
            Vector2 pos = new Vector2(transform.position.x,transform.position.z);
            StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
        }

        private void OnReset()
        {
            movementController.OnReset();
            animationController.OnReset();
        }

        private void OnSetScoreText(int Values)
        {
            scoreText.text = Values.ToString();
        }
        private void OnConveyor()
        {
            movementController.IsReadyToPlay(false);
            StartCoroutine(WaitForFinal());
        }

        IEnumerator WaitForFinal()
        {
            animationController.Playanim(animationStates:PlayerAnimationStates.Idle);
            yield return new WaitForSeconds(2f);
            CoreGameSignals.Instance.onMiniGameStart?.Invoke();
        }
    }
}