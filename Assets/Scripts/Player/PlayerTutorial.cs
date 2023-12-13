using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTutorial : MonoBehaviour
{
    Rigidbody2D rb2D = null;

    bool isJump = false;    // ジャンプしているか
    float jumpPower = 0;    // ジャンプ力
    int gravityChange = 0;  // 重力の変更

    public int GravityChange
    {
        get { return gravityChange; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        GravitySwitch();
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
#endif
    }


    public void AndroidJump()
    {
        if (isJump == true) return;
        GameSoundManager.Instance.PlaySE(GameSoundManager.SEType.Jump);

        rb2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        isJump = true;
    }

    public void AndroidGravitySwitch()
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


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            rb2D.gravityScale = 5.0f;
            jumpPower = 800.0f * Time.deltaTime;
            isJump = false;
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            rb2D.gravityScale = -5.0f;
            jumpPower = -800.0f * Time.deltaTime;
            isJump = false;
        }
    }
}
