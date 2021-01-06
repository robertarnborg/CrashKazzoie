using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnTriggerEnter : MonoBehaviour
{
    public UnityEvent eventToTrigger;

    [Tooltip("Only Fire Event Once")]
    public bool isOneShot = true;
    [Tooltip("Fire X amount of Times")]
    public bool isAmountOfTimesTriggered;
    public int amountOfTimesToTrigger;
    public int currentAmountOfTimesTriggered;

    public bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (isOneShot && !isTriggered)
        {
            eventToTrigger.Invoke();
            isTriggered = true;
        }
        else if (isAmountOfTimesTriggered)
        {
            if (currentAmountOfTimesTriggered <= amountOfTimesToTrigger)
            {
                eventToTrigger.Invoke();
                currentAmountOfTimesTriggered++;
            }
            else
            {
                isTriggered = true;
            }
        }
        else
        {
            if (!isOneShot)
            {
                eventToTrigger.Invoke();
            }
        }
    }
}
