using Godot;
using System;
using System.Collections.Generic;
public enum ActionButtons : int { North, East, South, West }
public enum CharacterAnimation { Idle, Run, Jump, DodgeF, DodgeB, Melee }

public partial class ActionManager : Node
{
	public PlayerCharacter PlayerCharacter { get; private set; }
    Dictionary<ActionButtons, IAction> actions = new Dictionary<ActionButtons, IAction>() { {ActionButtons.North, null},{ActionButtons.East, null},{ActionButtons.South, null},{ActionButtons.West, null}, };
    public float gravity = 15f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        PlayerCharacter = (PlayerCharacter)GetTree().GetFirstNodeInGroup("Player");

        GD.PrintErr("YOU ARE MANUALLY CREATING BUTTON ACTIONS IN THE CHARACTER CONTROLLER");


        actions[ActionButtons.South] = ModReader.actions?[0];
         actions[ActionButtons.East] = ModReader.actions?[1];
        actions[ActionButtons.West] = ModReader.actions?[2];
        
        actions[ActionButtons.North] = ModReader.actions?[3];
        foreach (var action in ModReader.actions)
        {
            AddChild((Action)action);
        }

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = PlayerCharacter.Velocity;

        // Add the gravity.
        if (!PlayerCharacter.IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        HandleRunning(velocity);

        PlayerCharacter.MoveAndSlide();
    }

    void HandleRunning(Vector3 velocity)
    {
        Vector3 direction = Vector3.Zero;
        Vector2 inputDir = Input.GetVector("MoveN", "MoveS", "MoveE", "MoveW");


        float Y = new Vector2(-PlayerCharacter.Facing.X, PlayerCharacter.Facing.Z).Angle() + Mathf.DegToRad(-90);
        PlayerCharacter.Model.Rotation = new Vector3(0, Y, 0);
        PlayerCharacter.HitboxManager.Rotation = PlayerCharacter.Model.Rotation;

        direction = (PlayerCharacter.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y));


        direction = direction.LimitLength();
        if (direction != Vector3.Zero)
        {

            velocity.X = direction.X * PlayerCharacter.CurrentSpeed;
            velocity.Z = direction.Z * PlayerCharacter.CurrentSpeed;
            if (PlayerCharacter.IsOnFloor())
            {
                PlayerCharacter.Facing = direction.Normalized();
            }
            else
            {
                velocity.X *= 0.5f;
                velocity.Z *= 0.5f;
            }
            PlayerCharacter.isWalking = true;
        }
        else if (PlayerCharacter.IsOnFloor())
        {


            velocity.X = Mathf.MoveToward(PlayerCharacter.Velocity.X, 0, PlayerCharacter.CurrentSpeed);
            velocity.Z = Mathf.MoveToward(PlayerCharacter.Velocity.Z, 0, PlayerCharacter.CurrentSpeed);
            PlayerCharacter.isWalking = false;
        }



        PlayerCharacter.Velocity = velocity;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        HandleButtonsPressed( @event );
        HandleButtonsReleased( @event );

       

    }

    void HandleButtonsPressed(InputEvent @event)
    {
        if (@event.IsActionPressed("ActionN"))
        {
            GD.Print("ActionN Pressed");
            actions[ActionButtons.North]?.Use(PlayerCharacter);
        }

        if (@event.IsActionPressed("ActionE"))
        {
            GD.Print("ActionE Pressed");
            actions[ActionButtons.East]?.Use(PlayerCharacter);
        }

        if (@event.IsActionPressed("ActionS"))
        {
            GD.Print("ActionS Pressed");
            actions[ActionButtons.South]?.Use(PlayerCharacter);
        }


        if (@event.IsActionPressed("ActionW"))
        {
            GD.Print("ActionW Pressed");
            actions[ActionButtons.West]?.Use(PlayerCharacter);
        }


    }
    void HandleButtonsReleased(InputEvent @event)
    {

        if (@event.IsActionReleased("ActionN"))
        {
            GD.Print("ActionN Released");
            actions[ActionButtons.North]?.StopUse(PlayerCharacter);
        }

        if (@event.IsActionReleased("ActionE"))
        {
            GD.Print("ActionE Released");
            actions[ActionButtons.East]?.StopUse(PlayerCharacter);
        }

        if (@event.IsActionReleased("ActionS"))
        {
            GD.Print("ActionS Released");
            actions[ActionButtons.South]?.StopUse(PlayerCharacter);
        }


        if (@event.IsActionReleased("ActionW"))
        {
            GD.Print("ActionW Released");
            actions[ActionButtons.West]?.StopUse(PlayerCharacter);
        }
    }

    public void SetActionSlot(ActionButtons button, IAction action)
    {
        actions[button] = action;
    }

}
