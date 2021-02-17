using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : StateMachineBehaviour
{
    private ArcherController archer;
  
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        archer = GameObject.FindObjectOfType<ArcherController>();
        archer.isShooting = false;
    }


}
