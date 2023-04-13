using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    [SerializeField] private Image filler;

    public void UpdateLifebar(int health, int maxHealth)
    {
        filler.fillAmount = (float)health / maxHealth;
    }
}
