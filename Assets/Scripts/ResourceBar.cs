using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;

public class ResourceBar : MonoBehaviour
{

    public MMProgressBar bar;
    public Image icon;
    public Sprite iconSprite;
    public Image led;
    public ResourceType type;

    [Range(0,1)]
    public float total;
    // Start is called before the first frame update
    void Start()
    {
        bar.SetBar01(total);
        icon.sprite = iconSprite;
    }

    public void updateBar(float value) {
        total += value;

        // ICI MEME A LA LIMITE
        
        total = Mathf.Clamp(total, 0, 1);
        bar.UpdateBar01(total);
    }
}
