using Godot;
using System;
using KokeBlacksmith;

namespace KokeBlacksmith.States.Enemy
{
    public class MovementState : Node, IState<KokeBlacksmith.Enemy>
    {
        #region IState
        public void OnStateEnter(KokeBlacksmith.Enemy character)
        {
            //Calculate forward
            bool facingRight = character.Forward.x > 0;

            //Check if the raycast based on forward has floor
            if(!this._IsFacingFloor(character))
                character.Flip();

            GD.Print("Movement enter");
        }

        public void OnStateExit(KokeBlacksmith.Enemy character)
        {
            //throw new NotImplementedException();
        }

        public IState<KokeBlacksmith.Enemy> Update(KokeBlacksmith.Enemy character, float delta)
        {
            //Check for player to attack

            //Check for floor
            if(_IsFacingFloor(character as KokeBlacksmith.Enemy))
                character.Velocity = character.Forward * character.Speed;
            else
                return character.States["Movement"];
                //If not, idle

            //Move

            return null;
        }
        #endregion

        private bool _IsFacingFloor(KokeBlacksmith.Enemy enemy)
        {
            if(enemy.Forward.x > 0)
                return !(!enemy.RightRaycast.IsColliding() && enemy.LeftRaycast.IsColliding());
            else
                return !(!enemy.LeftRaycast.IsColliding() && enemy.RightRaycast.IsColliding());
        }
    }
}

