using Jam.Animation.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Animation
{

    public class AnimatorProvider : MonoBehaviour, IShowAttack, IShowMovement
    {
        private readonly string ParameterAttack = "IsAttack";
        private readonly string ParameterSpeedX = "SpeedX";
        private readonly string ParameterSpeedZ = "SpeedZ";
        private readonly string ParameterDeath = "Death";

        [SerializeField] private Animator animator;

        public void ShowAttack(bool value) => animator.SetBool(ParameterAttack, value);

        public void ShowMovement(float speedX, float speedZ)
        {
            animator.SetFloat(ParameterSpeedX, speedX);
            animator.SetFloat(ParameterSpeedZ, speedZ);
        }

        public void ShowDeath(bool value)
        {
            animator.SetBool(ParameterDeath, value);
        }
    }
}