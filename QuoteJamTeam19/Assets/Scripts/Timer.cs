using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text t_Timer;
    public float timerSpeed; //1 = normal 
    
    private int seconds;
    private int minuts;
    private bool canCount = true;

    private void Start()
    {
        ResetTimer(0,30);
    }

    private void Update()
    {
        if (canCount)
            StartCoroutine(Count());
    }

    void StartTimer()
    {
        canCount = true;
    }
    void StopTimer()
    {
        canCount = false;
    }

    void ResetTimer(int _minuts, int _seconds)
    {
        seconds = _seconds;
        minuts = _minuts;
    }

    public IEnumerator Count()
    {
        canCount = false;
        seconds--;
        if(seconds < 0)
        {
            seconds = 59;
            minuts--;
        }

        if (minuts == 0 && seconds == 0)
        {
            StopTimer();
            GameManager.Instance.Lose();
            t_Timer.text = minuts.ToString() + ":" + "0" + seconds.ToString();
        }
        else
        {
            if (seconds <= 9)
            {
                t_Timer.text = minuts.ToString() + ":" + "0"+seconds.ToString();
            }
            else
            {
                t_Timer.text = minuts.ToString() + ":" + seconds.ToString();
            }
            
            yield return new WaitForSeconds(1.0f);
            canCount = true;
        }

        
    }


}
