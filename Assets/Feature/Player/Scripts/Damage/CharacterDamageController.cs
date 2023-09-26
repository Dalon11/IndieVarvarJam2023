using Jam.Fabric.Initable.Abstraction;
using Jam.Model;
using Jam.Model.Abstraction;
using Jam.Player.Abstraction;
using System; 

namespace Jam.Player
{

    public class CharacterDamageController : ITakeDamage, IMakeDamage, IInitializable<IAttackModel>, IInitializable<IStateModel>, IInitializable<IDecreaseHealth>
    {
        private IAttackModel _attackModel;
        private IStateModel _stateModel;
        private IDecreaseHealth _healModel;

        public event Action onTakeDamage = () => { };
        public event Action onMakeDamage = () => { };


        public void Init(IAttackModel model) => _attackModel = model;
        public void Init(IStateModel model) => _stateModel = model;

        public void Init(IDecreaseHealth model) => _healModel = model;

        public void TakeDamage(float damage)
        {
            _healModel.DecreaseHealth(damage);
            onTakeDamage.Invoke();
            Death();
        }

        public void MakeDamage(ITakeDamage enemy)
        {
            enemy.TakeDamage(_attackModel.Damage);
            onMakeDamage.Invoke();
        }

        private void Death()
        {
            if (_healModel.Health.Value <= 0)
            {
                _stateModel.IsDeath.Value = true;
            }
            
        }

    }
}
