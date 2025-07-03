using UnityEngine;

public class DamagedVFX : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("DamagedVFX")
            .Set(GetComponent<ParticleSystem>())
            .Add<Damaged>();
    }
}