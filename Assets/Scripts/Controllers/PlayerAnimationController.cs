using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Veriables

        #region Public Veriables
        public PlayerAnimationStates State=PlayerAnimationStates.Idle;
        
        

        #endregion

        #region Private Veriables

        

        #endregion

        #region Serial Veriables

        [SerializeField] private Animator animatorController;

        #endregion

        #endregion

        public void Playanim(PlayerAnimationStates animationStates)
        {
            animatorController.SetTrigger(animationStates.ToString());
        }

        public void OnReset()
        {
            animatorController.SetTrigger(PlayerAnimationStates.Idle.ToString());
        }
       
    }           
}