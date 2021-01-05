using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public bool isPersistentDontDestroyOnLoad = true;

    public bool playOnStart = true;
    public bool fadeInOnStart = false;
    public float fadeInTime = 1.0f;
    public float fadeInVolume = 1.0f;


    #region Music
    public AudioClip idle1OutsideHouse;
    public AudioClip idle2FoyerHouse;
    public AudioClip Ambience1Pedal;
    public AudioClip Ambience1Piano;
    public AudioClip Ambience2HornCall;
    public AudioClip Ambience2aHornCall;
    public AudioClip Ambience2bHornCallGlissando;
    public AudioClip actionSuspenseMusic;
    public AudioClip successfulDeliveryMusic;
    public AudioClip successfulDeliveryMusicMaximum;
    public AudioClip failedDeliveryMusic;
    public AudioClip gameOverMusic;
    public AudioClip jazzMusic;
    #endregion

    #region ActionStingerCues
    public AudioClip actionStingerAmajor;
    public AudioClip actionStingerEmajor;
    public AudioClip actionStingerFmajor;
    #endregion
    public AudioClip clipToPlayOnStart;
    public AudioClip clipToFadeTo;

    #region AudioSources
    public AudioSource audioTrack1;
    public AudioSource audioTrack2;

    public AudioSource importantAudioCues;

    public AudioSource currentAudioTrack;
    #endregion
    public AudioMixer mainAudioMixer;

    // Position of each music track

    float _oldIdleMusicPos;
    float _oldActionMusicPos;

    void Awake()
    {
        if (isPersistentDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        currentAudioTrack = audioTrack1;
        if (playOnStart)
        {
            audioTrack1.clip = clipToPlayOnStart;
            audioTrack1.Play();
        }
        if (fadeInOnStart)
        {
            PlayCrossFadeMusic(clipToPlayOnStart, fadeInTime, fadeInVolume);
        }
    }
    #region Main Functions
    public void PlayCrossFadeMusic(AudioClip clipToPlay, float duration = 1f, float targetVolume = 1f, float audioPosition = 0)
    {
        if (currentAudioTrack == audioTrack1)
        {
            StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic1", duration, 0f));
            StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic2", duration, targetVolume));
            currentAudioTrack = audioTrack2;
        }
        else
        {
            StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic2", duration, 0f));
            StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic1", duration, targetVolume));
            currentAudioTrack = audioTrack1;
        }
        currentAudioTrack.time = audioPosition;
        currentAudioTrack.clip = clipToPlay;
        currentAudioTrack.Play();
    }

    public void FadeOutCurrentAudioTrack(float duration = 1f, float targetVolume = 1f)
    {
        if (currentAudioTrack == audioTrack1)
        {
            StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic1", duration, 0f));
        }
        else
        {
            StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic2", duration, 0f));
        }
    }

    public void FadeOutAllMusic(float duration = 1.0f)
    {
        StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic1", duration, 0f));
        StartCoroutine(FadeMixerGroup.StartFade(mainAudioMixer, "VolumeMusic2", duration, 0f));
    }

    public void SetLoopForAllAudioTracks(bool activate)
    {
        audioTrack1.loop = activate;
        audioTrack2.loop = activate;
    }
    #endregion
    #region Specific Music Event Functions


    public void PlayCalmHouseMusic()
    {
        PlayCrossFadeMusic(idle1OutsideHouse, 1, 1, _oldIdleMusicPos);
    }


    public void PlayHornCallMusic()
    {
        PlayCrossFadeMusic(Ambience2aHornCall, 1, 1);
    }


    public void PlayHornCallHorrorMusic()
    {
        PlayCrossFadeMusic(Ambience2bHornCallGlissando, 1, 1);
    }



    public void OnIdleBetweenDelivery()
    {
        if (currentAudioTrack.clip == actionSuspenseMusic)
        {
            _oldActionMusicPos = currentAudioTrack.time;
        }
        PlayCrossFadeMusic(idle1OutsideHouse, 1, 1, _oldIdleMusicPos);
    }

    public void OnPickup()
    {
        if (currentAudioTrack.clip == idle1OutsideHouse)
        {
            _oldIdleMusicPos = currentAudioTrack.time;
        }
        AudioClip actionStingerToPlay = CheckActionStingerTime(_oldActionMusicPos);
        importantAudioCues.PlayOneShot(actionStingerToPlay);
        PlayCrossFadeMusic(actionSuspenseMusic, 0.1f, 1, _oldActionMusicPos);
    }

    public void OnSuccessfulDelivery()
    {
        importantAudioCues.PlayOneShot(successfulDeliveryMusic);
    }
    public void OnSuccessfulDeliveryMaximum()
    {
        importantAudioCues.PlayOneShot(successfulDeliveryMusicMaximum);
    }

    public void OnFailedPickup()
    {
        importantAudioCues.PlayOneShot(failedDeliveryMusic);

    }

    public void GameOverMusic()
    {
        importantAudioCues.PlayOneShot(gameOverMusic);
        currentAudioTrack.Stop();
    }

    #endregion
    #region Stinger Cues Functions
    AudioClip CheckActionStingerTime(float actionClipTime)
    {
        AudioClip thisStingerToPlay;
        if (actionClipTime < 0.10f) // Worse, but probably best key change check ever :DDDDD
        {
            thisStingerToPlay = actionStingerAmajor;
        }
        else if (actionClipTime < 0.25f)
        {
            thisStingerToPlay = actionStingerEmajor;
        }
        else if (actionClipTime < 1.40f)
        {
            thisStingerToPlay = actionStingerAmajor;
        }
        else
        {
            thisStingerToPlay = actionStingerFmajor;
        }

        return thisStingerToPlay;
    }
    #endregion
}


