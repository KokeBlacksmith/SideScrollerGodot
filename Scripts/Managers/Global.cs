using Godot;
using System;

public class Global : Godot.Node
{
    public Node CurrentScene { get; set; }
    public int Gravity { get; } = 200;

    public override void _Ready()
    {
        Viewport root = GetTree().GetRoot();
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
    }
}