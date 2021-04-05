using Godot;
using KokeBlacksmith.States;
using KokeBlacksmith;

namespace KokeBlacksmith.States.Player
{
    public class IdleState : Node, IState<KokeBlacksmith.Player>
    {
        private InputManager inputManager = null;
        private bool isJumpPressed = false;

        #region IState
        public void OnStateEnter(KokeBlacksmith.Player character)
        {
            inputManager = character.InputManager;
            character.AnimatedSprite.Play("Idle");
            character.Velocity = Vector2.Zero;
        }

        public void OnStateExit(KokeBlacksmith.Player character)
        {
            //inputManager.JumpPressed -= onJumpPressed;
        }

        public IState<KokeBlacksmith.Player> Update(KokeBlacksmith.Player character, float delta)
        {
            if(Mathf.Abs(inputManager.Horizontal) > 0)
                return character.States["Movement"];

            if(inputManager.Jumping)
                return character.States["Jump"];

            if(inputManager.Action)
                return character.States["GroundAttack"];

            return null;
        }
        #endregion
    }

}