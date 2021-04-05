using System;
using Godot;
using KokeBlacksmith.States;

namespace KokeBlacksmith.States.Player
{
    public class MovementState : Node, IState<KokeBlacksmith.Player>
    {
        private InputManager inputManager = null;

        #region IState
        public void OnStateEnter(KokeBlacksmith.Player character)
        {
            inputManager = character.InputManager;
            character.AnimatedSprite.Play("Run");
        }

        public void OnStateExit(KokeBlacksmith.Player character)
        {
        }

        public IState<KokeBlacksmith.Player> Update(KokeBlacksmith.Player character, float delta)
        {
            character.CalculateSpriteFlip();
            Vector2 tmpVelocity = character.Velocity;
            tmpVelocity.x = inputManager.Horizontal * character.Speed;
            tmpVelocity.y += character.Global.Gravity * delta;
            character.Velocity = tmpVelocity;

            if(inputManager.Jumping)
                return character.States["Jump"];
                
            if(!character.IsOnFloor())
            {
                return character.States["Fall"];
            }
            else if(Mathf.IsZeroApprox(inputManager.Horizontal))
                return character.States["Idle"];

            if(inputManager.Action)
                return character.States["GroundAttack"];

            return null;
        }
        #endregion
    }

}