using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void NextScene()
    {
        StartCoroutine(NextSceneCO());
    }
    private IEnumerator NextSceneCO()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OneScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
