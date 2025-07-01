using TMPro;
using UnityEngine;

public class TextHealth : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("TextHealth")
            .Set(GetComponent<TextMeshProUGUI>());
    }
}