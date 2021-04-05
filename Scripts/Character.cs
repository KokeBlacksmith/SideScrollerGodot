using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using KokeBlacksmith.States;

public abstract class Character<T> : KinematicBody2D, IStatePlayer<T>
    where T : Character<T>
{
    //https://gameprogrammingpatterns.com/state.html
    [Export]
    protected float m_Speed = 150;

    [Export]
    protected int m_JumpForce = 350;

    protected IState<T> _currentState = null;

    #region IStatePlayer
    public Dictionary<string, IState<T>> States { get; protected set; }

    public virtual void FetchStates()
    {
        throw new NotImplementedException();
    }

    public IState<T> GetCurrentState() => _currentState;
    public IState<T> ChangeCurrentState(IState<T> newState)
    {
        _currentState?.OnStateExit((T)this);
        _currentState = newState;
        _currentState?.OnStateEnter((T)this);
        return _currentState;
    }

    #endregion

    #region Properties

    public float Speed { get => m_Speed; }
    public float JumpForce { get => m_JumpForce; }

    public Vector2 Forward { get => AnimatedSprite.FlipH ? Vector2.Left : Vector2.Right; }

    public AnimatedSprite AnimatedSprite { get; private set; }
    
    public Global Global { get; private set; }

    public Vector2 Velocity { get; set; } = Vector2.Zero;
    #endregion

    #region Events
    public event Action<string> AnimationFinished;
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FetchStates();

        AnimatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");
        Global = (Global)GetNode("/root/Global");
        this.ChangeCurrentState(States["Idle"]);
    }

    public bool CalculateSpriteFlip()
    {
        if((Velocity.x > 0 && AnimatedSprite.FlipH) || (Velocity.x < 0 && !AnimatedSprite.FlipH))
        {
            Flip();
            return true;
        }

        return false;
    }

    public void Flip()
    {
        AnimatedSprite.FlipH = !AnimatedSprite.FlipH;
    }

    public override void _Process(float delta)
    {
        this.GetCurrentState().Update((T)this, delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        IState<T> newState = this.GetCurrentState().Update((T)this, delta);

        if(newState != null)
            this.ChangeCurrentState(newState);

        Velocity = this.MoveAndSlide(Velocity, Vector2.Up);
    }

    public void OnAnimatedSpriteAnimationFinished()
    {
        AnimationFinished?.Invoke(this.AnimatedSprite.Animation);
    }
}
