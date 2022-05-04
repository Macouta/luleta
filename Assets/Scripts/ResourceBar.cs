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

    [Range(0,1)]
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        bar.SetBar01(value);
        icon.sprite = iconSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
