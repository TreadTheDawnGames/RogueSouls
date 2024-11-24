using Godot;
using System;
using System.Collections.Generic;


public enum AvailableHitboxes { FrontNear, FrontFar, Left, Right, Back };
public partial class HitboxManager : Node3D
{
	Dictionary<AvailableHitboxes, HitBox3D> Hitboxes = new Dictionary<AvailableHitboxes, HitBox3D>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hitboxes.Add(AvailableHitboxes.FrontNear, GetNode<HitBox3D>("NearSmall"));
		Hitboxes.Add(AvailableHitboxes.FrontFar, GetNode<HitBox3D>("FarSmall"));
		Hitboxes.Add(AvailableHitboxes.Left, GetNode<HitBox3D>("LeftSmall"));
		Hitboxes.Add(AvailableHitboxes.Right, GetNode<HitBox3D>("RightSmall"));
		Hitboxes.Add(AvailableHitboxes.Back, GetNode<HitBox3D>("BehindSmall"));

		foreach(var hitbox in Hitboxes)
		{
			hitbox.Value.SetEnabled(false);
		}
	}

	public void ActivateHitboxes(List<AvailableHitboxes> activeBoxes, bool setTo)
	{
		foreach(AvailableHitboxes activatingBox in activeBoxes)
		{
			if(Hitboxes.ContainsKey(activatingBox))
			{
				Hitboxes[activatingBox].SetEnabled(setTo);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
