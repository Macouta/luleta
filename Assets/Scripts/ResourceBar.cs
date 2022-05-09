using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using Sirenix.OdinInspector;

public enum Status
{
    None, Low, Medium, High
}

public class ResourceBar : MonoBehaviour
{

    [BoxGroup("UI")]
    public MMProgressBar bar;
    [BoxGroup("UI")]
    public Image icon;
    [BoxGroup("UI")]
    public Sprite iconSprite;
    [BoxGroup("UI")]
    public Image led;

    [Space]

    [BoxGroup("Resource")]
    public ResourceType type;

    [BoxGroup("Resource")]
    [MinMaxSlider(0, 100, true)]
    public Vector2Int minMaxWin = new Vector2Int(0, 100);

    [BoxGroup("Resource")]
    [MinMaxSlider(0, 100, true)]
    public Vector2Int minMaxLoose = new Vector2Int(0, 100);

    [BoxGroup("Resource")]
    [Range(0,1)]
    public float total;

    private float nextValue;

    [BoxGroup("Resource")]
    public Status status;

    // Start is called before the first frame update
    void Start()
    {
        bar.SetBar01(total);
        icon.sprite = iconSprite;
    }

    public void updateBar(float value) {
        total += value;
        Debug.Log(type.ToString() + " " + value);

        // ICI MEME A LA LIMITE
        
        total = Mathf.Clamp(total, 0, 1);
        bar.UpdateBar01(total);
        SetStatus();
    }

    public float setNextValue(float sign) {
        nextValue = (float)(sign > 0 ? Random.Range(minMaxWin.x, minMaxWin.y) : -Random.Range(minMaxLoose.x, minMaxLoose.y))/100f;
        return nextValue;
    }

    public void updateBarRng() {
        Debug.Log(type.ToString() + " " + nextValue);
        updateBar(nextValue);
    }

    private void SetStatus()
    {
        if (total <= 0f)
            status = Status.None;
        else if (total <= 0.2f)
            status = Status.Low;
        else if (total <= 0.7f)
            status = Status.Medium;
        else
            status = Status.High;
    }
}
