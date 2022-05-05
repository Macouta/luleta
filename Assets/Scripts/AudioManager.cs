using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //variables
    public AudioSource powerOn, powerOff, spaceJump, ambiance, failed, invade, trade, wheel;
    
    //PUBLIC
    public void Play_powerOn()
    {
        powerOn.Play();
    }
    public void Play_powerOff()
    {
        powerOff.Play();
    }
    public void Play_spaceJump()
    {
        spaceJump.Play();
    }
    public void Play_ambianceFantasy()
    {
        ambiance.Play();
    }
    public void Play_ambianceReal()
    {
        ambiance.Stop();
    }
    public void Play_failed()
    {
        failed.Play();
    }
    public void Play_invade()
    {
        invade.Play();
    }
    public void Play_trade()
    {
        trade.Play();
    }
    public void Play_wheel(bool on)
    {
        if (on)
            wheel.UnPause();
        else
            wheel.Pause();
    }
    public void SetWheelSpeed(float speed)
    {
        float aspeed = Mathf.Abs(speed);
        wheel.volume = Mathf.Clamp(aspeed, 0f, 2f);
        wheel.pitch = Mathf.Clamp(aspeed, 1f, 3f);
    }

    //PRIVATE
    void Start()
    {
        wheel.Play();
        wheel.Pause();
    }

    void Update()
    {
        
    }


}
