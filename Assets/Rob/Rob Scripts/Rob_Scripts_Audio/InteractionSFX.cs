using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSFX : SimplerSFX
{
    public void PlayPickupSFX(AudioClip interactableObjectPickupSFX)
    {
        audioSource.PlayOneShot(interactableObjectPickupSFX);
    }
}
