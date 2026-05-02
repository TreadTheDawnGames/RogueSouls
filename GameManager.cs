using Godot;
using System;

public partial class GameManager : Node3D
{
	PackedScene worldScene = GD.Load<PackedScene>("res://Scenes/world.tscn");
	public static ActionLibrary globalActionLibrary;
	public void ChangeScene(PackedScene scene, Node caller)
	{
		var newScene = scene.Instantiate();
		AddChild(newScene);
		caller.QueueFree();

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var bootScrn = GetNode<BootScreen>("BootScreen");
		bootScrn.DoneBooting += () => ChangeScene(worldScene, bootScrn);
		bootScrn.DoBoot();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
