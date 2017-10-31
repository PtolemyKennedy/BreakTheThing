//Tayler-James Martin 2017
//Calculates and records points scored in a level
//Implementation for Score capping based on a flag
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Local points recording for a level
/// </summary>
public class PointsSystem : MonoBehaviour
{

    /// <summary>
    /// Total points scored this session
    /// </summary>
    private int Points;

    /// <summary>
    /// Base Value of breaking a joint
    /// </summary>
    public int JointBaseValue;

    /// <summary>
    /// Whether or not too many explosives were used, if over cap, score is capped
    /// </summary>
    public bool IsOverExplosivesCap;

    /// <summary>
    /// Maximum Score that can be reached when over explosives cap
    /// </summary>
    public int ScoreCap;

    /// <summary>
    /// Score needed to win the level
    /// </summary>
    public int TargetScore;

    public bool IsWin = false;

    public Text pointsText;

    /// <summary>
    /// sets the maximum score available should someone use more than the maximum amount of explosives
    /// </summary>
    /// <param name="P_scoreCap">Score Cap desired for a level</param>
    public void SetScoreCap(int P_scoreCap)
    {
        ScoreCap = P_scoreCap;
    }

    /// <summary>
    /// Adds or Subtracts the score based on the base value of joints with specific multiplier
    /// Will not allow score to go over score cap if over the explosives used cap
    /// </summary>
    /// <param name="JointMultiplyer">Multiplier value of joint broken, set to negative to reduce score</param>
    public void ScorePoints(float P_JointMultiplier)
    {
        int ScoreValue;

        ScoreValue = Mathf.RoundToInt(JointBaseValue * P_JointMultiplier);

        //tests to see if explosives cap is reached, caps score if so
        //otherwise adds score as normal
        if (IsOverExplosivesCap && (Points + ScoreValue >= ScoreCap))
        {
            Points = ScoreCap;
        }
        else
        {
            Points += ScoreValue;
        }
        if (Points >= TargetScore)
        {
            //YouWin();
        }

        pointsText.text = "Points: " + Points;
    }

    //private void YouWin()
    //{
    //    IsWin = true;
    //    //temporary
    //    GameObject.Find("Game Win").GetComponent<UnityEngine.UI.Text>().text = "You Win!";
    //}

    /// <summary>
    /// Adds base value to points
    /// </summary>
    public void ScorePoints()
    {
        ScorePoints(1);       
    }

    /// <summary>
    /// Gets current score as integer
    /// </summary>
    /// <returns>Current score</returns>
    public int GetPoints()
    {
        return Points;
    }

}