using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextMessagePanel : MonoBehaviour
{
    #region MessagePanelWithBackGround
    public GameObject narrativeTextPanel;
    public TMP_Text narrativeTextMessage;
    public Animator narrativeTextMessagePanelAnim;

    public Sprite narrativeTextPanelImageDefault;
    public Image narrativeTextPanelImage;
    #endregion

    #region TextWithoutBackGround
    public GameObject textPanel;
    public Text textMessage;
    public Animator messageAnim;
    #endregion

    private static TextMessagePanel _instance;
    
    public static TextMessagePanel Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            // Set MessagePanelWithBackground References
            narrativeTextMessagePanelAnim = _instance.GetComponent<Animator>();
            narrativeTextPanelImage = _instance.transform.GetChild(0).gameObject.GetComponent<Image>();

            // Set MessageWithoutBackground References
            textPanel = _instance.transform.GetChild(1).gameObject;
            messageAnim = _instance.transform.GetChild(1).GetComponent<Animator>();
            textMessage = _instance.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();


        }
    }

    // THIS COULD USE A SPLITTING INTO TWO C# OVERLOAD... To Think About for the future, optional parameters

    public void ShowSetMessageText(bool showMessagePanel, 
                                   bool setMessageText = true,
                                   string message = "",
                                   
                                   bool hasMessageFadeOut = false,
                                   float messageFadeTime = 2.0f)
    {
        if (showMessagePanel)
        {
            narrativeTextPanel.SetActive(showMessagePanel);
            if (setMessageText)
            {
                narrativeTextMessage.text = message;
            }
            narrativeTextMessagePanelAnim.Play("MessagePanel_FadeIn");
        }
        else
        {
            if(narrativeTextPanel.activeSelf == true)
            {
                if (hasMessageFadeOut)
                {
                    StartMessageTimeout(messageFadeTime);
                }
                else
                {
                    narrativeTextMessagePanelAnim.Play("MessagePanel_FadeOut");
                }
            }
        }
    }

    public void ShowSetOnlyMessageNoBackgroundText(bool showMessage,
                                   bool setMessageText = true,
                                   string message = "",
                                   TextAnchor textAnchor = TextAnchor.MiddleCenter,
                                   bool hasMessageFadeOut = false,
                                   float messageFadeTime = 2.0f)
    {
        if (showMessage)
        {
            textMessage.alignment = textAnchor;
            textPanel.SetActive(showMessage);
            if (setMessageText)
            {
                textMessage.text = message;
            }
            messageAnim.Play("MessagePanel_FadeIn");
        }
        else
        {
            if (textPanel.activeSelf == true)
            {
                if (hasMessageFadeOut)
                {
                    StartMessageTimeout(messageFadeTime);
                }
                else
                {
                    messageAnim.Play("MessagePanel_FadeOut");
                }
            }
        }
    }



    public void SetMessagePanelImageBackground(Sprite setBackGroundImage) // This Sets Background Image of Narrative Text Panel
    {
        narrativeTextPanelImage.sprite = setBackGroundImage;
    }

    public void ResetMessagePanelImageBackground()
    {
        narrativeTextPanelImage.sprite = narrativeTextPanelImageDefault;
    }

    public void StartMessageTimeout(float messageTimeOutDissappear)
    {
        StartCoroutine("MessageFadeTimeOut", messageTimeOutDissappear);
    }


    public IEnumerator MessageFadeTimeOut(float messageTimeOutDissappear)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f) // In a while loop while counting down
        {
            normalizedTime += Time.deltaTime / messageTimeOutDissappear;
            yield return null;
        }
        narrativeTextMessagePanelAnim.Play("MessagePanel_FadeOut");
    }
}
