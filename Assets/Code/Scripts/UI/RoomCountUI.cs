using TMPro;
using UnityEngine;

namespace UI 
{
    [RequireComponent(typeof(TMP_Text))]
    public class RoomCountUI : MonoBehaviour
    {
        // --------------- Bookkeeping ---------------
        private TMP_Text _roomCountText;

        private void Start()
        {
            // Expensive, but this will only be called at the start
            FindObjectOfType<RoomManager>().OnRoomCountUpdate.AddListener(OnRoomCountUpdate);

            _roomCountText = GetComponent<TMP_Text>();
        }

        private void OnRoomCountUpdate(int count)
        {
            _roomCountText.text = count + "";
        }
    }
}