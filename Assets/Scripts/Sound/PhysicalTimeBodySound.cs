using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTimeBodySound
{
    SoundFXManager soundManager;
    StateMachine controller;

    Transform parentTransform;
    public PhysicalTimeBodySound(StateMachine _controller, Transform _parentTransform)
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundFXManager>();
        parentTransform = _parentTransform;
        controller = _controller;
        controller.AddOnEnterAction(TimeBodyStates.Rewinding, RewindTimeSound);
        controller.AddOnEnterAction(TimeBodyStates.Stoped, StopTimeSound);
        controller.AddOnExitAction(TimeBodyStates.Rewinding, StopRewindTimeSound);
        controller.AddOnExitAction(TimeBodyStates.Stoped, StopStopTimeSound);
        controller.AddOnEnterAction(TimeBodyStates.ControlledRewinding, RewindTimeSoundManual);
        controller.AddOnEnterAction(TimeBodyStates.ControlledReverseRewinding, RewindTimeSoundManual);
        controller.AddOnEnterAction(TimeBodyStates.ControlledStoped, RewindTimeSound);
        controller.AddOnExitAction(TimeBodyStates.ControlledRewinding, StopRewindTimeManualSound);
        controller.AddOnExitAction(TimeBodyStates.ControlledReverseRewinding, StopRewindTimeManualSound);
        controller.AddOnExitAction(TimeBodyStates.ControlledStoped, StopStopTimeSound);
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
        soundManager.StopSound(Sound.TimeRewind);
    }

    private void StopRewindTimeManualSound()
    {
        soundManager.StopSound(Sound.ManualRewind);
    }

    private void StopStopTimeSound()
    {
        soundManager.StopSound(Sound.TimeStop);
    }
}
