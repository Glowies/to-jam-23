using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField] private List<Room> _roomPrefabs;
  [SerializeField] private Room _startRoom;

  private Room _nextRoomPrefab;

  private Room _previousRoom;
  private Room _currentRoom;
  private Room _nextRoom;

  private Transform _transform;

  private void Awake() {
    this._transform = this.transform;
  }

  private void Start() {
    this._currentRoom = this._startRoom;
    this.InstantiateNextRoom();
  }

  private void InstantiateNextRoom() {
    this.RandomSelectNextPrefab();

    Vector3 position = this._currentRoom.RightEdge;
    position.x += this._nextRoomPrefab.Width / 2f;
    this._nextRoom = Instantiate(this._nextRoomPrefab, position, Quaternion.identity, this.transform);
    this._nextRoom.OnRoomEntered.AddListener(this.OnEnteredNextRoom);
  }

  private void RandomSelectNextPrefab() {
    int index = Random.Range(0, this._roomPrefabs.Count);
    this._nextRoomPrefab = this._roomPrefabs[index];
  }

  private void OnEnteredNextRoom() {
    this._nextRoom.Show();
    this._currentRoom.Hide();
    this.DestroyPreviousRoom();

    this._previousRoom = this._currentRoom;
    this._currentRoom = this._nextRoom;

    this.InstantiateNextRoom();
  }

  private void DestroyPreviousRoom() {
    if (this._previousRoom == null)
      return;

    Destroy(this._previousRoom.gameObject);
  }
}
