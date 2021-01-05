using UnityEngine;

public enum FloorTypes{ GRASS, WOOD, CONCRETE, GRAVEL, DIRT}
public class WalkSfx : SimplerSFX
{
    public FloorTypes currentFloor = FloorTypes.WOOD;
    public AudioClip[] sfxGrass;
    public AudioClip[] sfxWood;
    public AudioClip[] sfxConcrete;
    public AudioClip[] sfxGravel;
    public AudioClip[] sfxDirt;

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
            case FloorTypes.WOOD:
                audioSource.PlayOneShot(sfxWood[Random.Range(0, sfxWood.Length)]);
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
            default:
                break;
        }
        
    }

    public void SetCurrentWalkEnvironment(FloorTypes newFloor)
    {
        currentFloor = newFloor;
    }

}
