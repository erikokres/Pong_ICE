using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    public bool isBounce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int random = Random.Range(0, 2);
        Debug.Log(random);
        if (random == 0)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 6 || transform.position.y < -6)
        {
            GameManager.Instance.SpawnBall();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Racket Red" && !isBounce)
        {
            Vector2 dir = new Vector2(1, 0).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
        }
        if (other.gameObject.tag == "Racket Blue" && !isBounce)
        {
            Vector2 dir = new Vector2(-1, 0).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Goal1")
    //     {
    //         GameManager.Instance.player2Score++;
    //         if (bonusGoal)
    //         {
    //             GameManager.Instance.player2Score++;
    //         }
    //         GameManager.Instance.SpawnBall();
    //         Destroy(gameObject);
    //     }

    //     if (other.gameObject.tag == "Goal2")
    //     {
    //         GameManager.Instance.player1Score++;
    //         if (bonusGoal)
    //         {
    //             GameManager.Instance.player1Score++;
    //         }
    //         GameManager.Instance.SpawnBall();
    //         Destroy(gameObject);
    //     }

    // }

    private IEnumerator DelayBounce()
    {
        isBounce = true;
        yield return new WaitForSeconds(1f);
        isBounce = false;
    }
}
