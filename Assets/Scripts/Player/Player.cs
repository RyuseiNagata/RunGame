using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] ShakeCamera shake = null;
    [SerializeField] GameObject goalEffect = null;
    [SerializeField] GameObject deathEffect = null;

    Rigidbody2D rb2D = null;

    bool isJump = false;            // ジャンプしているか
    float jumpPower = 0;            // ジャンプ力
    float speed = 275.0f;           // Playerの移動速度
    int gravityChange = 0;          // 重力の変更
    float invincibleTimer = 0.0f;   // 無敵の時間


    public int GravityChange
    {
        get { return gravityChange; }
    }
    bool isDeath = false;   // Playerが死んでるか判定
    public bool IsDeath
    {
        get { return isDeath; }
    }

    bool isGoal = false;    // ゴールしたか判定

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTimer <= 1.0f)
        {
            invincibleTimer += Time.deltaTime;
            return;
        }

        Goal();

        if (isGoal == true || isDeath == true) return; 
        if (rb2D.velocity.x < 3)
        {
            Destroy();
        }

        GravitySwitch();
        Jump();
    }


    void FixedUpdate()
    {
        if (isGoal == true || isDeath == true) return;
        rb2D.velocity = new Vector2(speed * Time.deltaTime, rb2D.velocity.y);
    }

    void Jump()
    {
#if UNITY_EDITOR
        if (isJump == true) return;
        if (Input.GetKey(KeyCode.Space))
        {
            GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Jump);
            rb2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
#elif UNITY_STANDALONE_WIN
        if (isJump == true) return;
        if (Input.GetKey(KeyCode.Space))
        {
            GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Jump);
            rb2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }

#else
        if (isJump == true) return;
        Touch touch = Input.GetTouch(0);

        if (Input.mousePosition.x <= Screen.width / 2)
        {
            // 左側をタップしたら
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Jump);
                rb2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }
#endif
    }

    void GravitySwitch()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.GravitySwitch);
            gravityChange++;
            if (gravityChange % 2 == 0)
            {
                rb2D.gravityScale = 1000.0f;
            }
            else if (gravityChange % 2 == 1)
            {
                rb2D.gravityScale = -1000.0f;
            }
        }
#elif UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.GravitySwitch);
            gravityChange++;
            if (gravityChange % 2 == 0)
            {
                rb2D.gravityScale = 1000.0f;
            }
            else if (gravityChange % 2 == 1)
            {
                rb2D.gravityScale = -1000.0f;
            }
        }

#else
        Touch touch = Input.GetTouch(0);

        if (Input.mousePosition.x >= Screen.width / 2)
        {
            // 右側をタップしたら
            if (touch.phase == TouchPhase.Began)
            {
                GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.GravitySwitch);
                gravityChange++;
                if (gravityChange % 2 == 0)
                {
                    rb2D.gravityScale = 1000.0f;
                }
                else if (gravityChange % 2 == 1)
                {
                    rb2D.gravityScale = -1000.0f;
                }
            }
        }
#endif
    }


    void Goal()
    {
        if (isGoal == false) return;
        if (isJump == true) return;
        rb2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        
        isJump = true;
    }

    void Destroy()
    {
        if(isDeath == false) 
        {
            GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Death);
            rb2D.velocity = Vector3.zero;
            shake.BeginShake(0.25f, 0.1f);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GameObject.Find("GameController")?.GetComponent<GameController>()?.FailureGame();
            isDeath = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            rb2D.gravityScale = 5.0f;
            jumpPower = 800.0f * Time.deltaTime;
            isJump = false;
        }
        else if(other.gameObject.tag == "Ceiling")
        {
            rb2D.gravityScale = -5.0f;
            jumpPower = -800.0f * Time.deltaTime;
            isJump = false;
        }

        if(other.gameObject.tag == "TheWall")
        {
            Destroy();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Instantiate(goalEffect, new Vector3(transform.position.x + 6.5f, 0.0f, 0.0f), Quaternion.identity);
            rb2D.velocity = Vector3.zero;
            GameObject.Find("GameController")?.GetComponent<GameController>()?.ClearGame();
            GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Clear);

            isGoal = true;
        }
    }
}
