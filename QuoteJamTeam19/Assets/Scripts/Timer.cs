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
    private bool hardStopTimer = false;

    private int hardMinuts;
    private int hardSeconds;

    private bool needHardSet = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        hardMinuts = minuts;
        hardSeconds = seconds;

        if (GameManager.Instance.TimerToSet)
        {
            minuts = GameManager.Instance.MinutsLeft;
            seconds = GameManager.Instance.SecondsLeft;
        }
        else
        {
            ResetTimer(minuts, seconds);
        }

        if (minuts <= 0 && seconds <= 60)
        {
            AlertCount();
        }
    }

    private void Update()
    {
        if (canCount && !hardStopTimer)
            StartCoroutine(Count());
    }

    public int GetMinuts()
    {
        return minuts;
    }

    public int GetSeconds()
    {
        return seconds;
    }

    void StartTimer()
    {
        canCount = true;
    }
    void StopTimer()
    {
        canCount = false;
    }

    public void HardStopTimer()
    {
        hardStopTimer = true;
    }

    void ResetTimer(int _minuts, int _seconds)
    {
        seconds = _seconds;
        minuts = _minuts;

        hardStopTimer = false;
    }

    public void HardResetTimer()
    {
        t_Timer.DOKill();

        ResetTimer(hardMinuts, hardSeconds);

        canCount = true;
    }

    public IEnumerator StartDamage()
    {
        while(!GameManager.Instance.isGamePaused)
        {
            DignityBar.Instance.ReduceDignity(2);
            yield return new WaitForSeconds(timerSpeed);
        }
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

        if(minuts == 0 && seconds == 59)
        {
            AlertCount();
        }

        if (minuts == 0 && seconds == 0)
        {
            StopTimer();
            t_Timer.text = minuts.ToString() + ":" + "0" + seconds.ToString();
            StartCoroutine(StartDamage());
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

    private void AlertCount()
    {
        print("one minut left!");
        //sound effect
        t_Timer.DOColor(Color.red, 2f);
        t_Timer.GetComponent<RectTransform>().DOShakePosition(60f, shakeAxis, shakeVibrato, shakeRandomness, false, false);
    }

    public void StartTimerParty()
    {
        gameObject.SetActive(true);
        t_Timer.DOColor(Color.white, 0f);
        t_Timer.GetComponent<RectTransform>().DOKill();
    }
}
