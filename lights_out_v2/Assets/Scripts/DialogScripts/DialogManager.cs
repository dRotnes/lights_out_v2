using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum DialogState
{
    active,
    unactive,
    finished
}
public class DialogManager : MonoBehaviour
{
    public DialogState currentState;

    public TextMeshProUGUI titleDisplay;
    public TextMeshProUGUI textDisplay;
    public GameObject dialogBox;
    public float typingSpeed;
    public float timeBetweenPhrases;

    private Queue<string> sentenceQueue = new Queue<string>();
    private bool _isTyped;

    private void Awake()
    {
        currentState = DialogState.unactive;

    }
    public void StartDialog(Dialog dialog, bool type)
    {
        
        _isTyped = type;
        dialogBox.SetActive(true);
        titleDisplay.text = dialog.name;

        foreach (string sentence in dialog.sentences)
        {
            sentenceQueue.Enqueue(sentence);
        }

        if (_isTyped == true)
        {
            StartCoroutine(DisplayTyped());
        }

        currentState = DialogState.active;
        
        

    }
    
    private void Update()
    {
        
        if(currentState == DialogState.active)
        {
            if(sentenceQueue.Count == 0)
            {
                currentState = DialogState.finished;
            }
        }
        
        if (Input.GetKeyDown("space") && !_isTyped && currentState != DialogState.unactive)
        {
            DisplayNonTyped();
        }
    }

    private void DisplayNonTyped()
    {
        if(currentState == DialogState.finished)
        {
            EndDialog();
            return;
        }
        string sentence = sentenceQueue.Dequeue();
        textDisplay.text = sentence;

    }
    private IEnumerator DisplayTyped()
    {
        if (currentState == DialogState.finished)
        {
            EndDialog();
            yield break;
        }

        textDisplay.text = "";
        string sentence = sentenceQueue.Dequeue();
        foreach (char letter in sentence.ToCharArray())
        {
           
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);

        }
        yield return new WaitForSecondsRealtime(timeBetweenPhrases);
        StartCoroutine(DisplayTyped());
    



    }

    public void EndDialog()
    {
        sentenceQueue.Clear();
        textDisplay.text = "";
        titleDisplay.text = "";
        dialogBox.SetActive(false);
        currentState = DialogState.unactive;
    }

}
