using System;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class WallCheckController : MonoBehaviour
    {
        private float _changesColor;
        private float _multiplier;

        private void OnDisable()
        {
            _changesColor = 0;
        }

        private void ChangeColor(Collider other)
        {
            _changesColor = (0.036f+_changesColor)%1;
            other.gameObject.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor,1,1),0.1f);
            other.gameObject.transform.DOLocalMoveZ(-3, 0.1f);

        }
        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag("Wall"))
            {
             ChangeColor(other);
            }
        }
    }
}