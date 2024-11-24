using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Security.AccessControl;
public partial class ModReader : Node2D
{
    public static List<IAction> actions = new();
	string modsDir = ProjectSettings.GlobalizePath("user://mods");
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		 //modsDir = ProjectSettings.GlobalizePath("user://mods");

		foreach (var mod in Directory.GetDirectories(modsDir))
		{
            PrintToLog(mod);
			PrintFilesInDir(mod);
		}

		PrintToLog("-----");
		
		PrintFilesInDir(modsDir);

		AssembleActionsFromJson();
	}

	void PrintFilesInDir(string dir)
	{
        foreach (var textName in Directory.GetFiles(dir))
        {
            PrintToLog(textName);
			ReadFile(textName);
        }
    }

	void ReadFile(string path)
	{
		try
		{
			if (File.Exists(path))
			{
				using (StreamReader sr = new StreamReader(path))
				{
					PrintToLog(sr.ReadToEnd());
					sr.Close();
				}
			}
		}
		catch { }
	}

	void PrintToLog(string what)
	{
		GD.Print(what);
		GetParent().GetNode<RichTextLabel>("RichTextLabel").Text += what + "\n";
	}

	public void AssembleActionsFromJson()
	{
		
	/*	try
		{
		if(File.Exists(Path.Combine(modsDir, "JsonActionA.json")))
		{
			File.Delete(Path.Combine(modsDir, "JsonActionA.json"));
		}
			//var file = File.Create(Path.Combine(modsDir, "JsonAction.json"));
			Action action = new 
             Action("", "AttackAction", "Core", null, 0.25f, 0, new List<AvailableHitboxes> { AvailableHitboxes.FrontNear, AvailableHitboxes.Left }, 0.5f);
				Action action2 = new Action("", "JumpAction", "Core", new Vector3(0, Mathf.Sqrt(2 * 15 * 64), 0), 1, 1);

			Action[] actions = { action, action2};

            var jsonString = JsonConvert.SerializeObject(actions,Formatting.Indented);
			File.WriteAllText(Path.Combine(modsDir, "JsonActionA.json"), jsonString);
			
		}
		catch (Exception e)
		{
			GD.Print(e.Message);
		}*/
		GD.Print("-------Reading-------");
		try
		{
			string jsonFile = File.ReadAllText(Path.Combine(modsDir, "JsonAction.json"));
			var action = JsonConvert.DeserializeObject<Action[]>(jsonFile);

            actions = action.ToList<IAction>();
		}
		catch(Exception e)
		{
			GD.Print(e.Message);
		}

		/*
		actions.Add(new Action("", "JumpAction", "Core", new Vector3(0, Mathf.Sqrt(2 * 15 * 64), 0), 1, 1));
		actions.Add(new Action("", "SprintAction", "Core", null, 1.5f));
		actions.Add(new Action("",  "AttackAction", "Core", null, 0.25f, 0, new List<AvailableHitboxes> { AvailableHitboxes.FrontNear, AvailableHitboxes.Left }, 0.5f)) ;
		actions.Add(new Action("",  "JumpAttackAction", "Core", new Vector3(0, Mathf.Sqrt(2 * 15 * 64), 0), 0.05f, 1, new List<AvailableHitboxes> { AvailableHitboxes.FrontNear }, 0.5f)) ;
		*/
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
