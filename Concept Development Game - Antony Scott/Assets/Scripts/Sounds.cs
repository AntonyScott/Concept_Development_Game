using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] //slider for volume
    public float volume;
    [Range(1f, 3f)] //slider for pitch
    public float pitch;

    [HideInInspector] //hides audio source
    public AudioSource source;
}
