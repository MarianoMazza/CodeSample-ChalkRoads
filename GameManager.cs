using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ParentWhiteCircle selectedParent;
    int selectedWhiteCircles = 0;
    int goal = 6;
    int lives = 3;
    [SerializeField] BlackCirclesManager blackCirclesManager;
    [SerializeField] Animator[] darkness = new Animator[5];
    [SerializeField] SpriteRenderer selectedParentSprite;
    [SerializeField] ParentWhiteCircle[] parentWhiteCircles = new ParentWhiteCircle[6];

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    public void SelectParentWhiteCircle(ParentWhiteCircle _selectedParent)
    {
        selectedParent = _selectedParent;
        selectedParentSprite.GetComponent<SpriteRenderer>().sprite = _selectedParent.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < parentWhiteCircles.Length; i++)
        {
            if (parentWhiteCircles[i] != selectedParent)
            {
                parentWhiteCircles[i].gameObject.SetActive(false);
            }
            else
            {
                parentWhiteCircles[i].GoAway();
            }
        }
        SpawnAndAdvance();
        blackCirclesManager.Spawn();
    }

    void SpawnAndAdvance()
    {
        selectedParent.SpawnChild(selectedWhiteCircles);
        if (selectedWhiteCircles != darkness.Length)
            darkness[selectedWhiteCircles].SetTrigger("Appear");
        if (selectedWhiteCircles != 0)
        {
            darkness[selectedWhiteCircles - 1].SetTrigger("Disappear");
            darkness[selectedWhiteCircles - 1].SetTrigger("Stay");
        }
    }

    public void SelectWhiteCircle()
    {
        selectedWhiteCircles++;
        if (selectedWhiteCircles == goal)
        {
            Victory();
        }
        else
        {
            SpawnAndAdvance();
            blackCirclesManager.IncreaseDifficulty();
        }
    }

    public void DamagePlayer()
    {
        lives--;
        if (lives == 0)
        {
            Loss();
        }
        else if (selectedWhiteCircles != 0)
        {
            selectedWhiteCircles--;
            SpawnAndAdvance();
        }
    }

    private void Loss()
    {
        this.GetComponent<Animator>().SetTrigger("BadEnding");
        Invoke("ResetScene", 6);
    }

    void Victory()
    {
        this.GetComponent<Animator>().SetTrigger("GoodEnding");
        Invoke("ResetScene", 6);
    }

    void ResetScene()
    {
        SceneManager.LoadScene("MainInteraction");
    }
}
