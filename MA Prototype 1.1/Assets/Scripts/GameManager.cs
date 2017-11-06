using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        //set the timescale to 1 to ensure no wierd stuff happens with time
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	//void Update ()
 //   {
		
	//}


    /// <summary>
    /// resets the scene
    /// </summary>
    public void SceneReset()
    {       
        SceneManager.LoadScene("prototype 'splosion");
    }

    /// <summary>
    /// starts the slow motion coroutine
    /// </summary>
    public void SlowMo()
    {
        StartCoroutine(SlowMo(0.3f, 0.15f, 2f));
    }


    /// <summary>
    /// slows down and speeds up time
    /// </summary>
    /// <param name="scale">speed at which time will flow, 0: stop time, 1: normal time</param> 
    /// <param name="time1">time before slowing down time, uses realtime seconds</param>
    /// <param name="time2">time between slowing down and resuming time, uses realtime seconds</param>
    IEnumerator SlowMo(float scale, float time1, float time2)
    {
        yield return new WaitForSecondsRealtime(time1);
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSecondsRealtime(time2);
        Time.timeScale = 1;
    }
}

