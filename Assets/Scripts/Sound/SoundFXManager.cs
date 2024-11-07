using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip Time_Stop_Clip;
    [SerializeField] private AudioClip Constant_Time_Stop_Clip;
    [SerializeField] private AudioClip Time_Rewind_Clip;
    [SerializeField] private AudioClip Manual_Rewind_Clip;
    [SerializeField] private AudioClip HardReset_Clip;
    [SerializeField] private AudioClip Heavy_Metal_Impact_Clip;
    [SerializeField] private AudioClip Metal_Clang_Clip;
    [SerializeField] private AudioClip Thud1_Clip;
    [SerializeField] private AudioClip Thud2_Clip;
    [SerializeField] private AudioClip Thud3_Clip;
    [SerializeField] private AudioClip Thud4_Clip;
    [SerializeField] private AudioClip Soft_Thud_Clip;
    [SerializeField] private AudioClip Stone_Impact_Clip;
    [SerializeField] private AudioClip Ambience_hum_Clip;
    [SerializeField] private AudioClip Ambience2_moody_Clip;
    [SerializeField] private AudioClip Ambience3_rumble_Clip;
    [SerializeField] private AudioClip Robot_Movement_Clip;
    [SerializeField] private AudioClip Robot_Jump_Clip;
    [SerializeField] private AudioClip Robot_Beep1_Clip;
    [SerializeField] private AudioClip Robot_Beep2_Clip;
    [SerializeField] private AudioClip Robot_Beep3_Clip;
    [SerializeField] private AudioClip Robot_Beep4_Clip;
    [SerializeField] private AudioClip Stone_Scraping_Short_Clip;
    [SerializeField] private AudioClip Stone_Scraping_Clip;
    [SerializeField] private AudioClip Generator_Clip;
    [SerializeField] private AudioSource audioSourceRef;
    private Dictionary<Sound, AudioClip> audioClips;
    private Dictionary<(Sound, Transform), AudioSource> audioSources;
    [SerializeField] private Dictionary<Sound, SoundSettings> soundSettings;
    [SerializeField] private AudioClip ambientForTheScene = null;

    void Start()
    {
        audioSources = new Dictionary<(Sound, Transform), AudioSource>();
        audioClips = new Dictionary<Sound, AudioClip>()
    {
        { Sound.TimeStop, Time_Stop_Clip },
        { Sound.ConstantTimeStop, Constant_Time_Stop_Clip },
        { Sound.TimeRewind, Time_Rewind_Clip },
        { Sound.ManualRewind, Manual_Rewind_Clip },
        { Sound.HardReset, HardReset_Clip },
        { Sound.HeavyMetalImpact, Heavy_Metal_Impact_Clip },
        { Sound.MetalClang, Metal_Clang_Clip },
        { Sound.Thud1, Thud1_Clip },
        { Sound.Thud2, Thud2_Clip },
        { Sound.Thud3, Thud3_Clip },
        { Sound.Thud4, Thud4_Clip },
        { Sound.SoftThud, Soft_Thud_Clip },
        { Sound.StoneImpact, Stone_Impact_Clip },
        { Sound.AmbienceHum, Ambience_hum_Clip },
        { Sound.AmbienceMoody, Ambience2_moody_Clip },
        { Sound.AmbienceRumble, Ambience3_rumble_Clip },
        { Sound.RobotMovement, Robot_Movement_Clip },
        { Sound.RobotJump, Robot_Jump_Clip},
        { Sound.RobotBeep1, Robot_Beep1_Clip },
        { Sound.RobotBeep2, Robot_Beep2_Clip },
        { Sound.RobotBeep3, Robot_Beep3_Clip },
        { Sound.RobotBeep4, Robot_Beep4_Clip },
        { Sound.StoneScrapingShort, Stone_Scraping_Short_Clip },
        { Sound.StoneScraping, Stone_Scraping_Clip },
        { Sound.Generator, Generator_Clip }
    };

        soundSettings = new Dictionary<Sound, SoundSettings>()
    {
        { Sound.TimeStop, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.ConstantTimeStop, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.TimeRewind, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.ManualRewind, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.HardReset, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.HeavyMetalImpact, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.MetalClang, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.Thud1, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.Thud2, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.Thud3, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.Thud4, new SoundSettings(audioSourceRef.pitch, 0.7f, audioSourceRef.spatialBlend) },
        { Sound.SoftThud, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.StoneImpact, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.AmbienceHum, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.AmbienceMoody, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.AmbienceRumble, new SoundSettings(audioSourceRef.pitch, 0.7f, 0.2f) },
        { Sound.RobotMovement, new SoundSettings(0.6f, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotJump, new SoundSettings(audioSourceRef.pitch, 0.3f, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep1, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep2, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep3, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep4, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.StoneScrapingShort, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.StoneScraping, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.Generator, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend)}
    };

        StartAmbient();
    }

    public void StartAmbient()
    {
        PlaySound(Sound.AmbienceRumble, true, transform, ambientForTheScene);
    }

    public void StopAmbient()
    {
        StopSound(Sound.AmbienceRumble, transform);
    }

    public void PlaySound(Sound sound, bool _loop, Transform _parent, AudioClip specifyClip = null)
    {
        if (!audioSources.ContainsKey((sound, _parent)))
            audioSources.Add((sound, _parent), Instantiate(audioSourceRef, _parent));

        AudioSource source = audioSources[(sound, _parent)];
        if (source.transform.parent != _parent)
        {
            source.transform.position = _parent.position;
            source.transform.SetParent(_parent);
        }

        if (source.isPlaying && _loop) return;
        source.loop = _loop;
        source.Stop();

        if (soundSettings.ContainsKey(sound))
        {
            SoundSettings settings = soundSettings[sound];
            source.pitch = settings.pitch;
            source.volume = settings.volume;
            source.spatialBlend = settings.spatialBlend;
        }
        source.clip = specifyClip == null ? audioClips[sound] : specifyClip;
        source.Play();
    }

    public void StopSound(Sound sound, Transform _parent)
    {
        if (!audioSources.ContainsKey((sound, _parent))) { audioSources.Add((sound, _parent), Instantiate(audioSourceRef, transform)); }
        AudioSource source = audioSources[(sound, _parent)];
        source.Stop();
    }
}
