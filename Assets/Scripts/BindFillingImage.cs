using UnityEngine;

public class BindFillingImage : MonoBehaviour
{
    public Player Player;

    private void Update()
    {
        GetComponent<UnityEngine.UI.Image>().fillAmount = Player.CurrentHealth / Player.MaxHealth;
    }
}
