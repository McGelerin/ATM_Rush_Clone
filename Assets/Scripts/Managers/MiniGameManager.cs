using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Veriables

        #region Private Veriables

        private int _score = 0;
        private Vector3 _initializePos;
        #endregion

        #region Public Veriables

        #endregion

        #region Serialized Veriables

        [SerializeField] private GameObject wallStage;
        [SerializeField] private GameObject fakeMoney;
        [SerializeField] private Transform fakeObject;
        #endregion

        #endregion

        #region Event Subscribtion

        private void OnEnable()
        {
            Subcribtion();
        }

        private void Subcribtion()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onMiniGameStart += OnMiniGameStart;
            ScoreSignals.Instance.onSendScore += OnSendScore;
        }

        private void UnSubcribtion()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGameStart;
            ScoreSignals.Instance.onSendScore -= OnSendScore;

        }

        private void OnDisable()
        {
            UnSubcribtion();
        }

        #endregion

        private void Awake()
        {
            NewWallStage();
            InitializeFake();
            _initializePos = transform.GetChild(0).localPosition;
        }

        private void NewWallStage()
        {
            for (int i = 0; i <= 90; i++)
            {
              var ob=  Instantiate(wallStage, transform);
              ob.transform.localPosition = new Vector3(0,i*10 , 0);
              ob.transform.GetChild(0).GetComponent<TextMeshPro>().text ="x"+(( i / 10f)+1f);
            }
        }

        private void InitializeFake()
        {
            for (int i = 0; i < 15; i++)    
            {
                var ob=  Instantiate(fakeMoney,fakeObject);
                ob.transform.localPosition = new Vector3(0,-i*1.58f , -7);
            }
        }

        private void OnSendScore(int scoreValue)
        {
            _score = scoreValue;
        }

        private void OnMiniGameStart()
        {
            fakeObject.gameObject.SetActive(true);
            StartCoroutine(GoUp());
        }

        IEnumerator GoUp()
        {
            yield return new WaitForSeconds(1f);
            transform.GetChild(0).DOLocalMoveY(_score, 2.5f).SetEase(Ease.Flash).SetDelay(1f);
            yield return new WaitForSeconds(4.5f);
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        }
        private void OnReset()
        {           
            fakeObject.gameObject.SetActive(false);
            transform.GetChild(0).localPosition = _initializePos;

        }
    }
}