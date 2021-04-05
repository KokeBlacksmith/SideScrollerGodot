using System;
using Godot;
using KokeBlacksmith.States;

namespace KokeBlacksmith.States.Player
{
    public class JumpState : Node, IState<KokeBlacksmith.Player>
    {
        private InputManager inputManager = null;
        private bool jumping;
        #region IState
        public void OnStateEnter(KokeBlacksmith.Player character)
        {
            character.Velocity = new Vector2(character.Velocity.x, -character.JumpForce);
            inputManager = character.InputManager;
            character.AnimatedSprite.Play("Jump");
            jumping = true;
        }

        public void OnStateExit(KokeBlacksmith.Player character)
        {
            jumping = false;
        }

        public IState<KokeBlacksmith.Player> Update(KokeBlacksmith.Player character, float delta)
        {
            character.CalculateSpriteFlip();
            Vector2 tmpVelocity = character.Velocity;
            tmpVelocity.x = inputManager.Horizontal * character.Speed;
            tmpVelocity.y += character.Global.Gravity * delta;
            character.Velocity = tmpVelocity;

            if(character.IsOnFloor())
            {
                if(Mathf.IsZeroApprox(character.Velocity.x))
                    return character.States["Idle"];
                else
                    return character.States["Movement"];
            }

            if((!inputManager.Jumping && character.Velocity.y < 0) || character.Velocity.y > 0)
            {
                return character.States["Fall"];
            }

            return null;
        }
        #endregion
    }

}
