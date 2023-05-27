using TMPro;
using UnityEngine;

namespace UI 
{
    [RequireComponent(typeof(TMP_Text))]
    public class DistanceUI : MonoBehaviour
    {
        // --------------- Bookkeeping ---------------
        private TMP_Text _scoreText;

        private void Start()
        {
            // Expensive, but this will only be called at the start
            FindObjectOfType<Controls.PlayerController>().OnScoreUpdate.AddListener(OnScoreUpdate);

            _scoreText = GetComponent<TMP_Text>();
        }

        private void OnScoreUpdate(float score)
        {
            // TODO: If we want a cool counter for later, this should be updated
            _scoreText.text = score + "m";
        }
    }
}