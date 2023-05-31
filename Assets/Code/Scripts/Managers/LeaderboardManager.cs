using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeaderboardManager : Singleton<LeaderboardManager> 
{
    public UnityEvent OnLeaderboardConfirmed;
    
    [SerializeField] private TMP_Text _initialOneText, _initialTwoText, _initialThreeText;
    private int _leaderboardPlacement;
    
    // If there are no high scores entered yet, hard code some scores in
    protected override void Awake()
    {
        base.Awake();
        OnLeaderboardConfirmed = new UnityEvent();
        
        // Not the most secure way of recording a leaderboard, but will do for now
        // Note: The playerprefs file can be altered to fix the scores...
        if (!PlayerPrefs.HasKey("highScore1")) {
            PlayerPrefs.SetFloat("highScore1", 99);
            PlayerPrefs.SetInt("highScoreRooms1", 7);
            PlayerPrefs.SetString("name1","BMF");
            
            PlayerPrefs.SetFloat("highScore2", 85);
            PlayerPrefs.SetInt("highScoreRooms2", 6);
            PlayerPrefs.SetString("name2","LES");
            
            PlayerPrefs.SetFloat("highScore3", 64);
            PlayerPrefs.SetInt("highScoreRooms3", 4);
            PlayerPrefs.SetString("name3","KMD");
            
            PlayerPrefs.SetFloat("highScore4", 23);
            PlayerPrefs.SetInt("highScoreRooms4", 2);
            PlayerPrefs.SetString("name4","BOI");
            
            PlayerPrefs.SetFloat("highScore5", 16);
            PlayerPrefs.SetInt("highScoreRooms5", 1);
            PlayerPrefs.SetString("name5","CAT");

            PlayerPrefs.Save();
        }
    }

    public int GetPlacement()
    {
        return _leaderboardPlacement;
    }
    
    // Returns the placement in the leaderboard if the player placed (1-5) or -1 otherwise
    public int CalculatePlacement()
    {
        var score = ScoreManager.Instance.GetScore();
        float distance = score.Item1;
        int roomsTraversed = score.Item2;
        
        int placement = -1;
        
        // Compare the current distance to the existing leaderboard
        if (distance >= PlayerPrefs.GetFloat("highScore1")) {
            placement = 1;

            // Shift the rest of placements down
            PlayerPrefs.SetFloat("highScore5", PlayerPrefs.GetFloat("highScore4"));
            PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4"));
            
            PlayerPrefs.SetFloat("highScore4", PlayerPrefs.GetFloat("highScore3"));
            PlayerPrefs.SetString("name4", PlayerPrefs.GetString("name3"));
            
            PlayerPrefs.SetFloat("highScore3", PlayerPrefs.GetFloat("highScore2"));
            PlayerPrefs.SetString("name3", PlayerPrefs.GetString("name2"));
            
            PlayerPrefs.SetFloat("highScore2", PlayerPrefs.GetFloat("highScore1"));
            PlayerPrefs.SetString("name2", PlayerPrefs.GetString("name1"));
            
            PlayerPrefs.SetFloat("highScore1", distance);
            PlayerPrefs.SetInt("highScoreRooms1", roomsTraversed);

            PlayerPrefs.Save();
        } else if (distance >= PlayerPrefs.GetFloat("highScore2")) {
            placement = 2;

            PlayerPrefs.SetFloat("highScore5", PlayerPrefs.GetFloat("highScore4"));
            PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4"));
            
            PlayerPrefs.SetFloat("highScore4", PlayerPrefs.GetFloat("highScore3"));
            PlayerPrefs.SetString("name4", PlayerPrefs.GetString("name3"));
            
            PlayerPrefs.SetFloat("highScore3", PlayerPrefs.GetFloat("highScore2"));
            PlayerPrefs.SetString("name3", PlayerPrefs.GetString("name2"));
            
            PlayerPrefs.SetFloat("highScore2", distance);
            PlayerPrefs.SetInt("highScoreRooms2", roomsTraversed);

            PlayerPrefs.Save();
        } else if (distance >= PlayerPrefs.GetFloat("highScore3")) {
            placement = 3;

            PlayerPrefs.SetFloat("highScore5", PlayerPrefs.GetFloat("highScore4"));
            PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4"));
            
            PlayerPrefs.SetFloat("highScore4", PlayerPrefs.GetFloat("highScore3"));
            PlayerPrefs.SetString("name4", PlayerPrefs.GetString("name3"));
            
            PlayerPrefs.SetFloat("highScore3", distance);
            PlayerPrefs.SetInt("highScoreRooms3", roomsTraversed);

            PlayerPrefs.Save();
        } else if (distance > PlayerPrefs.GetFloat("highScore4")) {
            placement = 4;

            PlayerPrefs.SetFloat("highScore5", PlayerPrefs.GetFloat("highScore4"));
            PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4"));
            
            PlayerPrefs.SetFloat("highScore4", distance);
            PlayerPrefs.SetInt("highScoreRooms4", roomsTraversed);

            PlayerPrefs.Save();
        } else if (distance > PlayerPrefs.GetFloat("highScore5")) {
            placement = 5;

            PlayerPrefs.SetFloat("highScore5", distance);
            PlayerPrefs.SetInt("highScoreRooms5", roomsTraversed);

            PlayerPrefs.Save();
        }

        _leaderboardPlacement = placement;
        return placement;
    }

    public void ConfirmLeaderboardEntry()
    {
        // Save the name data to the leaderboard
        PlayerPrefs.SetString("name" + _leaderboardPlacement, _initialOneText.text + _initialTwoText.text + _initialThreeText.text);
        OnLeaderboardConfirmed?.Invoke();
    }
}