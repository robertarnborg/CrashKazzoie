using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StandardUnityEvent : MonoBehaviour
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
    [Tooltip("Text without Dialogue Panel")]
    public bool isTextOnly;
    [TextArea(0, 40)]
    public string playerMessageOnTrigger;
    public bool fadeMessageOnTriggerExit = false;
    public bool hasMessageTimeout;
    [Tooltip("Fade message Timer for Both fadeMessageOnTriggerExit and hasMessageTimeout")]
    public float messageTimeout = 5f;
    public TextAnchor textAnchor = TextAnchor.UpperLeft;
    #endregion

    #region Image Panel Fields
    public Sprite panelImage;
    #endregion

    public bool isTriggered;

    public void ActivateTrigger()
    {
        if (isOneShot && !isTriggered) // Is OneShotEvent
        {
            if (hasPlayerMessageOnTrigger)
            {
                ActivateMessage();
            }
            ActivateEvent();
            isTriggered = true;
        }
        else if (isAmountOfTimesTriggered) // Is X Amount of Times Can Be Triggered Event
        {
            if (currentAmountOfTimesTriggered <= amountOfTimesToTrigger)
            {
                if (hasPlayerMessageOnTrigger)
                {
                    ActivateMessage();
                }
                ActivateEvent();
                currentAmountOfTimesTriggered++;
            }
            else
            {
                isTriggered = true;
            }
        }
        else // Is Unlimited Amount Of Events
        {
            if (!isOneShot)
            {
                if (hasPlayerMessageOnTrigger)
                {
                    ActivateMessage();
                }
                ActivateEvent();
            }
        }
    }


    public void ActivateMessage()
    {
        if (isTextOnly)
        {
            TextMessagePanel.Instance.ShowSetOnlyMessageNoBackgroundText(true, true, playerMessageOnTrigger, textAnchor);
            if (hasMessageTimeout)
            {
                TextMessagePanel.Instance.StartMessageOnlyTextTimeout(messageTimeout);
            }
        }
        else
        {
            TextMessagePanel.Instance.SetMessagePanelImageBackground(panelImage);
            TextMessagePanel.Instance.ShowSetMessageText(true, true, playerMessageOnTrigger);
            if (hasMessageTimeout)
            {
                TextMessagePanel.Instance.StartMessageTimeout(messageTimeout);
            }
        }

    }


    public void ActivateEvent()
    {
        if (hasDelayUntilEvent)
        {
            StartCoroutine("InvokeEventOnDelay", eventDelay);
        }
        else
        {
            eventToTrigger.Invoke();
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