using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentWhiteCircle : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] whiteCircles = new GameObject[6];

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameManager.SelectParentWhiteCircle(this);
    }

    public void GoAway()
    {
        transform.position = new Vector2(100, 100);
    }

    public void SpawnChild(int childToSpawn)
    {
        whiteCircles[childToSpawn].SetActive(true);
        whiteCircles[childToSpawn].transform.position = new Vector3(Random.Range(-4, 3), Random.Range(-0.60f, 3.4f), -5);
    }
}
