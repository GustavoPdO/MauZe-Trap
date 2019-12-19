using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
    }
}
