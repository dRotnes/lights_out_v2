using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAmeOver : MonoBehaviour
{
    public void End()
    {
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
