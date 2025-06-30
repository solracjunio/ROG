using UnityEngine;

public class FireRate : MonoBehaviour
{
    public Player Player;
    public Weapon Weapon;
    public float SpeedMultiplier = 1.25f;

    public void ApplySpeed()
    {
        if (Player != null)
        {
            Weapon.FireRate *= SpeedMultiplier;
            Player.Experience = 0;
        }
    }
}