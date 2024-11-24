using Godot;
using System;
using static System.Collections.Specialized.BitVector32;
using Godot.Collections;

public partial class PlayerCharacter : CharacterBody3D
{
    [Export]public float BaseSpeed { get; private set; } = 10.0f;
    public float CurrentSpeed = 10.0f;

    public Vector3 Facing = Vector3.Forward;
    public bool isWalking = false;


    public Node3D Model;


    public HitboxManager HitboxManager;
    HurtBox3D HurtBox;

    [Signal]
    public delegate void PlayerOnFloorEventHandler();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Model = GetNode<Node3D>("ModelManager");
        HurtBox = GetNode<HurtBox3D>("HurtBox3D");
        HitboxManager = GetNode<HitboxManager>("HitboxManager");

        HurtBox.HurtBoxTakeDamage += TakeDamage;
    }

    void TakeDamage(int damage, HitBox3D box)
    {
        Velocity = new Vector3(0, 10, 0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        if (IsOnFloor())
        {
            EmitSignal(SignalName.PlayerOnFloor);
        }
	}
}
