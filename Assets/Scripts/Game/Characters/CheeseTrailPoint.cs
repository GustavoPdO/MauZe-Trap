using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseTrailPoint : MonoBehaviour
{
    [Header("First Direction")]
    [SerializeField]
    private Transform firstRepositioning;
    [SerializeField]
    private int firstYAxis = 0;

    [Header("Second Direction")]
    [SerializeField]
    private Transform secondRepositioning;
    [SerializeField]
    private int secondYAxis = 0;

    [Space(5)]
    [SerializeField]
    private Animator timeControl;

    private void Start() {
        timeControl = GetComponent(typeof(Animator)) as Animator;
    }

    void OnTriggerEnter(Collider other) {

        if(other.transform.localEulerAngles.y == firstYAxis)
        {
            other.transform.localEulerAngles = new Vector3(0, secondYAxis, 0);
            other.transform.position = new Vector3(secondRepositioning.position.x, other.transform.position.y, secondRepositioning.position.z);
            other.attachedRigidbody.velocity = new Vector3(0, 0, 0);
            timeControl.SetTrigger("Collision");
        }
        else
        {
            other.transform.localEulerAngles = new Vector3(0, firstYAxis, 0);
            other.transform.position = new Vector3(firstRepositioning.position.x, other.transform.position.y, firstRepositioning.position.z);
            other.attachedRigidbody.velocity = new Vector3(0, 0, 0);
            timeControl.SetTrigger("Collision");
        }

    }

}
