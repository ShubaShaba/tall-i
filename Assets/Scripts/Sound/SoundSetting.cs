public struct SoundSettings
{
    public float pitch;
    public float volume;
    public float spatialBlend;

    public SoundSettings(float pitch, float volume, float spatialBlend)
    {
        this.pitch = pitch;
        this.volume = volume;
        this.spatialBlend = spatialBlend;
    }
}
