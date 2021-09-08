using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DignityBar : MonoBehaviour
{
    private float dignityAmount;
    private float maxDignity;
    private static DignityBar instance;
    public static DignityBar Instance { get { return instance; } }

    private Slider dignitySlider;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    private void Start()
    {
        maxDignity = 100f;
        dignityAmount = maxDignity;
        dignitySlider = GetComponentInChildren<Slider>();
    }

    public void ReduceDignity(float amount)
    {
        dignityAmount -= amount;
        Mathf.Clamp(dignityAmount, 0, maxDignity);
        UpdateRatioSlider();
    }

    private void UpdateRatioSlider()
    {
        dignitySlider.value = Mathf.Clamp(dignityAmount, 0, maxDignity) / 100;
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if(dignityAmount <= 0)
        {
            GameManager.Instance.Lose();
        }
    }
}
