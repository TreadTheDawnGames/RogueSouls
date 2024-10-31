using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class HitBox3D : Area3D
{
    [Export] public int damage = 1;
    [Export] public uint layers;

    CollisionShape3D[] myShape;

    public override void _Ready()
    {
        CollisionLayer = layers;
        CollisionMask = 0;

        List<CollisionShape3D> nodes = new List<CollisionShape3D>();

        foreach (CollisionShape3D child in GetChildren().OfType<CollisionShape3D>())
        {
                nodes.Add(child);
        }
        myShape = nodes.ToArray() ;
    }


    public void SetEnabled(bool enabled)
    {
        foreach (CollisionShape3D shape in myShape)
        {
            shape.SetDeferred("disabled", !enabled);

        }
    }

    public bool Identify()
    {
        return true;
    }


}
