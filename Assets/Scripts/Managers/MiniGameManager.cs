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
        #region Self Variables

        #region Serialized Veriables

        [SerializeField] private GameObject wallStage;
        [SerializeField] private GameObject fakeMoney;
        [SerializeField] private Transform fakeObject;
        [SerializeField] private Material mat;

        #endregion

        #region Private Veriables
        private int _score = 0;
        private float _multipler;
        private Vector3 _initializePos;

        #endregion

        #endregion

        private void Awake()
        {
            NewWallStage();
            InitializeFake();
            _initializePos = transform.GetChild(0).localPosition;
        }

        #region Event Subscribtion

        private void OnEnable()
        {
            Subcribtion();
        }

        private void Subcribtion()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onMiniGameStart += OnMiniGameStart;
            ScoreSignals.Instance.onSendFinalScore += OnSendScore;
            ScoreSignals.Instance.onGetMultiplier += OnGetMultipler;
        }

        private void UnSubcribtion()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGameStart;
            ScoreSignals.Instance.onSendFinalScore -= OnSendScore;
            ScoreSignals.Instance.onGetMultiplier -= OnGetMultipler;

        }

        private void OnDisable()
        {
            UnSubcribtion();
        }

        #endregion

        private void NewWallStage()
        {
            for (int i = 0; i <= 90; i++)
            {
                var ob = Instantiate(wallStage, transform);
                ob.transform.localPosition = new Vector3(0, i * 10, 0);
                ob.transform.GetChild(0).GetComponent<TextMeshPro>().text = "x" + ((i / 10f) + 1f);
            }
        }

        private void InitializeFake()
        {
            for (int i = 0; i < 15; i++)
            {
                var ob = Instantiate(fakeMoney, fakeObject);
                ob.transform.localPosition = new Vector3(0, -i * 1.58f, -7);
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
            if (_score==0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
                
            }
            else
            {
                transform.GetChild(0).DOLocalMoveY(_score, 2.5f).SetEase(Ease.Flash).SetDelay(1f);
                yield return new WaitForSeconds(4.5f);
                LevelSignals.Instance.onLevelSuccessful?.Invoke();
            }
          
        }
        private void ResetWalls()
        {
            for (int i = 1; i <= 90; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material=mat;
                transform.GetChild(i).transform.DOLocalMoveZ(0, 0);
            }
        }

        public void OnSetMultipler(float multipler)
        {
            _multipler = multipler;
        }

        private float OnGetMultipler() => _multipler;

        private void OnReset()
        {
            StopAllCoroutines();
            DOTween.KillAll();
            ResetWalls();
            fakeObject.gameObject.SetActive(false);
            transform.GetChild(0).localPosition = _initializePos;
        }
    }
}