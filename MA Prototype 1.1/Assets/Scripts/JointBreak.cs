using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour
{
    PointsSystem ps;
    int strength;
	// Use this for initialization
	void Start ()
    {
        ps = GameObject.FindGameObjectWithTag("GameController").GetComponent<PointsSystem>();
        FixedJoint fj = GetComponent<FixedJoint>();

        if (fj != null)
        {
            strength = Mathf.RoundToInt(fj.breakForce);
        }        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnJointBreak(float breakForce)
    {
        ps.ScorePoints(strength);
    }
}
