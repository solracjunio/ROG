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
    public Spawner Spawner;
    public SpawnerHealthPickup SpawnerHealthPickup;
    public Weapon Weapon;
    public float Experience;
    public float MaxExperience = 100f;
    public GameObject AbilityPanel;

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
        var Inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        transform.position += Speed * Time.deltaTime * Inputs;

        var diff = CurrentHealth - _tempHealth;
        if (diff < 0)
        {
            DamageEffect.Play();
            StartCoroutine(ActivateVignette(DamageVignette));
            if (CurrentHealth <= 0)
            {
                GameOver.SetActive(true);
                Speed = 0f;
                Spawner.enabled = false;
                Weapon.enabled = false;
                SpawnerHealthPickup.enabled = false;
            }
        }
        else if (diff > 0)
        {
            HealEffect.Play();
            StartCoroutine(ActivateVignette(HealVignette));
        }
        _tempHealth = CurrentHealth;

        if (IsClamped)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, ClampRange.x, ClampRange.y);
        }

        if (Experience >= MaxExperience)
        {
            AbilityPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            AbilityPanel.SetActive(false);
            Time.timeScale = 1f;
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
