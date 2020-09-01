using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum DialogState
{
    active,
    unactive
}
public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI titleDisplay;
    public TextMeshProUGUI textDisplay;
    public GameObject dialogBox;
    public float typingSpeed;
    public float timeBetweenPhrases;
    public DialogState currentState;
    public bool canpass;
    public Image stateImage;
    public Sprite continueImage;
    public Sprite finishedImage;
    public Sprite writingImage;

    public SignalSend dialogIsFinished;

    private Queue<string> sentenceQueue;
    private bool _isTyped;


    private void Start()
    {
        sentenceQueue = new Queue<string>();
        currentState = DialogState.unactive;
    }
    public void StartDialog(Dialog dialog)
    {
        sentenceQueue.Clear();
        dialogBox.SetActive(true);
        titleDisplay.text = dialog.name;
        _isTyped = dialog.isTyped;
        foreach (string sentence in dialog.sentences)
        {
            sentenceQueue.Enqueue(sentence);
        }
        currentState = DialogState.active;
        DisplayNextSentence();
        
    }
    public void DisplayNextSentence()
    {
        canpass = false;
        if (sentenceQueue.Count == 0)
        {
            EndDialog();
            return;
        }
        
        if(_isTyped == true)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayTyped());
        }
        else
        {
            string sentence = sentenceQueue.Dequeue();
            textDisplay.text = sentence;
            canpass = true;
            stateImage.sprite = continueImage;
            if (sentenceQueue.Count == 0)
            {
                stateImage.sprite = finishedImage;
            }
        }
        StartCoroutine(Blink());
    }
    
    private IEnumerator DisplayTyped()
    {
        string sentence = sentenceQueue.Dequeue();
        textDisplay.text = "";
        stateImage.sprite = writingImage;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            FindObjectOfType<AudioManager>().Play("TalkingSound");
            yield return new WaitForSeconds(typingSpeed);
            if (Input.GetKeyDown("space"))
            {
                textDisplay.text = sentence;
                break;
            }
        }
        stateImage.sprite = continueImage;
        if (sentenceQueue.Count == 0)
        {
            stateImage.sprite = finishedImage;
        }
        canpass = true;
        yield return new WaitForSeconds(timeBetweenPhrases);
        DisplayNextSentence();
    }


    public void EndDialog()
    {
        StopAllCoroutines();
        textDisplay.text = "";
        titleDisplay.text = "";
        dialogBox.SetActive(false);
        currentState = DialogState.unactive;
        if(dialogIsFinished)
            dialogIsFinished.RaiseSignal();
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            switch (stateImage.color.a.ToString())
            {
                case "0":
                    stateImage.color = new Color(stateImage.color.r, stateImage.color.g, stateImage.color.b, 1);
                    //Play sound
                    yield return new WaitForSeconds(0.3f);
                    break;
                case "1":
                    stateImage.color = new Color(stateImage.color.r, stateImage.color.g, stateImage.color.b, 0);
                    //Play sound
                    yield return new WaitForSeconds(0.3f);
                    break;
            }
        }
    }


}
