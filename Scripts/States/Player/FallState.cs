using Godot;
using System;
using KokeBlacksmith;

namespace KokeBlacksmith.States.Player
{
    public class FallState : Node, IState<KokeBlacksmith.Player>
    {
        private InputManager inputManager;
        #region IState
        public void OnStateEnter(KokeBlacksmith.Player character)
        {
            inputManager = character.InputManager;
            character.AnimatedSprite.Play("Falling");
        }

        public void OnStateExit(KokeBlacksmith.Player character)
        {
            
        }

        public IState<KokeBlacksmith.Player> Update(KokeBlacksmith.Player character, float delta)
        {
            character.CalculateSpriteFlip();
            Vector2 tmpVelocity = character.Velocity;
            tmpVelocity.x = inputManager.Horizontal * character.Speed;
            tmpVelocity.y += character.Global.Gravity * 2.5f * delta;
            character.Velocity = tmpVelocity;
            
            if(character.IsOnFloor())
            {
                if(Mathf.IsZeroApprox(character.Velocity.x))
                    return character.States["Idle"];
                else
                    return character.States["Movement"];
            }

            return null;
        }
        #endregion
    }

}
