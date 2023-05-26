using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
  [SerializeField] private List<Room> _roomPrefabs;
  [SerializeField] private Room _startRoom;
  [SerializeField] private Room _startNextRoom;

  private Room _nextRoomPrefab;

  // There are always at three rooms in the scene
  private Room _previousRoom;
  private Room _currentRoom;  // 1st
  private Room _nextRoom;     // 2nd
  private Room _nextNextRoom; // 3rd

  private Transform _transform;

  private void Awake() {
    this._transform = this.transform;
  }

  private void Start() {
    this._currentRoom = this._startRoom;
    this._nextRoom = this._startNextRoom;
    this.InstantiateNextNextRoom();
  }

  private void InstantiateNextNextRoom() {
    this.RandomSelectNextPrefab();

    Vector3 position = this._nextRoom.RightEdge;
    position.x += this._nextRoomPrefab.Width / 2f;
    this._nextNextRoom = Instantiate(this._nextRoomPrefab, position, Quaternion.identity, this._transform);
    this._nextNextRoom.OnRoomEntered.AddListener(this.OnEnteredNextRoom);
  }

  private void RandomSelectNextPrefab() {
    int index = Random.Range(0, this._roomPrefabs.Count);
    this._nextRoomPrefab = this._roomPrefabs[index];
  }

  private void OnEnteredNextRoom() {
    this._nextNextRoom.Show();
    this._currentRoom.Hide();
    this.DestroyPreviousRoom();

    this._previousRoom = this._currentRoom;
    this._currentRoom = this._nextRoom;
    this._nextRoom = this._nextNextRoom;

    this.InstantiateNextNextRoom();
  }

  private void DestroyPreviousRoom() {
    if (this._previousRoom == null)
      return;

    this._previousRoom.DetachFrontWall();
    Destroy(this._previousRoom.gameObject);
  }
}
