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


    private SignalSend _dialogIsFinished;
    private Queue<string> _sentenceQueue;
    private bool _isTyped;
    private bool typing;
    private bool _breakCO;
    private string _currentString;


    private void Start()
    {
        _sentenceQueue = new Queue<string>();
        currentState = DialogState.unactive;
    }
    public void StartDialog(Dialog dialog)
    {
        _sentenceQueue.Clear();
        dialogBox.SetActive(true);
        titleDisplay.text = dialog.name;
        _isTyped = dialog.isTyped;
        _dialogIsFinished = dialog.finishedSignal;
        foreach (string sentence in dialog.sentences)
        {
            _sentenceQueue.Enqueue(sentence);
        }
        currentState = DialogState.active;
        DisplayNextSentence();
        
    }
    private void Update()
    {
        if (_isTyped)
        {

            if (Input.GetButtonDown("Fire2") && currentState == DialogState.active)
            {
                if (typing)
                {

                    _breakCO = true;
                    textDisplay.text = _currentString;
                }
                else if (canpass)
                {
                    StopAllCoroutines();
                    DisplayNextSentence();
                }
                else
                {
                    return;
                }
            }
        }
          
    }
    public void DisplayNextSentence()
    {
        canpass = false;
        if (_sentenceQueue.Count == 0)
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
            _currentString= _sentenceQueue.Dequeue();
            textDisplay.text = _currentString;
            stateImage.sprite = continueImage;
            canpass = true;
            if (_sentenceQueue.Count == 0)
            {
                stateImage.sprite = finishedImage;
            }
        }
        StartCoroutine(Blink());
    }
    
    private IEnumerator DisplayTyped()
    {
        _currentString = _sentenceQueue.Dequeue();
        textDisplay.text = "";
        stateImage.sprite = writingImage;
        typing = true;
        foreach (char letter in _currentString.ToCharArray())
        {
            if (_breakCO)
                break;
            textDisplay.text += letter;
            FindObjectOfType<AudioManager>().Play("TalkingSound");
            yield return new WaitForSeconds(typingSpeed);
        }
        _breakCO = false;
        typing = false;
        stateImage.sprite = continueImage;
        if (_sentenceQueue.Count == 0)
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
        if(_dialogIsFinished)
            _dialogIsFinished.RaiseSignal();
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
