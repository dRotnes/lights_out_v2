using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public BoolValue load;
    private void Start()
    {
        if (load.value)
        {
            load.value = false;
            SavingManager savemanager = FindObjectOfType<SavingManager>();
            if (savemanager!=null)
            {

                savemanager.LoadGame();
            }
        }

    }

    public void NextScene()
    {
        StartCoroutine(NextSceneCO());
    }
    private IEnumerator NextSceneCO()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
