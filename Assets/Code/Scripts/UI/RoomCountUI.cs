using TMPro;
using UnityEngine;

namespace UI 
{
    [RequireComponent(typeof(TMP_Text))]
    public class RoomCountUI : MonoBehaviour
    {
        // --------------- Bookkeeping ---------------
        private int _roomCount;
        private TMP_Text _roomCountText;

        private void Start()
        {
            // Expensive, but this will only be called at the start
            FindObjectOfType<RoomManager>().OnRoomCountUpdate.AddListener(OnRoomCountUpdate);

            _roomCountText = GetComponent<TMP_Text>();
        }

        public int GetRoomCount()
        {
            return _roomCount;
        }

        private void OnRoomCountUpdate(int count)
        {
            _roomCountText.text = count + "";
            _roomCount = count;
        }
    }
}