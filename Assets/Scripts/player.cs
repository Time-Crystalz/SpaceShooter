
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{

    private Rigidbody2D playerBody;
    private float forceValue = 400f;
    private float xBoundary;
    private float yBoundary;
    private bool moveUp;
    private bool rotateLeft;
    private bool rotateRight;
    [SerializeField] private Camera cam;
    public static player Instance { get; private set; }


    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        Instance = this;
        if (cam == null)
        {
         cam = Camera.main;   
        }
    }


    private void Start()
    {

        playerBody.gravityScale = 0f;
        playerBody.linearDamping = 0.9f;
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight   = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        xBoundary = topRight.x;   
        yBoundary = topRight.y;   
    }
    private void FixedUpdate()
    {



        bool wkey = Keyboard.current.wKey.isPressed || moveUp;
        bool akey = Keyboard.current.aKey.isPressed || rotateLeft;
        bool dkey = Keyboard.current.dKey.isPressed || rotateRight;
        

        if (wkey)
        {

            playerBody.AddForce(forceValue * transform.up * Time.deltaTime);
        }

        if (dkey)
        {
            playerBody.AddTorque(-200 * Time.deltaTime);
            playerBody.AddForce(forceValue/2 * transform.up * Time.deltaTime);

        }

        if (akey)
        {
            playerBody.AddTorque(200 * Time.deltaTime);
            playerBody.AddForce(forceValue/2 * transform.up * Time.deltaTime);
        }

        

        PlayerOffSCreenFix();


    }

    public void SetMoveUp(bool value) { moveUp = value; }
    public void SetRotateLeft(bool value) { rotateLeft = value; }
    public void SetRotateRight(bool value) { rotateRight = value; }

    private void PlayerOffSCreenFix()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x < -xBoundary)
        {

            transform.position = new Vector2(xBoundary,y);
        }
        if (x > xBoundary)
        {

            transform.position = new Vector2(-xBoundary, y);
        }
        if (y > yBoundary)
        {

            transform.position = new Vector2(x, -yBoundary);
        }
        if (y < -yBoundary)
        {

            transform.position = new Vector2(x, yBoundary);
        }

    }


}
