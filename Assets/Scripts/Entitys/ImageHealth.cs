using UnityEngine;

public class ImageHealth : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("ImageHealth")
            .Set(GetComponent<UnityEngine.UI.Image>());
    }
}