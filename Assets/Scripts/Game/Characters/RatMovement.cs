using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float scoutTime = 2;

    private GameObject rat;
    [SerializeField]
    private Rigidbody ratRB;
    [SerializeField]
    private Collider ratCollider;
    [SerializeField]
    private Transform scoutTransform;

    [SerializeField]
    private bool isChasingCheese = false;
    [SerializeField]
    private bool allowMoving = true;

    [SerializeField]
    private Enums.Direction direction;
    [SerializeField]
    private Enums.Direction lastDirection;

    public GameObject tileScout;
    [SerializeField]
    private float distanceToCheese;
    
    private LevelManager levelManager;

    public void setLevelManager(LevelManager level){
        levelManager = level;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        ratRB.velocity = transform.forward * (-moveSpeed * Convert.ToInt32(allowMoving));
    }

    void OnTriggerEnter(Collider other) {
        if(tileScout){
            tileScout.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(Vector3.Distance(transform.position, other.transform.position));
        if(!isChasingCheese && (Vector3.Distance(transform.position, other.transform.position) <= 0.1))
        {
            allowMoving = true;
            tileScout = other.gameObject;
            other.gameObject.SetActive(false);
            scout();
            changeDirection();
        }
        if(isChasingCheese && (Vector3.Distance(transform.position, other.transform.position) <= 0.1))
        {   
            followCheese();
            changeDirection();
        }
    
    }

    private void scout()
    {
        Debug.DrawLine(scoutTransform.position,
            new Vector3(scoutTransform.position.x, scoutTransform.position.y, scoutTransform.position.z + 1),
            Color.red,
            2);

        if (!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x, scoutTransform.position.y, scoutTransform.position.z + 1),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Trapdoor")
            | 1 << LayerMask.NameToLayer("Glass"))
        && lastDirection != Enums.Direction.North)
        {
            direction = Enums.Direction.North;
            return;
        }

        Debug.DrawLine(scoutTransform.position,
        new Vector3(scoutTransform.position.x + 1, scoutTransform.position.y, scoutTransform.position.z),
        Color.red,
        2);

        if (!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x + 1, scoutTransform.position.y, scoutTransform.position.z),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Trapdoor")
            | 1 << LayerMask.NameToLayer("Glass"))
        && lastDirection != Enums.Direction.East)
        {
            direction = Enums.Direction.East;
            return;
        }

        Debug.DrawLine(scoutTransform.position,
        new Vector3(scoutTransform.position.x, scoutTransform.position.y, scoutTransform.position.z - 1),
        Color.red,
        2);

        if (!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x, scoutTransform.position.y, scoutTransform.position.z - 1),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Trapdoor")
            | 1 << LayerMask.NameToLayer("Glass"))
        && lastDirection != Enums.Direction.South)
        {
            direction = Enums.Direction.South;
            return;
        }

        Debug.DrawLine(scoutTransform.position,
        new Vector3(scoutTransform.position.x - 1, scoutTransform.position.y, scoutTransform.position.z),
        Color.red,
        2);

        if (!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x - 1, scoutTransform.position.y, scoutTransform.position.z),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Trapdoor")
            | 1 << LayerMask.NameToLayer("Glass"))
            && lastDirection != Enums.Direction.West)
        {
            direction = Enums.Direction.West;
            return;
        }

        direction = lastDirection;
        return;                                
    }

    private void followCheese() {
        float newRotation = levelManager.getCheeseEulerAngle() - 180;

        if(newRotation == -180 || newRotation == 180)
        {
            if(!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x, scoutTransform.position.y, scoutTransform.position.z + 1f),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Glass")))
            {
                Debug.Log(1);
                direction = Enums.Direction.North;
                return;
            }
            else
            {
                allowMoving = false;
                return;
            }
            
        }
        if(newRotation == -90 || newRotation == 270)
        {
            if(!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x + .75f, scoutTransform.position.y, scoutTransform.position.z),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Glass")))
            {
                Debug.Log(2);
                direction = Enums.Direction.East;
                return;
            }
            else
            {
                allowMoving = false;
                return;
            }
            
        }
        if(newRotation == 0 || newRotation == 360)
        {
            if(!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x, scoutTransform.position.y, scoutTransform.position.z - .75f),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Glass")))
            {
                Debug.Log(3);
                direction = Enums.Direction.South;
                return;
            }
            else
            {
                allowMoving = false;
                return;
            }
            
        }
        if(newRotation == -270 || newRotation == 90)
        {
            if(!Physics.Linecast(
            scoutTransform.position,
            new Vector3(scoutTransform.position.x - .75f, scoutTransform.position.y, scoutTransform.position.z),
            1 << LayerMask.NameToLayer("Concrete") | 1 << LayerMask.NameToLayer("Glass")))
            {
                Debug.Log(4);
                direction = Enums.Direction.West;
                return;
            }
            else
            {
                allowMoving = false;
                return;
            }
        }
    }

    private void changeDirection() {
        switch (direction)
        {
            case Enums.Direction.North:
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Vector3.up);
                lastDirection = Enums.Direction.South;
                break;
            case Enums.Direction.East:
                transform.LookAt(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Vector3.up);
                lastDirection = Enums.Direction.West;
                break;
            case Enums.Direction.South:
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Vector3.up);
                lastDirection = Enums.Direction.North;
                break;
            case Enums.Direction.West:
                transform.LookAt(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Vector3.up);
                lastDirection = Enums.Direction.East;
                break;
        }
    }

    public void setCheeseChasing()
    {
        isChasingCheese = true;
    }

    public void setCheeseLeaving()
    {
        isChasingCheese = false;
    }
}
