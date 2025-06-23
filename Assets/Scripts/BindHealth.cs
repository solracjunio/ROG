using TMPro;
using UnityEngine;

public class BindHealth : MonoBehaviour
{
    public Player Player;
    public string Prefix;
    public int Decimal;
    public bool IsClamped;
    public Vector2 ClampRange;

    private void Update()
    {
        float health;
        if (IsClamped)
        {
            health = Mathf.Clamp(Player.CurrentHealth, ClampRange.x, ClampRange.y);
        }
        else
        {
            health = Player.CurrentHealth;
        }

        var healthString = health.ToString("F" + Decimal);
        var text = $"{Prefix} {healthString}";
        GetComponent<TextMeshProUGUI>().text = text;
    }
}
