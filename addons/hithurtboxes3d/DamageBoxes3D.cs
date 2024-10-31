# if TOOLS
using Godot;
using System;

[Tool]
public partial class DamageBoxes3D : EditorPlugin
{
    public override void _EnterTree()
    {
        // Initialization of the plugin goes here.
        // Add the new type with a name, a parent type, a script and an icon.
        var script = GD.Load<Script>("res://addons/hithurtboxes3d/HitBox3D.cs");
        var script2 = GD.Load<Script>("res://addons/hithurtboxes3d/HurtBox3D.cs");
        var texture = GD.Load<Texture2D>("res://addons/hithurtboxes3d/HitHurtBox2D.png");

        AddCustomType("HitBox3D", "Area3D", script, texture);
        AddCustomType("HurtBox3D", "Area3D", script2, texture);
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        // Always remember to remove it from the engine when deactivated.
        RemoveCustomType("HitBox3D");
        RemoveCustomType("HurtBox3D");
    }
}
#endif