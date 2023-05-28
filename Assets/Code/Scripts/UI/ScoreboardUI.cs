using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour {
    public Color defaultColor, highlight;

    public TMP_Text one, oneScore, oneName;
    public TMP_Text two, twoScore, twoName;
    public TMP_Text three, threeScore, threeName;
    public TMP_Text four, fourScore, fourName;
    public TMP_Text five, fiveScore, fiveName;

    void OnEnable() {
        one.color = defaultColor;
        oneScore.color = defaultColor;
        oneName.color = defaultColor;

        oneScore.text = PlayerPrefs.GetFloat("highScore1") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms1");
        oneName.text = PlayerPrefs.GetString("name1");

        two.color = defaultColor;
        twoScore.color = defaultColor;
        twoName.color = defaultColor;

        twoScore.text = PlayerPrefs.GetFloat("highScore2") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms2");
        twoName.text = PlayerPrefs.GetString("name2");

        three.color = defaultColor;
        threeScore.color = defaultColor;
        threeName.color = defaultColor;

        threeScore.text = PlayerPrefs.GetFloat("highScore3") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms3");
        threeName.text = PlayerPrefs.GetString("name3");

        four.color = defaultColor;
        fourScore.color = defaultColor;
        fourName.color = defaultColor;

        fourScore.text = PlayerPrefs.GetFloat("highScore4") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms4");
        fourName.text = PlayerPrefs.GetString("name4");

        five.color = defaultColor;
        fiveScore.color = defaultColor;
        fiveName.color = defaultColor;

        fiveScore.text = PlayerPrefs.GetFloat("highScore5") + "m, Room " + PlayerPrefs.GetInt("highScoreRooms5");
        fiveName.text = PlayerPrefs.GetString("name5");

        switch (LeaderboardManager.Instance.GetPlacement()) {
            case 1:
                one.color = highlight;
                oneScore.color = highlight;
                oneName.color = highlight;
                break;
            case 2:
                two.color = highlight;
                twoScore.color = highlight;
                twoName.color = highlight;
                break;
            case 3:
                three.color = highlight;
                threeScore.color = highlight;
                threeName.color = highlight;
                break;
            case 4:
                four.color = highlight;
                fourScore.color = highlight;
                fourName.color = highlight;
                break;
            case 5:
                five.color = highlight;
                fiveScore.color = highlight;
                fiveName.color = highlight;
                break;
        }
    }
}

