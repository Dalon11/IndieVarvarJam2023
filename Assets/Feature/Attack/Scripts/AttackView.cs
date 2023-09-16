using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Attack
{
    public class AttackView : MonoBehaviour
    {
        [SerializeField] Animator animator;
       
        public void ShowTakeDamage()
        {
            animator.SetTrigger("TakeDamage");
        }
        public void ShowMakeDamage()
        {
            animator.SetTrigger("MakeDamage");
        }
        public void ShowAttack()
        {
            animator.SetTrigger("Attack");

        }
    }
}
