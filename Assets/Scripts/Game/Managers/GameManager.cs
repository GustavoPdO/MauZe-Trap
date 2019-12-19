using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    public static GameManager Instance
    {
        get
        {
            if(gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
                if(gameManager == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameManager).Name;
                    gameManager = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return gameManager;
        }
    }

    private void Awake() {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
