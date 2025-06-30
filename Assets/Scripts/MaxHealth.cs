using UnityEngine;

public class MaxHealth : MonoBehaviour
{
    public Player Player;
    public float Multiplier = 1.25f;

    public void ApplySpeed()
    {
        if (Player != null)
        {
            Player.MaxHealth *= Multiplier;
            Player.Experience = 0;
        }
    }
}