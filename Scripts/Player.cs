using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using KokeBlacksmith.States.Player;
using KokeBlacksmith.States;

namespace KokeBlacksmith
{
    public class Player : Character<Player>
    {
        #region Properties
        public InputManager InputManager { get; private set; }
        #endregion

        public override void _Ready()
        {
            InputManager = (InputManager)GetNode("/root/InputManager");
            base._Ready();
        }
        
        public override void FetchStates()
        {
            States = new Dictionary<string, IState<Player>>() 
            {
                {"Idle", (IdleState)GetNode("States/Idle")},
                {"Movement", (MovementState)GetNode("States/Movement")},
                {"Jump", (JumpState)GetNode("States/Jump")},
                {"Fall", (FallState)GetNode("States/Fall")},
                {"GroundAttack", (GroundAttackState)GetNode("States/GroundAttack")}
            };
        }
    }

}
