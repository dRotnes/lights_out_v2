using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public Player playerStats;

    private void Start()
    {
        InitHearts();
    }
    public void InitHearts()
    {
        foreach(Image image in hearts)
        {
            image.gameObject.SetActive(false);
        }
        for (int i = 0; i < playerStats.numberOfHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite =fullHeart;
        }
    }

    public void UpdateHearts()
    {
        foreach (Image image in hearts)
        {
            image.gameObject.SetActive(false);
        }
        float tempHealth = playerStats.currentHealth/ 2;

        for (int i = 0; i < playerStats.numberOfHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
            if (i <= tempHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = halfHeart;
            }
        }
    }

    public void AddHeart()
    {
        playerStats.numberOfHearts += 1;
        InitHearts();
    }
}
