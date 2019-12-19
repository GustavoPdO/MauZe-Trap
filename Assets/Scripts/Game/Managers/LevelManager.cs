using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager levelManager;

    public static LevelManager Instance
    {
        get
        {
            if(levelManager == null)
            {
                levelManager = FindObjectOfType<LevelManager>();
                if(levelManager == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(LevelManager).Name;
                    levelManager = go.AddComponent<LevelManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return levelManager;
        }
    }

    private void Awake() {
        if(levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    
    [Header("Rat character info")]
    [SerializeField]
    private Vector3 ratPosition;

    private GameObject ratObject;
    private RatMovement ratMovement;
    private Transform ratTransform;

    [Header("Cheese character info")]
    [SerializeField]
    private Vector3 cheesePosition;

    private GameObject cheeseObject;
    private CheeseMovement cheeseMovement;
    private Transform cheeseTransform;

    public float getCheeseEulerAngle() {
        return cheeseTransform.localEulerAngles.y;
    }

    private void Start() {
        ratObject = GameObject.FindGameObjectWithTag("Rat");
        ratMovement = ratObject.GetComponent<RatMovement>();
        ratMovement.setLevelManager(this);
        ratTransform = ratObject.transform;

        cheeseObject = GameObject.FindGameObjectWithTag("Player");
        cheeseMovement = cheeseObject.GetComponent<CheeseMovement>();
        cheeseMovement.setLevelManager(this);
        cheeseTransform = cheeseObject.transform;
    }

    private void FixedUpdate() {
        ratPosition = ratTransform.position;
        cheesePosition = cheeseTransform.position;
    }

    public void checkLineOfSight() {
        
    }

    public void attractRat() {
        ratMovement.setCheeseChasing();
    }

    public void leaveRat() {
        ratMovement.setCheeseLeaving();
    }

}
