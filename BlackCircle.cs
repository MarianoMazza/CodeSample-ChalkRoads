using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCircle : MonoBehaviour
{
    [SerializeField] BlackCirclesManager blackCirclesManager;
    [SerializeField] Transform playerTransform;
    public float movementSpeed { private get; set; }
    Rigidbody2D rigidBody;
    WaitForSeconds waitTime = new WaitForSeconds(5);
    new CircleCollider2D collider;
    Vector3 sizeModifier;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        sizeModifier = new Vector3(0.2f, 0.2f, 0.2f);
    }

    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        rigidBody.velocity = (playerTransform.position - transform.position).normalized * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            blackCirclesManager.DecreaseDifficulty();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        collider.enabled = false;
        yield return waitTime;
        collider.enabled = true;
    }

    public void SpawnIfInactive()
    {
        if (gameObject.activeSelf) return;
            gameObject.SetActive(true);
        if (Random.Range(1, 3) == 1)
        {
            transform.position = new Vector3(Random.Range(-8.6f, -6), Random.Range(-3.6f, 5.5f), -7);
        }
        else
        {
            transform.position = new Vector3(Random.Range(7.2f, 9), Random.Range(-3.6f, 5.5f), -7);
        }
    }

    public void Grow()
    {
        transform.localScale += sizeModifier;
    }

    public void Shrink()
    {
        if (transform.localScale.x >= 0.5)
            transform.localScale -= sizeModifier;
    }
}