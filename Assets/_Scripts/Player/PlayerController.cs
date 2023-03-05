using UnityEngine;
using System.Collections.Generic;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}//enum end

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [Space(10)]

    [SerializeField] bool isMoving  = false;
    [SerializeField] float moveSpeed = 10f;

    private Vector3 _moveVector = Vector3.zero;
    Direction swipeDirection = Direction.None;

    [SerializeField] private bool _useKeyboard = true;

    private Player _player = null;
    private const float DistanceThreshold = 1f;
    private const float maxSwipeTime      = 0.5f;
    private const float minSwipeDistance  = 0.10f;

    private Rigidbody rigidbody;
    private Vector3   startPos3 = Vector3.zero;
    private Vector3   targetPos = Vector3.zero;
    private Vector2   startPos2 = Vector2.zero;
    private float     startTime = 0f;

    //Properties
    public bool IsMoving
    {
        get => isMoving; 
        set
        {
            isMoving = value;
            if(!isMoving)
                _moveVector = rigidbody.velocity = Vector3.zero;
        }//set end
    }//IsMoving end

    public void Init()
    {
        rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
        _player.Init();
    }//Start() end

    public void Restart()
    {
        _player.SpawnCubes = isMoving = false;
        rigidbody.velocity = startPos3 = targetPos = _moveVector= Vector3.zero;
        startPos2 = Vector2.zero;
        startTime = 0f;
        this.enabled = true;
    }//Restart() end

    private void Update()
    {
        DetectInput();
        DecideMovement();
    }//Update() end

    private void FixedUpdate()
    {
        if(!isMoving)
            return;
        if(Vector3.Distance(startPos3, transform.position) >= DistanceThreshold)
        {
            transform.position = targetPos;
            startPos3 = transform.position;
            targetPos = transform.position + _moveVector;
            _player.SpawnCube();
        }//if end
        transform.position += (targetPos - startPos3) * moveSpeed * Time.fixedDeltaTime;
    }//FixedUpdate() end

    private void DetectInput()
    {
        swipeDirection = Direction.None;

        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                startPos2  = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width); // normalize position according to screen width.
                startTime = Time.time;
            }
            if(t.phase == TouchPhase.Ended)
            {
                if(Time.time - startTime > maxSwipeTime) 
                    return;

                Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                Vector2 swipe = new Vector2(endPos.x - startPos2.x, endPos.y - startPos2.y);

                if(swipe.magnitude < minSwipeDistance)
                    return;

                if(Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                { 
                    if(swipe.x > 0)
                        swipeDirection = Direction.Right;
                        // swipeRight = true;
                    else
                        swipeDirection = Direction.Left;
                        // swipeLeft = true;
                }//if end
                else
                { 
                    if(swipe.y > 0)
                        swipeDirection = Direction.Up;
                        // swipeUp = true;
                    else
                        swipeDirection = Direction.Down;
                        // swipeDown = true;
                }
            }
        }

        if(_useKeyboard)
        {
            if(Input.GetKeyDown(KeyCode.W))
                swipeDirection = Direction.Up;
            if(Input.GetKeyDown(KeyCode.S))
                swipeDirection = Direction.Down;
            if(Input.GetKeyDown(KeyCode.A))
                swipeDirection = Direction.Left;
            if(Input.GetKeyDown(KeyCode.D))
                swipeDirection = Direction.Right;
        }
    }

    private void DecideMovement()
    {
        switch(swipeDirection)
        {
            case Direction.None:
            return;
            case Direction.Up:
                if(_player.SpawnCubes && _moveVector.Equals(Vector3.back))
                    return;
                _moveVector = Vector3.forward;
            break;
            case Direction.Down:
                if(_player.SpawnCubes && _moveVector.Equals(Vector3.forward))
                    return;
                _moveVector = Vector3.back;
            break;
            case Direction.Left:
                if(_player.SpawnCubes && _moveVector.Equals(Vector3.right))
                    return;
                _moveVector = Vector3.left;
            break;
            case Direction.Right:
                if(_player.SpawnCubes && _moveVector.Equals(Vector3.left))
                    return;
                _moveVector = Vector3.right;
            break;
        }//switch end
        SetTargetPos();
    }//ApplyMovement() end

    private void SetTargetPos()
    {
        if(isMoving)
            return;
        _player.SpawnCubes = isMoving = true;
        targetPos  = transform.position + _moveVector;
        startPos3  = transform.position;
    }//SetTargetPos() end

}//class end