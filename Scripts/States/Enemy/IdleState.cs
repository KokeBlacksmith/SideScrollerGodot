using Godot;
using System;

namespace KokeBlacksmith.States.Enemy
{
    public class IdleState : Node, IState<KokeBlacksmith.Enemy>
    {
        private Timer _timer = null;
        private bool timeout = false;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            if(_timer == null)
                _timer = (Timer)GetNode("Timer");
        }
        
        #region IState
        public void OnStateEnter(KokeBlacksmith.Enemy character)
        {
            character.Velocity = Vector2.Zero;
            timeout = false;
            _timer.Start();
        }

        public void OnStateExit(KokeBlacksmith.Enemy character)
        {
            _timer.Stop();
        }

        public IState<KokeBlacksmith.Enemy> Update(KokeBlacksmith.Enemy character, float delta)
        {
            if(timeout)
                return character.States["Movement"];

            return null;
        }
        #endregion

        private void _OnTimerTimeout()
        {
            timeout = true;
        }
    }
}
    
