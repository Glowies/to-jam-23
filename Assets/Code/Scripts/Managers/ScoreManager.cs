using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    public UnityEvent<float, int> OnScoreUpdate;
    
    private float _totalDistance;
    
    // TODO: Do we want the room count to start at 0 (number of rooms crossed) or 1 (number of rooms you've been to)?
    private int _roomsTraversed = 1;
    
    protected override void Awake() {
        base.Awake();
        OnScoreUpdate = new UnityEvent<float, int>();
    }
    
    public void SetDistance(float distance)
    {
        _totalDistance = distance;
        OnScoreUpdate?.Invoke(distance, _roomsTraversed);
    }

    public void UpdateRoomCount()
    {
        _roomsTraversed++;
        OnScoreUpdate?.Invoke(_totalDistance, _roomsTraversed);
    }
    
    /// <summary>
    /// Getter method to return the score in the form of a Tuple(float, int)
    /// First element: The totalDistance the player has traveled.
    /// Second element: The total number of rooms the player has accessed.
    /// </summary>
    public (float, int) GetScore()
    {
        return (_totalDistance, _roomsTraversed);
    }
}
