using UnityEngine;

public enum FloorTypes{ GRASS, WATER, CONCRETE, GRAVEL, DIRT, SAND}
public class WalkSfx : SimplerSFX
{
    public FloorTypes currentFloor = FloorTypes.WATER;
    public AudioClip[] sfxGrass;
    public AudioClip[] sfxWater;
    public AudioClip[] sfxConcrete;
    public AudioClip[] sfxGravel;
    public AudioClip[] sfxDirt;
    public AudioClip[] sfxSand;

    public void PlayRandomWalkSounds()
    {
        audioSource.PlayOneShot(sfx[Random.Range(0, sfx.Length)]);
    }

    public void PlayRandomWalkSoundsEnvironment()
    {
        switch (currentFloor)
        {
            case FloorTypes.GRASS:
                audioSource.PlayOneShot(sfxGrass[Random.Range(0, sfxGrass.Length)]);
                break;
            case FloorTypes.WATER:
                audioSource.PlayOneShot(sfxWater[Random.Range(0, sfxWater.Length)]);
                break;
            case FloorTypes.CONCRETE:
                audioSource.PlayOneShot(sfxConcrete[Random.Range(0, sfxConcrete.Length)]);
                break;
            case FloorTypes.GRAVEL:
                audioSource.PlayOneShot(sfxGravel[Random.Range(0, sfxGravel.Length)]);
                break;
            case FloorTypes.DIRT:
                audioSource.PlayOneShot(sfxDirt[Random.Range(0, sfxDirt.Length)]);
                break;
            case FloorTypes.SAND:
                audioSource.PlayOneShot(sfxSand[Random.Range(0, sfxSand.Length)]);
                break;
            default:
                break;
        }
        
    }

    public void SetCurrentWalkEnvironment(FloorTypes newFloor)
    {
        currentFloor = newFloor;
    }

}
