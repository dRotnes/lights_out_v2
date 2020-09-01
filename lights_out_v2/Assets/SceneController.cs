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
            FindObjectOfType<SavingManager>().LoadGame();
        }

    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
