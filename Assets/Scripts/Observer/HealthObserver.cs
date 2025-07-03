using System.Collections;
using Flecs.NET.Core;
using UnityEngine;
using UnityEngine.UI;

public class HealthObserver : MonoBehaviour
{
    void Start()
    {
        GameWorld.Instance.MainWorld.Observer<HealthComponent>()
            .Event<HealedEvent>()
            .Each((Iter it, int i, ref HealthComponent _) =>
            {
                GameWorld.Instance.MainWorld.Query<ParticleSystem, Healed>()
                    .Each((Entity e, ref ParticleSystem ps) =>
                    {
                        ps.Play();
                    });

                GameWorld.Instance.MainWorld.Query<Image, Healed>()
                    .Each((Entity e, ref Image img) =>
                    {
                        StartCoroutine(ActivateVignette(img));
                    });
            });

        GameWorld.Instance.MainWorld.Observer<HealthComponent>()
            .Event<DamagedEvent>()
            .Each((Iter it, int i, ref HealthComponent _) =>
            {
                GameWorld.Instance.MainWorld.Query<ParticleSystem, Damaged>()
                    .Each((Entity e, ref ParticleSystem ps) =>
                    {
                        ps.Play();
                    });

                GameWorld.Instance.MainWorld.Query<Image, Damaged>()
                    .Each((Entity e, ref Image img) =>
                    {
                        StartCoroutine(ActivateVignette(img));
                    });
            });

        GameWorld.Instance.MainWorld.Observer<HealthComponent>()
            .Event<DeathEvent>()
            .Each((Iter it, int i, ref HealthComponent _) =>
            {
                GameWorld.Instance.MainWorld.Query<GameObject>()
                    .Each((Entity e, ref GameObject go) =>
                    {
                        go.SetActive(true);
                    });
            });
    }

    private IEnumerator ActivateVignette(Image img)
    {
        img.enabled = true;
        yield return new WaitForSeconds(0.5f);
        img.enabled = false;
    }
}

public struct HealedEvent { };
public struct DamagedEvent { };
public struct DeathEvent { };