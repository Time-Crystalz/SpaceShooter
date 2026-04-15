using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI killsText;

    private void Start()
    {
        pointsText.text = "Points: " + GameManager.points;
        killsText.text = "Kills: " + GameManager.killCount;
    }
}