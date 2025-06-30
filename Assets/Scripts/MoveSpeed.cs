using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    public Player Player;
    public float SpeedMultiplier = 1.25f;

    public void ApplySpeed()
    {
        if (Player != null)
        {
            Player.Speed *= SpeedMultiplier;
            Player.Experience = 0;
        }
    }
}