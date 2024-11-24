using Godot;
using System;

public partial interface IAction
{

    public string PathToDisplayIcon { get; set; }
    public void Use(PlayerCharacter character) { }
    public void StopUse(PlayerCharacter character) { }

}
