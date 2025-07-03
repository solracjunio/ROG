using UnityEngine;
using UnityEngine.UI;

public class DamagedVignetteVFX : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("DamagedVignette")
            .Set(GetComponent<Image>())
            .Add<Damaged>();
    }
}