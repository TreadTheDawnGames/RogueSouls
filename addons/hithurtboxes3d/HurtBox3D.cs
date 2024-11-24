using Godot;
using System;

public partial class HurtBox3D : Area3D
{

    [Export] public uint layers;
    CollisionShape3D collisionShape;

    [Signal]
    public delegate void HurtBoxTakeDamageEventHandler(int damage, HitBox3D attackingBox);

    public override void _Ready()
    {
/*        CollisionLayer = 0;
        CollisionMask = layers;
*/        AreaEntered += (AreaEntered) => OnAreaEntered(AreaEntered);
        collisionShape = GetNode<CollisionShape3D>("CollisionShape3D");
    }

    
   public void SetEnabled(bool enabled)
    {
        collisionShape.SetDeferred("disabled", !enabled);
    }



    public void OnAreaEntered(Area3D hitBox)
    {
        if (hitBox.Owner.Name != Owner.Name)
        {
            HitBox3D box = (HitBox3D)hitBox;
            if (box != null)
            {
                EmitSignal(SignalName.HurtBoxTakeDamage, box.damage, box);
            }
                //Owner.Call("TakeDamage", box.damage, box);


        }

        
    }
}
