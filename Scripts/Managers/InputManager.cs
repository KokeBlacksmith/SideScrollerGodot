using Godot;
using System;

public class InputManager : Godot.Node
{
    //https://docs.godotengine.org/es/stable/tutorials/inputs/input_examples.html

    #region Properties

    public float Horizontal { get; private set; }
    public bool Jumping { get; private set; }

    public bool Action {get; private set;}
    #endregion

    public override void _PhysicsProcess(float delta)
    {
        Jumping = Input.IsActionPressed("ui_up");

        if(Input.IsActionPressed("ui_right"))
            Horizontal = 1;
        else if(Input.IsActionPressed("ui_left"))
            Horizontal = -1;
        else
            Horizontal = 0;

        Action = Input.IsActionPressed("ui_select");
    }

}

