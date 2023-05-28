using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text _finalScoreText;
    
    public void SetScore(float distance, int roomCount)
    {
        _finalScoreText.text = "You crossed " + distance + "m and " + roomCount + " rooms";
    }
}
