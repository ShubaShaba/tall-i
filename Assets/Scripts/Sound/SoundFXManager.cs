using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NOTE: THIS GIGANTIC MAPS WHERE CHATGPT GENERATED, the rest of the code and the idea is original*
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
    [SerializeField] private AudioClip Robot_Beep1_Clip;
    [SerializeField] private AudioClip Robot_Beep2_Clip;
    [SerializeField] private AudioClip Robot_Beep3_Clip;
    [SerializeField] private AudioClip Robot_Beep4_Clip;
    [SerializeField] private AudioClip Stone_Scraping_Short_Clip;
    [SerializeField] private AudioClip Stone_Scraping_Clip;
    [SerializeField] private AudioClip Generator_Clip;
    [SerializeField] private AudioSource audioSourceRef;
    private Dictionary<Sound, AudioClip> audioClips;
    private Dictionary<Sound, AudioSource> audioSources;
    [SerializeField] private Dictionary<Sound, SoundSettings> soundSettings;

    void Start()
    {
        audioSources = new Dictionary<Sound, AudioSource>();
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
        { Sound.Thud4, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.SoftThud, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.StoneImpact, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.AmbienceHum, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.AmbienceMoody, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.AmbienceRumble, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotMovement, new SoundSettings(0.6f, 0.4f, 1f) },
        { Sound.RobotBeep1, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep2, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep3, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.RobotBeep4, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.StoneScrapingShort, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.StoneScraping, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend) },
        { Sound.Generator, new SoundSettings(audioSourceRef.pitch, audioSourceRef.volume, audioSourceRef.spatialBlend)}
    };
    }


    public void PlaySound(Sound sound, bool _loop, Transform _parent)
    {
        if (!audioSources.ContainsKey(sound))
        {
            audioSources.Add(sound, Instantiate(audioSourceRef, _parent));
            audioSources[sound].transform.position = _parent.position;
            audioSources[sound].transform.SetParent(_parent);
        }
        AudioSource source = audioSources[sound];

        if (source.isPlaying) return;
        source.loop = _loop;

        if (soundSettings.ContainsKey(sound))
        {
            SoundSettings settings = soundSettings[sound];
            source.pitch = settings.pitch;
            source.volume = settings.volume;
            source.spatialBlend = settings.spatialBlend;
        }
        source.PlayOneShot(audioClips[sound]);
    }

    public void StopSound(Sound sound)
    {
        if (!audioSources.ContainsKey(sound)) { audioSources.Add(sound, Instantiate(audioSourceRef, transform)); }
        AudioSource source = audioSources[sound];
        source.Stop();
    }
}
