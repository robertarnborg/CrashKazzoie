using UnityEngine;

public class MusicArea : MonoBehaviour
{
    public AudioClip musicToPlay;

    [Tooltip("TO DO, IS BUGGY!")]
    public bool isFadeWhenExitArea;

    public bool isFaderOnly;
    public bool isFadeOutCurrentMusicArea;
    public bool isFadeOutAllMusicAreas;

    public float musicFadeInDuration = 2f;
    public float musicFadeInVolume = 1f;

    public float currentmusicTime;
}
