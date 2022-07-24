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
         

            if (other.CompareTag("Obstacle"))
            {
                
                rigidbody.transform.DOMoveZ(rigidbody.transform.position.z-10f,1f).SetEase(Ease.OutBack);
                

            }
            //
            // if (other.CompareTag("WinZone"))
            // {
            //
            // }
            //
            if (other.CompareTag("ATM"))
            {
                CoreGameSignals.Instance.onAtmTouched?.Invoke(other.gameObject);
            }  
            if (other.CompareTag("Collectable"))
            {
                other.tag = "Collected";
                StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
            }
        }
        
    }
}