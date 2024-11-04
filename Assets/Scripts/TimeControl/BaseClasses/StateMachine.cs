using System;
using System.Collections.Generic;

public class StateMachine
{
    protected TimeBodyStates currentState;
    protected TimeBodyStates previousState;
    protected Dictionary<TimeBodyStates, List<Action>> onEnterActions;
    protected Dictionary<TimeBodyStates, List<Action>> onExitActions;
    
    public void AddOnExitAction(TimeBodyStates state, Action action)
    {
        if (onExitActions.ContainsKey(state))
        {
            onExitActions[state].Add(action);
        }
        else
        {
            onExitActions.Add(state, new List<Action>() { action });
        }
    }

    public void AddOnEnterAction(TimeBodyStates state, Action action)
    {
        if (onEnterActions.ContainsKey(state))
        {
            onEnterActions[state].Add(action);
        }
        else
        {
            onEnterActions.Add(state, new List<Action>() { action });
        }
    }

    public void SetState(TimeBodyStates state)
    {
        if (state == currentState)
            return;

        if (onExitActions.ContainsKey(currentState))
        {
            foreach (Action action in onExitActions[currentState])
                action();
        }

        previousState = currentState;
        currentState = state;

        if (onEnterActions.ContainsKey(currentState))
        {
            foreach (Action action in onEnterActions[currentState])
                action();
        }
    }

    public TimeBodyStates GetCurrentState()
    {
        return currentState;
    }

    public TimeBodyStates GetPreviousState()
    {
        return previousState;
    }
}