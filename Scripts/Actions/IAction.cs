using Godot;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;

[JsonConverter(typeof(StringEnumConverter))]
public enum ActionTypeEnum { Attack, Movement, Passive}
public partial interface IAction
{
    public string IconString { get; set; }
    public Image IconImage { get; set; }
    public ActionTypeEnum ActionType { get; set; }
    public string ActionNameID { get; set; }
    public void Use(PlayerCharacter character) { }
    public void StopUse(PlayerCharacter character) { }

    public void Initialize();
    public string ToString();
}
