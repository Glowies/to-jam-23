using TMPro;
using UnityEngine;

namespace UI 
{
    [RequireComponent(typeof(TMP_Text))]
    public class RoomCountUI : MonoBehaviour
    {
        private TMP_Text _roomCountText;

        private void Start()
        {
            ScoreManager.Instance.OnScoreUpdate.AddListener(OnRoomCountUpdate);

            _roomCountText = GetComponent<TMP_Text>();
            
            // Set initial score
            var score = ScoreManager.Instance.GetScore();
            _roomCountText.text = score.Item2 + "";
        }

        private void OnRoomCountUpdate(float distance, int count)
        {
            _roomCountText.text = count + "";
        }
    }
}