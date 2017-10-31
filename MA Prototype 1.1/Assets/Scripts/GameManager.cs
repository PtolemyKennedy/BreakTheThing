using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SceneReset()
    {       
        SceneManager.LoadScene("prototype 'splosion");
    }

    public void SlowMo()
    {
        StartCoroutine(SlowMo(0.3f, 0.15f));
    }

   IEnumerator SlowMo(float scale, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSecondsRealtime(1.25f);
        Time.timeScale = 1;
    }
}

