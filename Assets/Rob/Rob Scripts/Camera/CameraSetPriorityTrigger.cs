using Cinemachine;
using UnityEngine;

public class CameraSetPriorityTrigger : MonoBehaviour
{
    public int cameraASetPriority;
    public int cameraBSetPriority;

    public CinemachineVirtualCamera cameraA;
    public CinemachineFreeLook cameraB;

    public bool transitionBackOnExit = true;


    private void OnTriggerEnter(Collider other)
    {
        cameraA.Priority = cameraASetPriority;
        cameraB.Priority = cameraBSetPriority;

    }

    private void OnTriggerExit(Collider other)
    {
        if (transitionBackOnExit)
        {
            cameraA.Priority = cameraBSetPriority;
            cameraB.Priority = cameraASetPriority;
        }
    }

}
