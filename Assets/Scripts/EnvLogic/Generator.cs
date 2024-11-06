using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new TwoStatesTimeBendingController();
        twoStatesTimeBodyHelper = new TwoStatesTimeBodyHelper(visuals, timeBendingController);

        timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, OnEnterRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Natural, OnEnterNatural);

        forwardCoroutine = ForwardSequence();
        backwardCoroutine = BackwardSequence();
    }

    public void Focus() { twoStatesTimeBodyHelper.Focus(); }

    public void UnFocus() { twoStatesTimeBodyHelper.UnFocus(); }

    public void ToggleRewind() { twoStatesTimeBodyHelper.ToggleState(); }

    public override void Interact()
    {
        forwardCoroutine = ForwardSequence();
        StartCoroutine(forwardCoroutine);
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
        backwardCoroutine = BackwardSequence();
        StartCoroutine(backwardCoroutine);
    }
    private void OnEnterNatural()
    {
        visuals.FocusAnimation();
        StopCoroutine(backwardCoroutine);
    }

    private IEnumerator ForwardSequence()
    {
        bool condition = isInjected() && !isTriggered && timeBendingController.GetCurrentState() == TimeBodyStates.Natural;
        if (condition)
        {
            StopCoroutine(backwardCoroutine);
            openCloseAnimation.SetBool("IsEnabled", !isTriggered);
        }
        yield return new WaitForSeconds(1);
        if (condition) dissolveAnimation.PlayAnimation();
        yield return new WaitForSeconds(2);
        if (condition)
        {
            Switch();
            isTriggered = true;
        }
    }

    private IEnumerator BackwardSequence()
    {
        if (isTriggered)
        {
            StopCoroutine(forwardCoroutine);
            dissolveAnimation.CancelAnimation();
        }
        yield return new WaitForSeconds(2);
        if (isTriggered) openCloseAnimation.SetBool("IsEnabled", !isTriggered);
        yield return new WaitForSeconds(1);

        if (isTriggered)
        {
            Switch();
            isTriggered = false;
        }
        ToggleRewind();
    }
}