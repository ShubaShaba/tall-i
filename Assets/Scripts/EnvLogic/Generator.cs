using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
NOTE!!!!!!!!!!: 

ANIMATION IS A MESS
NEED TO FIX THIS, HAVE NOT TIME
IT WORKS FOR NOW 

*/
public class Generator : KeyHole, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private GlowEffect dissolveAnimation;
    [SerializeField] private Animator openCloseAnimation;
    private TimeBendingVisual visuals;
    private TwoStatesTimeBendingController timeBendingController;
    private TwoStatesTimeBodyHelper twoStatesTimeBodyHelper;
    private IEnumerator forwardCoroutine;
    private IEnumerator backwardCoroutine;
    private bool isRunning;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new TwoStatesTimeBendingController();
        twoStatesTimeBodyHelper = new TwoStatesTimeBodyHelper(visuals, timeBendingController);

        timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, OnEnterRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Natural, OnEnterNatural);

        forwardCoroutine = ForwardSequence();
        backwardCoroutine = BackwardSequence();
        isRunning = false;
    }

    public void Focus() { twoStatesTimeBodyHelper.Focus(); }

    public void UnFocus() { twoStatesTimeBodyHelper.UnFocus(); }

    public void ToggleRewind()
    {
        if (timeBendingController.GetCurrentState() == TimeBodyStates.Natural)
        {
            StopAllCoroutines();
            twoStatesTimeBodyHelper.ToggleState();
        }
    }

    public override void Interact()
    {
        if (isInjected() && !isTriggered && timeBendingController.GetCurrentState() == TimeBodyStates.Natural)
        {
            Invoke("delayedSwitchForward", 2f);
            forwardCoroutine = ForwardSequence();
            StartCoroutine(forwardCoroutine);
        }
    }

    public override void Eject() { if (!isTriggered) base.Eject(); }
    public void ManualBackward() { return; }
    public void ManualForward() { return; }
    public void ToggleFreeze() { return; }
    public void ToggleManualControl() { return; }
    public bool IsInManualMode() { return false; }
    public TimeBodyStates GetCurrentState() { return timeBendingController.GetCurrentState(); }

    private void OnEnterRewind()
    {
        visuals.RewindAnimation();
        if (isTriggered)
        {
            Switch();
            Invoke("delayedSwitchBackward", 2f);
            backwardCoroutine = BackwardSequence();
            StartCoroutine(backwardCoroutine);
        }
        else
        {
            CancelInvoke("delayedSwitchForward");
            StopAllCoroutines();
            backwardCoroutine = BackwardSequence();
            StartCoroutine(backwardCoroutine);
        }
    }
    private void OnEnterNatural()
    {
        visuals.FocusAnimation();
        if (isTriggered)
        {
            Switch();
            CancelInvoke("delayedSwitchBackward");
            StopAllCoroutines();
            openCloseAnimation.SetBool("IsEnabled", true);
            dissolveAnimation.PlayAnimation();
        }
        else
        {
            StopAllCoroutines();
            dissolveAnimation.CancelAnimation();
        }
    }

    private void delayedSwitchForward()
    {
        if (isInjected() && !isTriggered && timeBendingController.GetCurrentState() == TimeBodyStates.Natural)
        {
            Switch();
            isTriggered = true;
        }
    }

    private void delayedSwitchBackward()
    {
        if (isTriggered)
        {
            isTriggered = false;
        }
        ToggleRewind();
    }

    private IEnumerator ForwardSequence()
    {
        openCloseAnimation.SetBool("IsEnabled", true);
        yield return new WaitForSeconds(1);
        dissolveAnimation.PlayAnimation();
        yield return new WaitForSeconds(1);
    }

    private IEnumerator BackwardSequence()
    {
        dissolveAnimation.CancelAnimation();
        yield return new WaitForSeconds(1);
        openCloseAnimation.SetBool("IsEnabled", false);
        yield return new WaitForSeconds(1);
        timeBendingController.SetState(TimeBodyStates.Natural);
    }
}