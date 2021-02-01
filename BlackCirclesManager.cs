using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCirclesManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] BlackCircle[] blackCircles = new BlackCircle[6];
    int activeBlackCircles = 1;

    void Start()
    {
        AssignSpeedToBlackCircles(1, 0.5f);
    }

    private void AssignSpeedToBlackCircles(float speed, float increase)
    {
        for (int i = 0; i < blackCircles.Length; i++)
        {
            blackCircles[i].movementSpeed = speed;
            speed += increase;
        }
    }

    public void IncreaseDifficulty()
    {
        if (activeBlackCircles < blackCircles.Length)
        {
            activeBlackCircles++;
        }
        for (int i = 0; i < activeBlackCircles - 1; i++)
        {
            blackCircles[i].Grow();
        }
        Spawn();
    }

    public void DecreaseDifficulty()
    {
        if (activeBlackCircles != 1)
        {
            activeBlackCircles--;
            blackCircles[activeBlackCircles].gameObject.SetActive(false);
            for (int i = 0; i < activeBlackCircles; i++)
            {
                blackCircles[i].Shrink();
            }
            Spawn();
        }
        gameManager.DamagePlayer();
    }

    public void Spawn()
    {
        blackCircles[activeBlackCircles - 1].SpawnIfInactive();
    }
}
