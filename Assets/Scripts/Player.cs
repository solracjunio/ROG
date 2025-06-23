using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public float CurrentHealth;
    public float MaxHealth = 100f;
    private float _tempHealth;
    public bool IsClamped;
    public Vector2 ClampRange;
    public ParticleSystem DamageEffect;
    public ParticleSystem HealEffect;
    public GameObject DamageVignette;
    public GameObject HealVignette;
    public GameObject GameOver;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _tempHealth = CurrentHealth;
        if (IsClamped)
        {
            ClampRange = new Vector2(0f, MaxHealth);
        }
    }

    private void Update()
    {
        var Inputs = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.position += Speed * Time.deltaTime * Inputs;

        var diff = CurrentHealth - _tempHealth;
        if (diff < 0)
        {
            print("Damaged");
            DamageEffect.Play();
            StartCoroutine(ActivateVignette(DamageVignette));
            if (CurrentHealth <= 0)
            {
                print("Player is dead");
                GameOver.SetActive(true);
            }
        }
        else if (diff > 0)
        {
            print("Healed");
            HealEffect.Play();
            StartCoroutine(ActivateVignette(HealVignette));
        }
        _tempHealth = CurrentHealth;

        if (IsClamped)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, ClampRange.x, ClampRange.y);
        }
    }

    public void AddHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }

    public IEnumerator ActivateVignette(GameObject vignette)
    {
        vignette.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        vignette.SetActive(false);
    }
}
