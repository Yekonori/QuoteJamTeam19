using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class DignityBar : MonoBehaviour
{
    private float dignityAmount;
    private float maxDignity;
    private static DignityBar instance;
    public static DignityBar Instance { get { return instance; } }

    private Slider dignitySlider;

    public Image fillImage;

    public CharacterMovement player;

    [Header("Colors")]

    public Color goodColor = Color.green;
    public Color middleColor = Color.yellow;
    public Color dangerColor = Color.red;

    [Header("Value")]
    [Range(0, 1)]
    public float dangerValue = 0.25f;
    [Range(0, 1)]
    public float middleValue = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        maxDignity = 100f;
        dignitySlider = GetComponentInChildren<Slider>();

        if (GameManager.Instance.DignityToSet)
        {
            dignityAmount = GameManager.Instance.DignityLeft;
        }
        else
        {
            dignityAmount = maxDignity;
        }

        UpdateRatioSlider();
        UpdateSliderColor();
    }

    public float GetDignityValue()
    {
        return dignityAmount;
    }

    public void ReduceDignity(float amount)
    {
        float to = dignityAmount - amount;

        DOTween.To(() => dignityAmount, x => dignityAmount = x, to, 0.5f).OnUpdate(UpdateUI).OnComplete(UpdateUI);

        Mathf.Clamp(dignityAmount, 0, maxDignity);

        //player.SetDirty(dignityAmount);
    }

    private void UpdateUI()
    {
        UpdateRatioSlider();
        UpdateSliderColor();
    }

    private void UpdateRatioSlider()
    {
        dignitySlider.value = Mathf.Clamp(dignityAmount, 0, maxDignity) / 100;
        CheckIfDead();
    }

    private void UpdateSliderColor()
    {
        if (dignitySlider.value <= dangerValue)
        {
            fillImage.DOColor(dangerColor, 1f);
        }
        else if (dignitySlider.value <= middleValue)
        {
            fillImage.DOColor(middleColor, 1f);
        }
        else
        {
            fillImage.DOColor(goodColor, 1f);
        }
    }

    private void CheckIfDead()
    {
        if(dignityAmount <= 0)
        {
            GameManager.Instance.Lose();
        }
    }

    private void HardResetDignity()
    {
        gameObject.SetActive(true);

        maxDignity = 100f;
        dignitySlider = GetComponentInChildren<Slider>();

        dignityAmount = maxDignity;

        UpdateRatioSlider();
        UpdateSliderColor();
    }

    public void Hide()
    {
       HardResetDignity();
       gameObject.SetActive(false);
    }
}
