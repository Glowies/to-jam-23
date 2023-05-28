using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour {
  [SerializeField] private List<Room> _roomPrefabs;
  [SerializeField] public Room _startRoom;
  [SerializeField] public Room _startNextRoom;

  private Room _nextRoomPrefab;

  // There are always at three rooms in the scene
  [Header("Current Room Info")]
  [SerializeField] private Room _previousRoom;
  [SerializeField] private Room _currentRoom;  // 1st
  [SerializeField] private Room _nextRoom;     // 2nd
  [SerializeField] private Room _nextNextRoom; // 3rd

  private Transform _transform;
  private readonly WaitForSeconds _waitForTenSeconds = new WaitForSeconds(10f);

  public UnityEvent<int> OnRoomCountUpdate = new UnityEvent<int>();
  private int _roomCount = 1;

  private void Awake() {
    this._transform = this.transform;
  }

  private void Start() {
    this._currentRoom = this._startRoom;
    this._nextRoom = this._startNextRoom;
    this._nextRoom.OnRoomEntered.AddListener(this.OnEnteredNextRoom);
    this.InstantiateNextNextRoom();
  }

  public Room GetCurrentRoom() {
    return _currentRoom;
  }

  public Room GetStartRoom()
  {
      return _startRoom;
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
    print("Next Room");
    this._nextRoom.Show();
    this._nextNextRoom.Show(0.95f);
    this._currentRoom.Hide();
    this.DestroyPreviousRoom();

    this._previousRoom = this._currentRoom;
    this._currentRoom = this._nextRoom;
    this._nextRoom = this._nextNextRoom;
    _roomCount++;
    OnRoomCountUpdate?.Invoke(_roomCount);

    this.InstantiateNextNextRoom();
  }

  private void DestroyPreviousRoom() {
    if (this._previousRoom == null)
      return;

    GameObject wall = this._previousRoom.DetachFrontWall();
    Destroy(this._previousRoom.gameObject);
    this.StartCoroutine(this.DestroyWallsLater(wall));
  }

  private IEnumerator DestroyWallsLater(GameObject target) {
    yield return this._waitForTenSeconds;
    Destroy(target);
  }
}
