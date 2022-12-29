using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, MyInputActions.IPlayerActions
{

    [field: SerializeField] public Transform groundcheck { get; private set; }
    [field: SerializeField] public Transform projectileSpawnPoint { get; private set; }
    [field: SerializeField] public GameObject projectile { get; private set; }
    [field: SerializeField] public GameObject shopCanvas { get; private set; }
    [field: SerializeField] public LayerMask whatIsGround { get; private set; }
    [SerializeField] public TreasureChest treasureChest = null;
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float shootCooldownTime = 1f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] Animator myAnimator;
    Health health;
    LevelLoader levelLoader;
    Vector2 moveInput;
    public float groundcheckradius = 0.3f;
    bool _move = false;
    bool doubleJump = false;
    bool canShoot = true;



    public MyInputActions inputAction { get; private set; }

    private void Awake()
    {
        health = FindObjectOfType<Health>();
        levelLoader = FindObjectOfType<LevelLoader>();
        health.SubscribeToBroker();

    }
    private void OnEnable()
    {
        inputAction = new MyInputActions();
        inputAction.Player.SetCallbacks(this);
        inputAction.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = levelLoader.playerPosition;
        Debug.Log(PlayerPrefsControl.FeatureUnlocked("X2"));
        Debug.Log(PlayerPrefsControl.FeatureUnlocked("Shoot"));
    }


    // Update is called once per frame
    void Update()
    {
        Move();
       // if (PlayerPrefsControl.FeatureUnlocked("X2"))
        {
            UpdateJumpState();
        }

    }

    private void UpdateJumpState()
    {
        if (IsGrounded())
        {
            doubleJump = true;
        }
    }

    void Move()
    {
        {
            // Debug.Log("Moving left or right: " + moveInput.x);
            myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        }
    }
    public bool IsFlipped()
    {
        return transform.localScale.x > 0;
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, whatIsGround);
    }
    private bool CanShoot()
    {
        return (canShoot);
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        bool playerHasHorizontalSpeed = Math.Abs(myRigidbody.velocity.x) > 0;
        myAnimator.SetBool("moving", playerHasHorizontalSpeed);

        {
            myRigidbody.velocity = playerVelocity;
        }
    }
    private void FlipSprite()
    {

        bool playerHasHorizontalSpeed = Math.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
            
        }

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        switch (moveInput.x)
        {
            case 1:
            case -1:
                _move = true;
                myAnimator.SetBool("moving", _move);
                myRigidbody.transform.localScale = new Vector3(moveInput.x, 1, 1);

                break;
            case 0:
                _move = false;
                myAnimator.SetBool("moving", _move);

                break;
            default:
                _move = false;
                myAnimator.SetBool("moving", _move);
                break;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && context.performed)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (!IsGrounded() && context.performed)
        {
            if (PlayerPrefsControl.FeatureUnlocked("X2") && doubleJump)
            {
                myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (PlayerPrefsControl.FeatureUnlocked("Shoot") && canShoot)
        {
            Debug.Log("shooting");
            var myProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            myProjectile.GetComponent<Projectile>().SetDirectionAndShoot(IsFlipped());
            canShoot = false;
            StartCoroutine(ShootCooldown());

        }
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldownTime);
        canShoot = true;
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (treasureChest)
        {
            treasureChest.Open();
        }
    }
    public void OnShop(InputAction.CallbackContext context)
    {
        shopCanvas = FindObjectOfType<ShopCanvas>(true).gameObject;
        Time.timeScale = 0;
        shopCanvas.SetActive(true);
    }


}
