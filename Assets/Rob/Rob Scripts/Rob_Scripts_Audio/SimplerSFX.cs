using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimplerSFX : MonoBehaviour
{
    [Tooltip("Generic Sfx")]
    public AudioClip[] sfx;
    public AudioSource audioSource;

    public bool playOnEnable;

    public void PlayRandomSfx()
    {
        audioSource.PlayOneShot(sfx[Random.Range(0, sfx.Length)]);
    }

    private void OnEnable()
    {
     if (playOnEnable)
        {
            PlayRandomSfx();
        }   
    }
}
