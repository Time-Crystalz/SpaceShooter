using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI pointsText;

    

    private void Update()
    {
        killText.text = "Kills: " + GameManager.killCount.ToString();
        pointsText.text = "Points " + GameManager.points.ToString();
    }
}
