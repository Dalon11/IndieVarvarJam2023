using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Animation
{

    public class AnimatorProvider : MonoBehaviour
    {

        [SerializeField] private Animator animator;

        private void Update()
        {
            animator.SetFloat("SpeedX", Input.GetAxis("Horizontal"));
            animator.SetFloat("SpeedZ", Input.GetAxis("Vertical"));

            animator.SetBool("IsAttack", Input.GetKey(KeyCode.Space)); 
        }
    }
}