
namespace PixelRPG.Enemy.StateMachine
{
    public class BossStateMachine : EnemyStateMachine
    {
        protected override BaseState[] RegisterStates()
        {
            return new BaseState[]
            {
                new ChasingState(this, true),
                new ChasingState(this, true),
                new AttackingState(this),
                new DeadState(this),
            };
        }
    }
}
