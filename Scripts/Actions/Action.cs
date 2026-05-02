using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft;
using Newtonsoft.Json;
[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public partial class Action : Node, IAction
{
    [JsonProperty("Icon")]
    public string IconString { get; set; }

    [JsonProperty]
    public string ActionNameID { get; set; }

    [JsonProperty]
    public ActionTypeEnum ActionType { get; set; }

    [JsonProperty]
    public string ExpansionID;

    [JsonProperty]
    Vector3? NewVelocity { get; set; }

    [JsonProperty]
    float SpeedMultiplier { get; set; }

    [JsonProperty]
    int TimesUsable { get; set; }

    [JsonProperty]
    List<AvailableHitboxes> Hitbox { get; set; }

    [JsonProperty]
    float TimeActive { get; set; }

    public Image IconImage { get; set; }
    int TimesUsed { get; set; }
    bool IsTimerActive;

    public Action(string pathToDisplayIcon, string actionNameID, string expansionID, ActionTypeEnum type, Vector3? newVelocity = null, float speedMultiplier = 1, int timesUsable=0, List<AvailableHitboxes> hitbox = null, float timeActive = 0)
    {
        IconString = pathToDisplayIcon;
        ExpansionID = expansionID;
        ActionNameID = actionNameID;
        NewVelocity = newVelocity;
        SpeedMultiplier = speedMultiplier;
        TimesUsable = timesUsable;
        Hitbox = hitbox;
        TimeActive = timeActive;
        ActionType = type;
    }

    public void Initialize()
    {
        /*ImageTexture image = new ImageTexture();
        image.Load(IconString);*/

       IconImage = Image.LoadFromFile(IconString);
      //  GD.Print("IconTexture loaded successfully");
    }

    public void Use(PlayerCharacter character)
    {
        character.PlayerOnFloor += () => ResetUses(character);

        if (TimesUsable>0)
        {
            if (TimesUsed>=TimesUsable)
            {
                return;
            }
        }

        TimesUsed++;

        //find out how to make action last a certain amount of time rather than being done on button release

        if (TimeActive > 0)
        {
            if (!IsTimerActive)
            {
                IsTimerActive = true;
                var timer = GetTree().CreateTimer(TimeActive);
                timer.Timeout += () => Timeout(character);
            }
        }

        if (Hitbox != null)
        {
            character.HitboxManager.ActivateHitboxes(Hitbox, true);
        }

        if(NewVelocity != null)
        {
            character.Velocity += (Vector3)NewVelocity;
        }

        

            character.CurrentSpeed *= SpeedMultiplier;
    }

    public void Timeout(PlayerCharacter character)
    {
        IsTimerActive = false;
        if (Hitbox != null)
        {
            character.HitboxManager.ActivateHitboxes(Hitbox, false);
        }

        StopUse(character);
    }

    public void StopUse(PlayerCharacter character)
    {
        if (IsTimerActive)
            return;


            character.CurrentSpeed = character.BaseSpeed;


        
    }

    void ResetUses(PlayerCharacter character)
    {
        TimesUsed = 0;
        character.PlayerOnFloor -= () => ResetUses(character);
    }
    public override string ToString()
    {
        return ActionNameID + "----\n" +
            "   ActionType: " + ActionType + "\n" +
        "   PathToDisplayIcon: " + IconString + "\n" +
        "   ExpansionID: " + ExpansionID + "\n" +
        "   ActionNameID: " + ActionNameID + "\n" +
        "   NewVelocity: " + NewVelocity + "\n" +
        "   SpeedMultiplier: " + SpeedMultiplier + "\n" +
        "   TimesUsable: " + TimesUsable + "\n" +
        "   Hitbox: " + Hitbox + "\n" +
        "   TimeActive: " + TimeActive;


    }
}
