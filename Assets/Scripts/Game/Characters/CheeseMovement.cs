using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.1f;
    [SerializeField]
    private float rotationSpeed = 2;
    [SerializeField]
    private int sightDistance = 0;

    [SerializeField]
    private Transform cheeseTransform;
    [SerializeField]
    private Rigidbody cheeseRB;

    //[SerializeField]
    //private LevelManager levelManager;

    public void setLevelManager(LevelManager level){
        //levelManager = level;
    }

    private void Start() {
        cheeseRB =  GetComponent(typeof(Rigidbody)) as Rigidbody;
        cheeseTransform = cheeseRB.GetComponentInChildren<MeshRenderer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        cheeseTransform.Rotate(Vector3.forward, -rotationSpeed);
        cheeseRB.velocity = (transform.right * moveSpeed * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime);
        //levelManager.leaveRat();

        Debug.DrawRay(transform.position, -transform.forward * 11, Color.yellow, 1);

        RaycastHit wallHit;        
        if(!Physics.Raycast(transform.position, -transform.forward, out wallHit, 
            sightDistance, 1 << LayerMask.NameToLayer("Concrete")))
        {
            wallHit.distance = sightDistance;
        }

        if(!Physics.Raycast(transform.position, -transform.forward, wallHit.distance-0.1f,
            1 << LayerMask.NameToLayer("Concrete")))
        {
            RaycastHit ratHit;
            if(Physics.Raycast(transform.position, -transform.forward, out ratHit, wallHit.distance-0.1f,
                1 << LayerMask.NameToLayer("Cheese")))
            {
                //levelManager.attractRat();
            }         
        }
    }
}
