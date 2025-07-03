using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("GameOver")
        .Set(gameObject);

        gameObject.SetActive(false);
    }
}