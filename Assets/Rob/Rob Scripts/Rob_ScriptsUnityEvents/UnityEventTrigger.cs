using UnityEngine;
using UnityEngine.Events;

public class UnityEventTrigger : MonoBehaviour
{
    public UnityEvent eventToTrigger;

    [Tooltip("Only Fire Event Once")]
    public bool isOneShot = true;
    [Tooltip("Fire X amount of Times")]
    public bool isAmountOfTimesTriggered;
    public int amountOfTimesToTrigger;
    public int currentAmountOfTimesTriggered;
    #region Text Message Fields
    [TextArea]
    public bool hasPlayerMessageOnTrigger;
    public string playerMessageOnTrigger;
    public float messageTimeout = 5f;
    public TextAnchor textAnchor = TextAnchor.UpperLeft;
    #endregion


    public bool isTriggered;

    public void ActivateTrigger()
    {
        if (isOneShot && !isTriggered)
        {
            if (hasPlayerMessageOnTrigger)
            {
                TextMessagePanel.Instance.ShowSetMessageText(true, true, playerMessageOnTrigger, textAnchor);
                TextMessagePanel.Instance.StartMessageTimeout(messageTimeout);
            }
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

    public void InvokeTheEvent()
    {
        eventToTrigger.Invoke();
    }
}
