using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTimeBodySound
{
    private SoundFXManager soundManager;

    Transform parentTransform;
    public PhysicalTimeBodySound(Transform _parentTransform)
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundFXManager>();
        parentTransform = _parentTransform;
    }

    public void ConnectSound(StateMachine _timeBendingController)
    { 
        _timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, RewindTimeSound);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.Stoped, StopTimeSound);
        _timeBendingController.AddOnExitAction(TimeBodyStates.Rewinding, StopRewindTimeSound);
        _timeBendingController.AddOnExitAction(TimeBodyStates.Stoped, StopStopTimeSound);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledRewinding, RewindTimeSoundManual);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledReverseRewinding, RewindTimeSoundManual);
        _timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledStoped, StopTimeSound);
        _timeBendingController.AddOnExitAction(TimeBodyStates.ControlledRewinding, StopRewindTimeManualSound);
        _timeBendingController.AddOnExitAction(TimeBodyStates.ControlledReverseRewinding, StopRewindTimeManualSound);
        _timeBendingController.AddOnExitAction(TimeBodyStates.ControlledStoped, StopStopTimeSound);
    }

    private void RewindTimeSound()
    {
        soundManager.PlaySound(Sound.TimeRewind, true, parentTransform);
    }

    private void RewindTimeSoundManual()
    {
        soundManager.PlaySound(Sound.ManualRewind, true, parentTransform);
    }

    private void StopTimeSound()
    {
        soundManager.PlaySound(Sound.TimeStop, false, parentTransform);

    }

    private void StopRewindTimeSound()
    {
        soundManager.StopSound(Sound.TimeRewind, parentTransform);
    }

    private void StopRewindTimeManualSound()
    {
        soundManager.StopSound(Sound.ManualRewind, parentTransform);
    }

    private void StopStopTimeSound()
    {
        soundManager.StopSound(Sound.TimeStop, parentTransform);
    }

    public void HardResetSound()
    {
        soundManager.PlaySound(Sound.HardReset, false, parentTransform);
    }
}
