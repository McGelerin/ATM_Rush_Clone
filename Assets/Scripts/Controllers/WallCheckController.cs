using UnityEngine;
using DG.Tweening;
using Managers;

namespace Controllers
{
    public class WallCheckController : MonoBehaviour
    {
        #region Self Variables
        #region SerializeField Variables

        [SerializeField] private MiniGameManager manager;
        #endregion
        #region Private Variables

        private float _changesColor;
        private float _multiplier = 0.90f;
        #endregion
        #endregion

        private void OnDisable()
        {
            _changesColor = 0;
            _multiplier = 0.90f;
        }

        private void ChangeColor(Collider other)
        {
            _changesColor = (0.036f + _changesColor) % 1;
            other.gameObject.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor, 1, 1), 0.1f);
            other.gameObject.transform.DOLocalMoveZ(-3, 0.1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                _multiplier += 0.1f;
                manager.OnSetMultipler(_multiplier);
                ChangeColor(other);
            }
        }
    }
}