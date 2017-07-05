using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageCanvasManager : MonoBehaviour {

    private static MessageCanvasManager instance;
    public static MessageCanvasManager Instance { get { return instance; } }
    public Text message;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        message.color = new Color(message.color.r, message.color.g, message.color.b, 0f);
    }

    public void ShowCanvasMessage(string text, float stayTime) {
        StartCoroutine(ShowMessage(text, stayTime));
    }

    private IEnumerator ShowMessage(string text, float stayTime)
    {
        message.color = new Color(message.color.r, message.color.g, message.color.b, 0f);
        message.text = text;
        float fadeInCurrentTime = 0f;
        float fadeInTime = 3f;
        Color solidColor = new Color(message.color.r, message.color.g, message.color.b, 1f);
        while (message.color.a < 1)
        {
            fadeInCurrentTime += Time.deltaTime;
            message.color = Color.Lerp(message.color, solidColor, fadeInCurrentTime / fadeInTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(stayTime);

        float fadeOutCurrentTime = 0f;
        float fadeOutTime = 3f;
        Color transparentColor = new Color(message.color.r, message.color.g, message.color.b, 0f);
        while (message.color.a > 0)
        {
            fadeOutCurrentTime += Time.deltaTime;
            message.color = Color.Lerp(message.color, transparentColor, fadeOutCurrentTime / fadeOutTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
