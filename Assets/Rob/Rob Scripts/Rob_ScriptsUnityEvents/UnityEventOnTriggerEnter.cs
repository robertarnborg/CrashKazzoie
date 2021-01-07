using System.Collections;
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

    public bool hasDelayUntilEvent;
    public float eventDelay = 5.0f;

    #region Text Message Fields
    public bool hasPlayerMessageOnTrigger;
    [TextArea]
    public string playerMessageOnTrigger;
    public bool fadeMessageOnTriggerExit = false;
    public bool hasMessageTimeout;
    [Tooltip("Fade message Timer for Both fadeMessageOnTriggerExit and hasMessageTimeout")]
    public float messageTimeout = 5f;
    public TextAnchor textAnchor = TextAnchor.UpperLeft;
    #endregion


    public bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActivateTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
                if (fadeMessageOnTriggerExit)
            {
                TextMessagePanel.Instance.StartMessageTimeout(messageTimeout);
            }
        }
    }

    public void ActivateTrigger()
    {
        if (isOneShot && !isTriggered)
        {
            if (hasPlayerMessageOnTrigger)
            {
                ActivateMessage();
            }
            if (hasDelayUntilEvent)
            {
                StartCoroutine("InvokeEventOnDelay", eventDelay);
            }
            else
            {
                eventToTrigger.Invoke();
            }
            isTriggered = true;
        }
        else if (isAmountOfTimesTriggered)
        {
            if (currentAmountOfTimesTriggered <= amountOfTimesToTrigger)
            {
                if (hasDelayUntilEvent)
                {
                    StartCoroutine("InvokeEventOnDelay", eventDelay);
                }
                else
                {
                    eventToTrigger.Invoke();
                }
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
                if (hasPlayerMessageOnTrigger)
                {
                    ActivateMessage();
                }
                if (hasDelayUntilEvent)
                {
                    StartCoroutine("InvokeEventOnDelay", eventDelay);
                }
                else
                {
                    eventToTrigger.Invoke();
                }
            }
        }
    }

    public void ActivateMessage()
    {
        TextMessagePanel.Instance.ShowSetMessageText(true, true, playerMessageOnTrigger);
        if (hasMessageTimeout)
        {
            TextMessagePanel.Instance.StartMessageTimeout(messageTimeout);
        }
    }

    public IEnumerator InvokeEventOnDelay(float eventDelay)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f) // In a while loop while counting down
        {
            normalizedTime += Time.deltaTime / eventDelay;
            yield return null;
        }
        eventToTrigger.Invoke();
    }


    public void InvokeTheEvent()
    {
        eventToTrigger.Invoke();
    }
}
