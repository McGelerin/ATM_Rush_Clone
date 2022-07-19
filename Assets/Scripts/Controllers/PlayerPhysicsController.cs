using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
         

            // if (other.CompareTag("Obstacle"))
            // {
            //   
            // }
            //
            // if (other.CompareTag("WinZone"))
            // {
            //
            // }
            //
            // if (other.CompareTag("ATM"))
            // {
            //     
            // }  
            // if (other.CompareTag("Collectable"))
            // {
            //     
            // }
        }
        
    }
}