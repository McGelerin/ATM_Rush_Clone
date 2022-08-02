using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Veriables

        [SerializeField] private TextMeshPro scoreText;

        #endregion

        #region Private Veriables

        private DOTweenAnimation _atmAnim;

        #endregion

        #endregion

        #region EventSubcribtions

        private void OnEnable()
        {
            Subscripe();
        }

        private void Subscripe()
        {
            CoreGameSignals.Instance.onAtmTouched += OnTouched;
            ScoreSignals.Instance.onSetAtmScoreText +=OnChangeAtmScore;
        }

        private void UnSubscripe()
        {
            CoreGameSignals.Instance.onAtmTouched -= OnTouched;
            ScoreSignals.Instance.onSetAtmScoreText +=OnChangeAtmScore ;


        }

        private void OnDisable()
        {
            UnSubscripe();
        }

        #endregion

        private void Awake()
        {
            _atmAnim = GetComponentInChildren<DOTweenAnimation>();
        }

        private void OnTouched(GameObject _touchedObject)
        {
            if (_touchedObject == gameObject)
            {
                _atmAnim.DOPlay();
            }
        }

        private void OnChangeAtmScore(int _atmValues)
        {
            scoreText.text = _atmValues.ToString();
        }
    }
}