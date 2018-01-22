using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceUI : MonoBehaviour
{
    public Text positionNumber;
    public Text raceTime;
    public Text countdown;
    public Text lap;
    public Text lapTime;
    public GameObject lapTimeGO;
    public float lastLapTime;

    public void SetPosition(int number)
    {
        positionNumber.text = number.ToString();
    }

    public void SetRaceTime(float time)
    {
        string timer = (time / 60 - 0.5f).ToString("00") + ":" + (time % 60 - 0.5f).ToString("00") + ":" + time.ToString(".00").Split('.')[1];
        raceTime.text = timer;
    }

    public void SetCoundown(int number)
    {
        countdown.text = number.ToString();
        if (number <= 0)
        {
            countdown.gameObject.SetActive(false);
        }
    }

    public void SetLap(int number)
    {
        lap.text = number.ToString() + "/" + FindObjectOfType<RaceManager>().raceLaps.ToString();
    }

    public void SetLapTime(float time)
    {
        float temp = time;
        time -= lastLapTime;
        string timer = (time / 60 - 0.5f).ToString("00") + ":" + (time % 60 - 0.5f).ToString("00") + ":" + time.ToString(".00").Split('.')[1];
        lapTime.text = timer;
        lastLapTime = temp;
        lapTimeGO.SetActive(true);
        StartCoroutine(HideLapTime());
    }

    IEnumerator HideLapTime()
    {
        yield return new WaitForSeconds(2f);
        lapTimeGO.SetActive(false);
    }
}
