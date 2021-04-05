using Godot;
using System;

namespace KokeBlacksmith.States.Player
{
    public class GroundAttackState : Node, IState<KokeBlacksmith.Player>
    {
        private InputManager inputManager;
        private bool attacking;
        #region IState
        public void OnStateEnter(KokeBlacksmith.Player character)
        {
            attacking = true;
            inputManager = character.InputManager;
            character.AnimationFinished += _OnAttackEnd;
            character.AnimatedSprite.Play("MeleeAttack");
            character.Velocity = Vector2.Zero;

        }

        public void OnStateExit(KokeBlacksmith.Player character)
        {
            character.AnimationFinished -= _OnAttackEnd;
        }

        public IState<KokeBlacksmith.Player> Update(KokeBlacksmith.Player character, float delta)
        {
            if(!attacking)
                return character.States["Idle"];

            return null;
        }
        #endregion

        private void _OnAttackEnd(string animationName)
        {
            if(animationName.Equals("MeleeAttack"))
                attacking = false;
        }
    }

}
