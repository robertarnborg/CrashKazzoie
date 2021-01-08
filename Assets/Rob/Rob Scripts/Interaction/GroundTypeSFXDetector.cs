using System;
using UnityEngine;

public class GroundTypeSFXDetector : MonoBehaviour
{

    public float detectionRayCastLength = 10f;
    public LayerMask detectionLayerMask;

    private WalkSfx _playerWalkSfx;

    public InteractionSFX interactionSFX;

    private static GroundTypeSFXDetector _instance;
    public static GroundTypeSFXDetector Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            _instance._playerWalkSfx = FindObjectOfType<WalkSfx>();
            _instance.interactionSFX = GetComponent<InteractionSFX>();
        }

    }

    private void FixedUpdate()
    {
        GroundTypeChecker();
    }

    private void GroundTypeChecker()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * detectionRayCastLength, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, detectionRayCastLength, detectionLayerMask))
        {
            if(hit.transform.gameObject.GetComponent<FloorTypeTriggers>() != null)
            {
                FloorTypeTriggers thisFloorTrigger = hit.transform.gameObject.GetComponent<FloorTypeTriggers>();
                _playerWalkSfx.SetCurrentWalkEnvironment(thisFloorTrigger.currentFloor);
            }
        }
        //Debug.DrawRay(rayCastTransforms[i].position, currentDirection * detectionRayCastLength, Color.red);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if(other.gameObject.GetComponent<GenericUnityEventTrigger>() != null) // MORE OPTIMIZED CHECK ONLY ONE PLACE FROM ONE SCRIPT BUT NOT THIS PROJECT :P ...
    //    //{
    //    //    GenericUnityEventTrigger thisTrigger = other.gameObject.GetComponent<GenericUnityEventTrigger>();
    //    //    if (thisTrigger.isTriggerOnEnter)
    //    //    {
    //    //        thisTrigger.ActivateTrigger();
    //    //    }
    //    //    else if (thisTrigger.isTriggerOnInteraction)
    //    //    {
    //    //        isCanInteract = true;
    //    //        _trigger = thisTrigger;
    //    //    }
    //    //}
    //    if(other.gameObject.GetComponent<FloorTypeTriggers>() != null)
    //    {
    //        FloorTypeTriggers thisFloorTrigger = other.gameObject.GetComponent<FloorTypeTriggers>();
    //        _playerWalkSfx.SetCurrentWalkEnvironment(thisFloorTrigger.currentFloor);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.GetComponent<GenericUnityEventTrigger>() != null)
    //    {
    //        GenericUnityEventTrigger thisTrigger = other.gameObject.GetComponent<GenericUnityEventTrigger>();
    //        isCanInteract = false;
    //        _trigger = null;
    //    }
    //}
}
