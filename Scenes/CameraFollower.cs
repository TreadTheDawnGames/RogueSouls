using Godot;
using System;

public partial class CameraFollower : Camera3D
{
    Node3D target;
    float startHeight;
    [Export]
    Vector3 offset = new Vector3(10,13,0);
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        target = (Node3D)GetTree().GetFirstNodeInGroup("CameraTarget");
        startHeight = GlobalPosition.Y;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector3 targetPosition = target.GlobalPosition + offset;
        
        targetPosition.Y = startHeight;

        GlobalPosition = GlobalPosition.Lerp(targetPosition, 0.07f);
    }
}
