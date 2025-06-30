using TMPro;
using UnityEngine;

public class BindExperienceText : MonoBehaviour
{
    public Player Player;
    public string Prefix;
    public int Decimal;
    public bool IsClamped;
    public Vector2 ClampRange;

    private void Update()
    {
        float experience;
        if (IsClamped)
        {
            experience = Mathf.Clamp(Player.Experience, ClampRange.x, ClampRange.y);
        }
        else
        {
            experience = Player.Experience;
        }

        var experienceString = experience.ToString("F" + Decimal);
        var text = $"{Prefix} {experienceString}";
        GetComponent<TextMeshProUGUI>().text = text;
    }
}
