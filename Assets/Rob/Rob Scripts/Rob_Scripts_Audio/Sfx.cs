using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sfx : MonoBehaviour
{
    [Tooltip("Generic Sfx")]
    public AudioClip[] sfx;
    public AudioClip[] onFailSfx;
    public AudioClip[] onSuccessSfx;
    public bool playRandomSfxOnAwake;
    public bool playRandomSfxOnEnable;

    [Tooltip("Will Play Random Sfx on collision")]
    public bool hasCollisionSfx;
    public bool hasCollisionSfxOnTrigger;

    [Tooltip("Collision Sfx")]
    public AudioClip[] onCollisionSfx;
    public AudioClip[] onGetShotSfx;
    public AudioClip[] onShootSfx;
    public AudioClip[] onDeathSfx;
    public AudioClip[] onImpactSfx;

    public bool useGameObjectComponentAudioSource = true;

    public AudioSource audioSource;
    public AudioSource audioSourceCollisionSfx;
    public AudioSource audioSourceImpactSfx;
    public AudioSource audioSourceDeathSfx;

    public string tagCollisionExlusion;

    void Awake()
    {
        if (useGameObjectComponentAudioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (audioSourceCollisionSfx == null)
        {
            audioSourceCollisionSfx = audioSource;
        }
        if (audioSourceImpactSfx == null)
        {
            audioSourceImpactSfx = audioSource;
        }
        if (playRandomSfxOnAwake)
        {
            PlayRandomSfx();
        }
    }


    void OnEnable()
    {
        if (playRandomSfxOnEnable)
        {
            PlayRandomSfx();
        }
    }

    public void PlayRandomSfx()
    {
        audioSource.PlayOneShot(sfx[Random.Range(0, sfx.Length)]);
    }

    public void PlayRandomOnFailSfx()
    {
        audioSource.PlayOneShot(onFailSfx[Random.Range(0, onFailSfx.Length)]);
    }

        public void PlayRandomOnSuccessSfx()
    {
        audioSource.PlayOneShot(onSuccessSfx[Random.Range(0, onSuccessSfx.Length)]);
    }


    public void PlayShootSoundRandomPitch(float randomPitchMin, float randomPitchMax)
    {
        audioSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        audioSource.PlayOneShot(onShootSfx[Random.Range(0, onShootSfx.Length)]);
    }


    void OnTriggerEnter(Collider other)
    {
        if (hasCollisionSfxOnTrigger)
        {
            audioSourceCollisionSfx.PlayOneShot(onCollisionSfx[Random.Range(0, onCollisionSfx.Length)]);
        }
    }

    public void PlayRandomDeathSFX()
    {
        audioSourceDeathSfx.PlayOneShot(onDeathSfx[Random.Range(0, onDeathSfx.Length)]);
    }

    public void PlayRandomImpactHitsSFX()
    {
        audioSourceImpactSfx.PlayOneShot(onImpactSfx[Random.Range(0, onImpactSfx.Length)]);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (hasCollisionSfx)
        {
            audioSourceCollisionSfx.PlayOneShot(onCollisionSfx[Random.Range(0, onCollisionSfx.Length)]);
        }

        if (collision.gameObject.tag == "PlayerProjectile")
        {
            audioSource.PlayOneShot(onShootSfx[Random.Range(0, onCollisionSfx.Length)]);
        }
    }
}
