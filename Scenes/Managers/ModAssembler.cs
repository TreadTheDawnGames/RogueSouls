using Godot;
using System;
using System.Text.Json.Serialization;

public partial class ModAssembler : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void LoadCurrentSessionData(SessionData data)
	{
		//loads mod data into game manager node for use elsewhere. This class's only job is to load data into the GameManager.
		ActionLibrary actionsLibrary = new(data.Actions);
		GameManager.globalActionLibrary = actionsLibrary;
		//EntityLibrary = new
		//GameManager.globalEntityLibrary
		//WorldGen
		//GlobalWorldGen
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
