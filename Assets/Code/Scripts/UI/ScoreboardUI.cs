using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour {
    public Color defaultColor, highlight;

    [SerializeField] public TMP_Text _one, _oneScore, _oneName;
    [SerializeField] public TMP_Text _two, _twoScore, _twoName;
    [SerializeField] public TMP_Text _three, _threeScore, _threeName;
    [SerializeField] public TMP_Text _four, _fourScore, _fourName;
    [SerializeField] public TMP_Text _five, _fiveScore, _fiveName;

    void OnEnable() {
        _one.color = defaultColor;
        _oneScore.color = defaultColor;
        _oneName.color = defaultColor;

        _oneScore.text = PlayerPrefs.GetFloat("highScore1") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms1");
        _oneName.text = PlayerPrefs.GetString("name1");

        _two.color = defaultColor;
        _twoScore.color = defaultColor;
        _twoName.color = defaultColor;

        _twoScore.text = PlayerPrefs.GetFloat("highScore2") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms2");
        _twoName.text = PlayerPrefs.GetString("name2");

        _three.color = defaultColor;
        _threeScore.color = defaultColor;
        _threeName.color = defaultColor;

        _threeScore.text = PlayerPrefs.GetFloat("highScore3") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms3");
        _threeName.text = PlayerPrefs.GetString("name3");

        _four.color = defaultColor;
        _fourScore.color = defaultColor;
        _fourName.color = defaultColor;

        _fourScore.text = PlayerPrefs.GetFloat("highScore4") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms4");
        _fourName.text = PlayerPrefs.GetString("name4");

        _five.color = defaultColor;
        _fiveScore.color = defaultColor;
        _fiveName.color = defaultColor;

        _fiveScore.text = PlayerPrefs.GetFloat("highScore5") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms5");
        _fiveName.text = PlayerPrefs.GetString("name5");

        switch (LeaderboardManager.Instance.GetPlacement()) {
            case 1:
                _one.color = highlight;
                _oneScore.color = highlight;
                _oneName.color = highlight;
                break;
            case 2:
                _two.color = highlight;
                _twoScore.color = highlight;
                _twoName.color = highlight;
                break;
            case 3:
                _three.color = highlight;
                _threeScore.color = highlight;
                _threeName.color = highlight;
                break;
            case 4:
                _four.color = highlight;
                _fourScore.color = highlight;
                _fourName.color = highlight;
                break;
            case 5:
                _five.color = highlight;
                _fiveScore.color = highlight;
                _fiveName.color = highlight;
                break;
        }
    }
}

