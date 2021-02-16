using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Sprite[] birdSprite;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject gameController;

    SpriteRenderer spriteRenderer;
    bool motionControl = true;
    int birdCounter = 0;
    float birdMotionSpeed;
    Rigidbody2D physics;
    
    int scoreCounter;
    bool gameOver = true;
    GameController gameControl;
    AudioSource []sound;
    CircleCollider2D birdCollider;

    int highScore = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        gameControl = gameController.GetComponent<GameController>();
        sound = GetComponents<AudioSource>();
        birdCollider = GetComponent<CircleCollider2D>();
        highScore = PlayerPrefs.GetInt("scoreMemory");
        Debug.Log(highScore);
    }

    void Update()
    {
        //Tıklandığında yukarı kuvvet uygulanır
        if (Input.GetMouseButtonDown(0) && gameOver)
        {
            physics.velocity = new Vector2(0, 0); //Hızı önce sıfırlıyoruz
            physics.AddForce(new Vector2(0, 200)); //Sonra kuvvet uygulyoruz
            sound[0].Play();
        }

        //yukarı çıkarken yukarı bakış
        if(physics.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        //aşağı düşerken aşağı bakış
        else if(physics.velocity.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }

        //Kanat çırpmak için animasyon
        Animation();
    }

    void Animation()
    {
        birdMotionSpeed += Time.deltaTime;
        if (birdMotionSpeed > 0.2f)
        {
            birdMotionSpeed = 0;

            if (motionControl)
            {
                spriteRenderer.sprite = birdSprite[birdCounter];
                birdCounter++;
                if (birdCounter == birdSprite.Length)
                {
                    birdCounter--;
                    motionControl = false;
                }
            }
            else
            {
                birdCounter--;
                spriteRenderer.sprite = birdSprite[birdCounter];
                if (birdCounter == 0)
                {
                    birdCounter++;
                    motionControl = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string colTag = col.gameObject.tag;
        if(colTag == "TagScore")
        {
            scoreCounter++;
            scoreText.text = scoreCounter + "";
            sound[1].Play();
        }
        if(colTag == "TagBarrier")
        {
            gameOver = false;
            sound[2].Play();
            gameControl.GameOver();
            birdCollider.enabled = false;

            if (highScore < scoreCounter)
            {
                highScore = scoreCounter;
                PlayerPrefs.SetInt("scoreMemory", highScore);
            }
            Invoke("goToMainMenu", 1.5f);
        }
    }

    void goToMainMenu()
    {
        PlayerPrefs.SetInt("currentScore", scoreCounter);
        SceneManager.LoadScene("MainMenu");       
    }
}
