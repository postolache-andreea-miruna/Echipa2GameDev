using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Animator _animator;
    [SerializeField]
    private LayerMask jumpGround;
    private float directionX = 0f;
    [SerializeField]
    private float moveSpeed = 14f;
    [SerializeField]
    private float jumpHigh = 7f;
    private int strawberry;
    private enum PlayerMove {initial, run, jump, fall}

    //neww
    [SerializeField]
    private GameObject[] limits;
    private int limit1 = 0;
    private int limit2 = 1;
    //new
    PlayerPosSaved playerPosData;
    PlayerPosSaved playerPosData2;
    private void Awake()
    {
        playerPosData = FindObjectOfType<PlayerPosSaved>();
        playerPosData.PlayerPositionLoad();
    }

    private void Start()
    {
        playerPosData2 = FindObjectOfType<PlayerPosSaved>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        _rigidBody.velocity = new Vector2(directionX * moveSpeed, _rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
            //edit -> projectSettings->input manager
            //jump = space
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpHigh);
        }

        UpdateAnimation();
        
    }
    private void UpdateAnimation()
    {
        
        PlayerMove _move;

            if (directionX < 0f) //la stanga
            {
                _spriteRenderer.flipX = true;

                _move = PlayerMove.run;
            }
            else if (directionX > 0f) //dreapta
            {
                _spriteRenderer.flipX = false;

                _move = PlayerMove.run;
            }
            else
            {
                _move = PlayerMove.initial;
            }

            if (_rigidBody.velocity.y < -.1f)
            {
                _move = PlayerMove.fall;
            }
            else if (_rigidBody.velocity.y > .1f)
            {
                _move = PlayerMove.jump;
            }
            
            _animator.SetInteger("_move", (int)_move); //transform enum into int
    }

    private bool IsGrounded() // jump just when we are on the terrain not in air
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("MazeTransitionTag"))
        {

            playerPosData2.PlayerPositionSave();
            MazeGameBeh.Instance.sceneToMoveTo();
        }

        else if (collision.gameObject.CompareTag("BridgeTransitionTag"))
        {
            playerPosData2.PlayerPositionSave();
            BridgeGameBeh.Instance.sceneToMoveTo();
            
        }

        else if (collision.gameObject.CompareTag("FishTransitionTag"))
        {
            playerPosData2.PlayerPositionSave();
            FishGameBeh.Instance.sceneToMoveTo();
        }

        else if (collision.gameObject.CompareTag("RiverTransitionTag"))
        {
            playerPosData2.PlayerPositionSave();
            RiverGameBeh.Instance.sceneToMoveTo();
        }
        else if (collision.gameObject.CompareTag("MitiTransitionTag"))
        {
            strawberry = PlayerPrefs.GetInt("Coins", 0);
            if (strawberry <= 14)
                SceneManager.LoadScene("GameOverScene");
            else if (strawberry > 14 && strawberry <= 29)
                SceneManager.LoadScene("KingReunitedScene");
            else if (strawberry > 29 && strawberry <= 49)
                SceneManager.LoadScene("BonusScene");
            else if(strawberry > 49)
                SceneManager.LoadScene("VideoScene");
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            playerPosData.ResetPlayerPosition(283, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Saw"))
        {
            playerPosData.ResetPlayerPosition(64, 5);
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            _rigidBody.velocity = Vector3.zero;    
        }
    }
}
