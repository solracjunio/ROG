using UnityEngine;

public class HealedVFX : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("HealedVFX")
            .Set(GetComponent<ParticleSystem>())
            .Add<Healed>();
    }
}
