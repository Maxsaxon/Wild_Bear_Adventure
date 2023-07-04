using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    private PlayerAnimations playerAnim;

    public GameObject attackPoint;

    
    void Awake()
    {
        playerAnim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) // get key down means when pressing key, animation works, when unpress it stops
        {
            if(Random.Range(0, 2) > 0)
            {
                playerAnim.Attack_1();
            }
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            if(Random.Range(0, 2) > 0)
            {
                playerAnim.Attack_2();
            }
        }
    }

    void ActivateAttackPoint()
    {
            attackPoint.SetActive(true);
    }

    void DeactivateAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
