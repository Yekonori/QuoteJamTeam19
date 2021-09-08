using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Timer : MonoBehaviour
{
    public Text t_Timer;
    [Range(0.1f,1.5f)]
    public float timerSpeed; //1 = normal 

    [SerializeField]
    private int minuts;
    [SerializeField]
    private int seconds;


    [SerializeField]
    private Vector3 shakeAxis;
    [SerializeField]
    private int shakeVibrato;
    [SerializeField]
    private int shakeRandomness;

    private bool canCount = true;

    private void Start()
    {
        ResetTimer(minuts, seconds);
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

        if(minuts == 1 && seconds == 0)
        {
            print("one minut left!");
            //sound effect
            t_Timer.DOColor(Color.red,2f);
            t_Timer.GetComponent<RectTransform>().DOShakePosition(60f, shakeAxis, shakeVibrato, shakeRandomness, false, false);
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
            
            yield return new WaitForSeconds(timerSpeed);
            canCount = true;
        }   
    }
}
