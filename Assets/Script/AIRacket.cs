using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacket : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("NPC Setting")]
    [SerializeField] float speed; //mengatur kecepatan AI
    [SerializeField] float delayTime; //nilai untuk mengatur delay dari pergerakan AI


    bool isMoveAI; //untuk melihat apakah raket bergerak atau tidak
    float randomPos; //nilai random batas antara -1 dan 1
    bool isSingleTake; //untuk double validasi
    bool isUp; //untuk mengetahui apakah raket sedang di atas atau di bawah

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.instance.isSinglePlayer)
        {
            if (!isMoveAI && !isSingleTake)
            {
                Debug.Log("Kepanggil");

                StartCoroutine(DelayAIMove());
                isSingleTake = true;
            }

            if (isMoveAI)
            {
                MoveAI();
            }
        }
    }

    IEnumerator DelayAIMove()
    {
        yield return new WaitForSeconds(delayTime); //waktu delay function dijalankan
        randomPos = Random.Range(-1f, 1f); //nilai random antara -1 dan 1

        if (transform.position.y < randomPos)
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }

        isSingleTake = false;
        isMoveAI = true;
    }

    void MoveAI()
    {
        if (!isUp)
        {
            rb.velocity = new Vector2(0, -1f) * speed;
            if (transform.position.y <= randomPos)
            {
                rb.velocity = Vector2.zero;
                isMoveAI = false;
            }
        }

        if (isUp)
        {
            rb.velocity = new Vector2(0, 1f) * speed;
            if (transform.position.y >= randomPos)
            {
                rb.velocity = Vector2.zero;
                isMoveAI = false;
            }
        }
    }
}
