using UnityEngine;
using UnityEngine.UI;

public class HealedVignetteVFX : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("HealedVignette")
            .Set(GetComponent<Image>())
            .Add<Healed>();
    }
}
