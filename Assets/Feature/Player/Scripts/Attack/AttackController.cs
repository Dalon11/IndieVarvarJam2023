using Jam.Animation.Abstraction;
using Jam.Fabric.Initable.Abstraction;
using Jam.GameInput.Abstraction;
using Jam.Model.Abstraction;
using Jam.Player.Abstraction;
using System;
using UniRx;

namespace Jam.Player.Controllers
{
    public class AttackController : IDisposable, IInitializable<IMakeDamage>, IInitializable<AbstractInput>, IInitializable<IShowAttack>, IInitializable<IStateModel>
    {
        private readonly Weapon _weapon;
        private AbstractInput _input;

        private IMakeDamage _damageController;
        private IShowAttack _view;
        private IStateModel stateModel;

        private IDisposable _disposable;

        public AttackController(Weapon weapon) => _weapon = weapon;

        public void Init(IMakeDamage model)
        {
            _damageController = model;
            _weapon.onTriggerEnter += _damageController.MakeDamage;
        }
        public void Init(IShowAttack model) => _view = model;

        public void Init(AbstractInput model)
        {
            _input = model;
            _disposable = _input.AttackButton.Subscribe(Attack);
        }
        public void Init(IStateModel model) => stateModel = model;

        public void Attack(bool value)
        {
            if (stateModel == null || stateModel.IsDeath.Value)
                return;

            _view?.ShowAttack(value);
        }

        public void Dispose()
        {
            if (_damageController != null)
                _weapon.onTriggerEnter -= _damageController.MakeDamage;

            _disposable.Dispose();
        }

    }
}
