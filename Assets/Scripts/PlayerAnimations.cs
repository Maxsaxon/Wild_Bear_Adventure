using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
       animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    public void WalkForward(bool WalkForward)
    {
        animator.SetBool(AnimationTags.WALK_PARAMETER, WalkForward);
    }

    public void Attack_1()
    {
        animator.SetTrigger(AnimationTags.ATTACK_TRIGGER_1);
    }

    public void Attack_2()
    {
        animator.SetTrigger(AnimationTags.ATTACK_TRIGGER_2);
    }

    public void Death_1()
    {
        animator.SetTrigger(AnimationTags.DEATH_TRIGGER);
    }
}
