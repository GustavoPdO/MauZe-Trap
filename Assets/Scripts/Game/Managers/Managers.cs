using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        LevelManager levelManager = LevelManager.Instance;
    }
}
