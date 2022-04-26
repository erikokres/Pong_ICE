using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string namePowerUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (namePowerUp == "BonusGoal")
            {
                Debug.Log("BonusGoal");
                other.GetComponent<NewBall>().bonusGoal = true;
            }

            if (namePowerUp == "SpeedUp")
            {
                Debug.Log("SpeedUp");
                NewBall ball = other.GetComponent<NewBall>();
                ball.speed *= 3f;
            }

            if (namePowerUp == "ChangeDirection")
            {
                Debug.Log("ChangeDirection");
                NewBall ball = other.GetComponent<NewBall>();
                if (ball.isLasthit)
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, Random.Range(-1, 1)) * ball.speed;
                }
                else
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(1, Random.Range(-1, 1)) * ball.speed;
                }
            }

            Destroy(gameObject);
        }
    }
}
