using Godot;
using System;

public partial class BootScreen : Node2D
{
	[Signal]
	public delegate void DoneBootingEventHandler();
	public void DoBoot()
	{
		GetNode<ModReader>("ModReader").InitializeMods();
		EmitSignal(SignalName.DoneBooting);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
