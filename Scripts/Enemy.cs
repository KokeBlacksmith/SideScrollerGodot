using Godot;
using System;
using System.Collections.Generic;
using KokeBlacksmith.States.Enemy;
using KokeBlacksmith.States;

namespace KokeBlacksmith
{
    public class Enemy : Character<Enemy>
    {
        #region Properties
        public RayCast2D LeftRaycast { get; private set; }
        public RayCast2D RightRaycast { get; private set; }
        #endregion

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            LeftRaycast = (RayCast2D)GetNode("RayCast2DLeft");
            RightRaycast = (RayCast2D)GetNode("RayCast2DRight");
        }

        public override void FetchStates()
        {
            States = new Dictionary<string, IState<Enemy>>() 
            {
                {"Idle", (IdleState)GetNode("States/Idle")},
                {"Movement", (MovementState)GetNode("States/Movement")}
                // {"Jump", (JumpState)GetNode("States/Jump")},
                // {"Fall", (FallState)GetNode("States/Fall")},
                // {"GroundAttack", (GroundAttackState)GetNode("States/GroundAttack")}
            };
        }
    }

}
