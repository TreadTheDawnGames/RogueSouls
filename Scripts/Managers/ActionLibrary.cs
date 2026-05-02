using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ActionLibrary : Node
{
	IAction[] Actions;
	IAction[] MovementActions;
	IAction[] AttackActions;
	IAction[] PassiveActions;

  

    public ActionLibrary(List<IAction> actions) 
	{
		List<IAction> attack = new List<IAction>();
		List<IAction> movement = new List<IAction>();
		List<IAction> passive = new List<IAction>();

		Actions = actions.ToArray();
		foreach (IAction action in Actions)
		{
            switch (action.ActionType)
            {
                case ActionTypeEnum.Attack:
					attack.Add(action);
                    break;
                case ActionTypeEnum.Movement:
					movement.Add(action);
                    break;
                case ActionTypeEnum.Passive:
                    passive.Add(action);
					break;
            }
        }
		AttackActions = attack.ToArray();
		MovementActions = movement.ToArray();
		PassiveActions = passive.ToArray();
    }

    public IAction RandomAction()
	{
		return Actions[GD.Randi()%Actions.Length-1];
	}

	public IAction RandomAttackAction()
	{
		return AttackActions[GD.Randi() % AttackActions.Length - 1];
    }

	public IAction RandomPassiveAction()
	{
        return PassiveActions[GD.Randi() % PassiveActions.Length - 1];

    }
	
	public IAction RandomMovementAction()
	{
        return MovementActions[GD.Randi() % MovementActions.Length - 1];

    }
}
