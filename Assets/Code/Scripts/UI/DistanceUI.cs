using TMPro;
using UnityEngine;

namespace UI 
{
    [RequireComponent(typeof(TMP_Text))]
    public class DistanceUI : MonoBehaviour
    {
        private TMP_Text _scoreText;

        private void Start()
        {
            ScoreManager.Instance.OnScoreUpdate.AddListener(OnScoreUpdate);

            _scoreText = GetComponent<TMP_Text>();
            
            // Set initial score
            var score = ScoreManager.Instance.GetScore();
            _scoreText.text = score.Item1 + "m";
        }

        private void OnScoreUpdate(float distance, int roomsTraversed)
        {
            // TODO: If we want a cool counter for later, this should be updated
            _scoreText.text = distance + "m";
        }
    }
}