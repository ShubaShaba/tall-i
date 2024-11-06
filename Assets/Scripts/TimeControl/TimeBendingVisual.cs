using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBendingVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem focusing;
    [SerializeField] private ParticleSystem rewinding;
    [SerializeField] private ParticleSystem freeze;
    [SerializeField] private ParticleSystem controlledFreeze;
    [SerializeField] private ParticleSystem controlledRewiding;
    [SerializeField] private GameObject respawnAnimation;

    private GameObject respawnFirst;

    private void Awake()
    {
        CancelEverything();
        respawnAnimation.GetComponent<ParticleSystem>().Stop();
        respawnAnimation.GetComponent<ParticleSystem>().Clear();
    }

    public void InitializeVisuals(StateMachine _timeBendingController)
    {
        _timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, RewindAnimation);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.Stoped, FreezeAnimation);
        _timeBendingController.AddOnExitAction(TimeBodyStates.Rewinding, FocusAnimation);
        _timeBendingController.AddOnExitAction(TimeBodyStates.Stoped, FocusAnimation);

        _timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledRewinding, ControlledRewindAnimation);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledReverseRewinding, ControlledRewindAnimation);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledStoped, ControlledFreezeAnimation);
        _timeBendingController.AddOnExitAction(TimeBodyStates.ControlledStoped, FocusAnimation);
        _timeBendingController.AddOnExitAction(TimeBodyStates.ControlledRewinding, FocusAnimation);
        _timeBendingController.AddOnExitAction(TimeBodyStates.ControlledReverseRewinding, FocusAnimation);
    }

    public void CancelEverything()
    {
        focusing.Stop();
        rewinding.Stop();
        freeze.Stop();
        controlledFreeze.Stop();
        controlledRewiding.Stop();
        focusing.Clear();
        rewinding.Clear();
        freeze.Clear();
        controlledFreeze.Clear();
        controlledRewiding.Clear();
    }

    public void FocusAnimation()
    {
        CancelEverything();
        focusing.Play();
    }

    public void RewindAnimation()
    {
        CancelEverything();
        rewinding.Play();
    }

    public void FreezeAnimation()
    {
        CancelEverything();
        freeze.Play();   
    }

    public void ControlledFreezeAnimation()
    {
        CancelEverything();
        controlledFreeze.Play();
        
    }

    public void ControlledRewindAnimation()
    {
        CancelEverything();
        controlledRewiding.Play();
    }

    public void RespawnAnimation(bool isFirst)
    {
        CancelEverything();
        if (respawnFirst != null && isFirst) 
            Destroy(respawnFirst);
        
        if (isFirst)    
            respawnFirst = Instantiate(respawnAnimation, transform.position, Quaternion.identity);
        respawnAnimation.GetComponent<ParticleSystem>().Play();
    }
}
