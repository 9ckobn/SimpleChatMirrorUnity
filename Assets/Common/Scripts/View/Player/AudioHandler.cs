using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audios = new List<AudioClip>();

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        source.PlayOneShot(RandomClip());
    }

    private AudioClip RandomClip() => audios[Random.Range(0, audios.Count)];
    
}
