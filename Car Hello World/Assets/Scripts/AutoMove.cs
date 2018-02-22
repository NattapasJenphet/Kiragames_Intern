using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{

    public float speedValue;
    public float rayDistance;
    private Vector3 currentDistance;

    public float distLeft;
    public float distRight;

    public bool moveLeft;
    public bool moveRight;
    public bool movable;

    public int laneStauts = 0;
    public float[] positionLane = { -3.15f, -1f, 1.2f, 3.2f };
    public int RandomLane;

    void Start()
    {
        movable = true;
        speedValue = 4;
        rayDistance = 10;
        checkCurrentPosition();
        RandomLane = Random.Range(-1, 1);
    }

    void Update()
    {
        destorySelf();
        speedControl(speedValue);
        CheckDrawLineLeft(transform.right);
        CheckDrawLineRight(transform.right * -1f);
        ObstacleObject(transform.position, transform.right);
        checkPlayerPosition();

        if (gameObject.transform.GetChild(1).GetComponent<CarRaycast>().Action == true)
        {
            BehaviorsOfCar();
        }
    }

    void speedControl(float speedInput)
    {
        speedInput = speedValue;
        //transform.Translate(new Vector3(0, 0, 1f * speedInput * Time.deltaTime));
        gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speedInput * Time.deltaTime);
    }

    void CheckDrawLineLeft(Vector3 pos)
    {
        currentDistance = pos;
        DrawLine(transform.position, currentDistance, rayDistance, Color.green);
    }

    void CheckDrawLineRight(Vector3 pos)
    {
        currentDistance = pos;
        DrawLine(transform.position, currentDistance, rayDistance, Color.blue);
    }

    void ObstacleObject(Vector3 pos, Vector3 dir)
    {
        Ray leftRay = new Ray(pos, dir);
        RaycastHit leftHit;
        Ray rightRay = new Ray(pos, dir * -1f);
        RaycastHit rightHit;

        if (Physics.Raycast(rightRay, out leftHit, rayDistance))
        {
            if (leftHit.collider.gameObject.tag == "Fence")
            {
                distLeft = Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(leftHit.collider.gameObject.transform.position.x, 0, 0));
            }
            if (leftHit.collider.gameObject.tag == "Blocker")
            {
                movable = false;
            }
        }

        if (Physics.Raycast(leftRay, out rightHit, rayDistance))
        {
            if (rightHit.collider.gameObject.tag == "Fence")
            {
                distRight = Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(rightHit.collider.gameObject.transform.position.x, 0, 0));
            }
            if (rightHit.collider.gameObject.tag == "Blocker")
            {
                movable = false;
            }
        }

        if (distLeft > distRight)
        {
            if (distRight < 1.6)
            {
                moveLeft = true;
            }
        }
        else if (distLeft < distRight)
        {
            if (distLeft < 1.6)
            {
                moveRight = true;
            }
        }
    }

    void checkCurrentPosition()
    {
        for (int i = 0; i < positionLane.Length; i++)
        {
            if (transform.position.x - positionLane[i] <= 0.2f && transform.position.x - positionLane[i] >= -0.2f)
            {
                laneStauts = i;
            }
        }
    }

    void BehaviorsOfCar()
    {
        if (laneStauts == 0 || laneStauts == 3)
        {
            if (movable == true)
            {
                if (RandomLane >= 0)
                {
                    if (moveRight == true)
                    {
                        GetComponent<Animator>().SetInteger("Animation", 1);
                        transform.position = Vector3.Lerp(transform.position, new Vector3(positionLane[laneStauts + 1], transform.position.y, transform.position.z + 4f), 1.5f * Time.deltaTime);
                    }
                    else if (moveLeft == true)
                    {
                        GetComponent<Animator>().SetInteger("Animation", -1);
                        transform.position = Vector3.Lerp(transform.position, new Vector3(positionLane[laneStauts - 1], transform.position.y, transform.position.z + 4f), 1.5f * Time.deltaTime);
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(positionLane[laneStauts], transform.position.y, transform.position.z + 4f), 1.5f * Time.deltaTime);
                }
            }
        }

        if (laneStauts == 1 || laneStauts == 2)
        {
            if (movable == true)
            {
                GetComponent<Animator>().SetInteger("Animation", RandomLane);
                transform.position = Vector3.Lerp(transform.position, new Vector3(positionLane[laneStauts + RandomLane], transform.position.y, transform.position.z + 4f), 1.5f * Time.deltaTime);
            }
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Turn left"))
        {
            GetComponent<Animator>().SetInteger("Animation", 0);
            GetComponent<Animator>().SetBool("Play", false);
            Invoke("deleyAnimation", 1);
        }
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Turn right"))
        {
            GetComponent<Animator>().SetInteger("Animation", 0);
            GetComponent<Animator>().SetBool("Play", false);
            Invoke("deleyAnimation", 1);
        }
    }

    void DrawLine(Vector3 pos, Vector3 dir, float lenght, Color color)
    {
        Debug.DrawLine(pos, pos + (dir * lenght), color);
    }

    void destorySelf()
    {
        if (transform.position.y < -19f && transform.position.y > -20f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == ("Blocker"))
        {
            if (transform.position.y > collision.collider.gameObject.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 5f);
            }
        }
    }

    void checkPlayerPosition()
    {
        GameObject carPlayer = GameObject.FindGameObjectWithTag("Player");
        Vector3 pos = transform.position;
        Vector3 pos_player = carPlayer.transform.position;
        float Distance = Vector3.Distance(pos, pos_player);

        if (Distance < 1.5f)
        {
            carPlayer.GetComponent<CoreGameController>().actionTimeCount += 0.01f;
            carPlayer.GetComponent<CoreGameController>().bonusScore += 10;
        }
    }

    void deleyAnimation()
    {
        movable = false;
    }
}
