using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Security.AccessControl;

public struct SessionData
{
	public List<IAction> Actions;
	public SessionData(List<IAction> actions)
	{
		Actions = actions;
	}
	//WorldGenData world;
	//EntityData entities
}
public partial class ModReader : Node2D
{
    public static List<IAction> actions = new();
	string modsDir = ProjectSettings.GlobalizePath("user://mods");
    // Called when the node enters the scene tree for the first time.
    public void InitializeMods()
	{
		ActionToJson(new Action("Icon", "Name", "NoExpansion", ActionTypeEnum.Attack),Path.Combine(modsDir, "testAction.json"));
            //foreach mod
		foreach (var modDirectory in Directory.GetDirectories(modsDir))
		{
			LoadActions(modDirectory);
			//LoadEntities(modDirectory;
			//LoadWorldGen(modDirectory);
		}
		
		foreach (var action in actions)
		{
			PrintToLog(action.ToString());
		}

		GetNode<ModAssembler>("ModAssembler").LoadCurrentSessionData(new SessionData(actions));

		PrintToLog("-----");
	

	}

	void LoadActions(string modDirectory)
	{
        try
        {
            if (Directory.Exists(Path.Combine(modDirectory, "Actions")))
            {
                PrintToLog(modDirectory.Split('\\').Last() + " has actions directory.");
                string actionsDirectoryPath = Path.Combine(modDirectory, "Actions");


                if (File.Exists(Path.Combine(actionsDirectoryPath, "Actions.json")))
                {
                    if (!Directory.Exists(Path.Combine(actionsDirectoryPath, "Icons")))
                    {
                        PrintToLog("Error loading actions: No Icons directory.");
                    }
                    else
                    {
                        string iconsDirectoryPath = Path.Combine(actionsDirectoryPath, "Icons");


                        string actionsJsonPath = Path.Combine(actionsDirectoryPath, "Actions.json");

                        PrintToLog(actionsJsonPath.Split("\\").Last() + " exists.");
                        foreach (var action in ActionsFromJson(actionsJsonPath))
                        {
                            PrintToLog("Loading action: " + action.ActionNameID);
                            string actionIconLocation = Path.Combine(iconsDirectoryPath, action.IconString);
                            action.IconString = ProjectSettings.LocalizePath(actionIconLocation);
                            action.Initialize();
                            DisplayImageDebug(action.IconImage);
							actions.Add(action);
                        }
                    }
                }
            }

        }
        catch
        {
            PrintToLog("Error loading actions");
        }

		
    }

	void DisplayImageDebug(Image image)
	{
		
		
		GetParent().GetNode<TextureRect>("TextureRect").Texture = ImageTexture.CreateFromImage(image);

    }

	/// <summary>
	/// Prints file contents in <paramref name="dir"/> to Log.
	/// </summary>
	/// <param name="dir"></param>
	void PrintFilesInDir(string dir)
	{
        foreach (var textName in Directory.GetFiles(dir))
        {
            PrintToLog(textName);
			PrintFile(textName);
        }
    }

	/// <summary>
	/// Prints text contents of <paramref name="path"/> to Log.
	/// </summary>
	/// <param name="path"></param>
	void PrintFile(string path)
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

	/// <summary>
	/// Returns an array of Actions deserialized from JSON at <paramref name="jsonPath"/>
	/// </summary>
	/// <param name="jsonPath"></param>
	/// <returns></returns>
	public IAction[] ActionsFromJson(string jsonPath)
	{

		var actions = new Action[0];
		GD.Print("-------Reading-------");
		try
		{
			string jsonFile = File.ReadAllText(jsonPath);
			actions = JsonConvert.DeserializeObject<Action[]>(jsonFile);

		}
		catch (Exception e)
		{
			PrintToLog(e.Message);
			//GD.Print(e.Message);
		}
		return (IAction[])actions;
		/*
		actions.Add(new Action("", "JumpAction", "Core", new Vector3(0, Mathf.Sqrt(2 * 15 * 64), 0), 1, 1));
		actions.Add(new Action("", "SprintAction", "Core", null, 1.5f));
		actions.Add(new Action("",  "AttackAction", "Core", null, 0.25f, 0, new List<AvailableHitboxes> { AvailableHitboxes.FrontNear, AvailableHitboxes.Left }, 0.5f)) ;
		actions.Add(new Action("",  "JumpAttackAction", "Core", new Vector3(0, Mathf.Sqrt(2 * 15 * 64), 0), 0.05f, 1, new List<AvailableHitboxes> { AvailableHitboxes.FrontNear }, 0.5f)) ;
		*/
		
	}

	/// <summary>
	/// Converts <paramref name="action"/> to JSON at <paramref name="fileOut"/>
	/// </summary>
	/// <param name="action"></param>
	/// <param name="fileOut"></param>
	private void ActionToJson(IAction action, string fileOut)
	{
		try
		{
			var jsonString = JsonConvert.SerializeObject(action, Formatting.Indented);
			File.WriteAllText(fileOut, jsonString);

		}
		catch (Exception e)
		{
			GD.Print(e.Message);
		}
	}

    
}
