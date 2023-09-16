using Jam.Fabric.Initable.Abstraction;
using Jam.Model;
using Jam.Model.Abstraction;
using Jam.Player.Abstraction;
using System;
using UnityEngine;

namespace Jam.Player
{

    public class CharacterDamageController : ITakeDamage, IMakeDamage, IInitializable<IAttackModel>, IInitializable<IDecreaseHealth>
    {
        private IAttackModel _attackModel;
        private IDecreaseHealth _healModel;

        public event Action onTakeDamage = () => { };
        public event Action onMakeDamage = () => { };


        public void Init(IAttackModel model) => _attackModel = model;

        public void Init(IDecreaseHealth model) => _healModel = model;

        public void TakeDamage(float damage)
        {
            _healModel.DecreaseHealth(damage);
            onTakeDamage.Invoke();
        }

        public void MakeDamage(ITakeDamage enemy)
        {
            enemy.TakeDamage(_attackModel.Damage);
            onMakeDamage.Invoke();
        }

    }
}
