using Godot;
using System;

public partial class boot_screen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var timer = GetTree().CreateTimer(1f);
		timer.Timeout += () => GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
