using Godot;
using System;

public partial class DummyTarget : Node3D
{

    MeshInstance3D mesh;

    Material material;

    bool isON = false;

    HurtBox3D hurtBox;

    void TakeDamage(int damage, HitBox3D box)
    {
        if (isON)
        {
            SetColor(new Color(1, 1, 1, 1));

        }
        else
        {
            SetColor(new Color(0, 0, 0, 0));

        }
        isON = !isON;
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mesh = GetNode<MeshInstance3D>("MeshInstance3D");

        hurtBox = GetNode<HurtBox3D>("HurtBox3D");

        hurtBox.HurtBoxTakeDamage += TakeDamage;
    }

    void SetColor(Color color)
    {
        StandardMaterial3D material = (StandardMaterial3D)mesh.GetActiveMaterial(0);

        material.AlbedoColor = color;

        mesh.MaterialOverride = material;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
