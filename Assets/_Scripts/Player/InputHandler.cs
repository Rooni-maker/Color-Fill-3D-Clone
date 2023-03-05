using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float speed;
    public float levelChangeSpeed;
    public float distanceRequirement;
    public GameObject linePrefab;
    public GameObject blowUpEffect;
    private Vector2 dir;
    private Vector2 oldDir;
    private Vector2 clickPosition;
    private Vector2 savedPosition;
    private Grid currentGrid;
    private Grid oldGrid;
    private float Ypos;
    private float startXpos;
    private bool canTurn = true;
    private bool startGridFilled = false;
    private bool movingToNextLevel = false;
    private Vector3 levelChangeTargetPosition;
    private bool creatingLine = false;
    [HideInInspector]
    public GridManager gm;
    // Start is called before the first frame update
    void Start()
    {
        startXpos = transform.position.x;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (((Vector2)Input.mousePosition - clickPosition).magnitude > distanceRequirement)
            {
                Vector2 swipeDir = ((Vector2)Input.mousePosition - clickPosition).normalized;
                clickPosition = Input.mousePosition;
                if (Mathf.Abs(swipeDir.x) + Mathf.Abs(swipeDir.y) >= 1)
                {
                    if (Mathf.Abs(swipeDir.x) > Mathf.Abs(swipeDir.y))
                    {
                        swipeDir.y = 0;
                    }
                    else
                    {
                        swipeDir.x = 0;
                    }
                }
                // Dir modifications
                dir = new Vector2Int(Mathf.RoundToInt(swipeDir.x), Mathf.RoundToInt(swipeDir.y));
                if (!canTurn)
                {
                    dir = Vector2.zero;
                }

                if (dir == Vector2.zero)
                {
                    dir = oldDir;
                }



                if (dir != oldDir)
                {
                    transform.position = new Vector3(currentGrid.transform.position.x, Ypos, currentGrid.transform.position.z);
                }

                //bool canImoveTo = gm.CanIMoveTo((int)(currentGrid.point.x + dir.x), (int)(currentGrid.point.y + dir.y));
                //if (!canImoveTo)
                //{
                   HitWall();
                //}


                //if (oldGrid && creatingLine)
                //{
                //    if (oldGrid.point.x == currentGrid.point.x + dir.x && oldGrid.point.y == currentGrid.point.y + dir.y)
                //    {
                //        dir = oldDir;
                //    }
                //}

                oldDir = dir;


            }

        }
    }
    private void FixedUpdate()
    {
        if (!movingToNextLevel)
        {
            transform.position = new Vector3(transform.position.x + (dir.x * speed), transform.position.y, transform.position.z + (dir.y * speed));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, levelChangeTargetPosition, levelChangeSpeed);

            if (Vector3.Distance(transform.position, levelChangeTargetPosition) < 0.001)
            {
                levelChangeTargetPosition = new Vector3(startXpos, transform.position.y, transform.position.z + 100);
            }
        }
    }
    public void HitWall()
    {
        if (!movingToNextLevel)
        {
            dir = Vector2.zero;
            transform.position = new Vector3(currentGrid.transform.position.x, Ypos, currentGrid.transform.position.z);
            //gm.PlayerStopped();
        }
    }

}
