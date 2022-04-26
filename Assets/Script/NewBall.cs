using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBall : MonoBehaviour
{

    public float speed = 30;

    public bool bonusGoal;
    public bool isLasthit;
    public string warnaBall;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.instance.PadSfx();
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        // Hit the left Racket?
        if (col.gameObject.name == "Racket Red")
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            isLasthit = true;
        }

        // Hit the right Racket?
        if (col.gameObject.name == "Racket Blue")
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            isLasthit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager.instance.BallBounceSfx();
        if (other.gameObject.tag == "Goal1")
        {
            SoundManager.instance.GoalSfx();
            GameManager.Instance.player2Score++;
            if (bonusGoal)
            {
                GameManager.Instance.player2Score++;
            }
            GameManager.Instance.SpawnBall();
            Destroy(gameObject);
            if (GameManager.Instance.goldenGoal)
            {
                GameManager.Instance.GameOver();
            }
        }

        if (other.gameObject.tag == "Goal2")
        {
            SoundManager.instance.GoalSfx();
            GameManager.Instance.player1Score++;
            if (bonusGoal)
            {
                GameManager.Instance.player1Score++;
            }
            GameManager.Instance.SpawnBall();
            Destroy(gameObject);
            if (GameManager.Instance.goldenGoal)
            {
                GameManager.Instance.GameOver();
            }
        }

    }
}
