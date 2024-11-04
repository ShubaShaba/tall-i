using System;
using System.Collections.Generic;

public class StateMachine
{
    protected TimeBodyStates currentState;
    protected TimeBodyStates previousState;
    protected Dictionary<TimeBodyStates, List<Action>> onEnterActions;
    protected Dictionary<TimeBodyStates, List<Action>> duringStateActions;
    protected Dictionary<TimeBodyStates, List<Action>> onExitActions;

    protected void Init() {
        onExitActions = new Dictionary<TimeBodyStates, List<Action>>();
        onEnterActions = new Dictionary<TimeBodyStates, List<Action>>();
        duringStateActions  = new Dictionary<TimeBodyStates, List<Action>>();
        currentState = TimeBodyStates.Natural;
        previousState = TimeBodyStates.Natural;
    }

    public void AddOnExitAction(TimeBodyStates state, Action action)
    {
        AddAction(onExitActions, state, action);
    }

    public void AddOnEnterAction(TimeBodyStates state, Action action)
    {
        AddAction(onEnterActions, state, action);
    }

    public void AddDuringStateActionFixedUpdate(TimeBodyStates state, Action action)
    {
        AddAction(duringStateActions, state, action);
    }

    public void SetState(TimeBodyStates state)
    {
        if (state == currentState)
            return;

        ExecuteActions(onExitActions);
        previousState = currentState;
        currentState = state;
        ExecuteActions(onEnterActions);
    }

    public TimeBodyStates GetCurrentState()
    {
        return currentState;
    }

    public TimeBodyStates GetPreviousState()
    {
        return previousState;
    }

    private void AddAction(Dictionary<TimeBodyStates, List<Action>> actionsSet, TimeBodyStates state, Action action)
    {
        if (actionsSet.ContainsKey(state))
        {
            actionsSet[state].Add(action);
        }
        else
        {
            actionsSet.Add(state, new List<Action>() { action });
        }
    }

    protected void ExecuteActions(Dictionary<TimeBodyStates, List<Action>> actions)
    {
        if (actions.ContainsKey(currentState))
        {
            foreach (Action action in actions[currentState])
                action();
        }
    }
}