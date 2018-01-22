using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public List<CarPosition> AICars;
    public CarPosition playersCar;
    List<float> distances = new List<float>();
    public float raceTimer;
    int startTimer = 3;
    bool countdown = false;
    bool startedCounting = false;
    public RaceUI raceUI;
    public int raceLaps = 2;
    public List<Transform> spawnPoints;
    public List<Transform> reverseSpawnPoints;
    public GameObject aICarPrefab;
    public GameObject playerCarPrefab;
    public bool reverse;

    private void Awake()
    {
        if(reverse)
        {
            FindObjectOfType<FixNames>().ReverseOrder();
            FindObjectOfType<Waypoints>().points.Reverse();
        }
        //Load data about AI,track,players car
    }

    private void Start()
    {
        if (reverse) spawnPoints = reverseSpawnPoints;
        Transform playerSpawnPoint = spawnPoints[spawnPoints.Count - 1];
        foreach(Transform t in spawnPoints)
        {
            if(t != playerSpawnPoint)
            {
                AICars.Add(Instantiate(aICarPrefab, t.position, t.rotation * aICarPrefab.transform.rotation).GetComponent<CarPosition>());
            }
            else
            {
                playersCar = Instantiate(playerCarPrefab, t.position, t.rotation * playerCarPrefab.transform.rotation).GetComponent<CarPosition>();
            }
        }
        foreach (CarPosition c in AICars)
        {
            distances.Add(0);
        }
    }

    void OnEnable()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (startTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            raceUI.SetCoundown(--startTimer);
        }

        foreach (CarPosition cp in AICars)
        {
            cp.GetComponent<CarMovement>().canDrive = true;
            cp.GetComponent<CarResetter>().enabled = true;
        }
        playersCar.GetComponent<CarMovement>().canDrive = true;
        playersCar.GetComponent<CarResetter>().enabled = true;
        startedCounting = true;
    }

    public void EndRace(CarPosition cp)
    {
        cp.GetComponent<CarMovement>().canDrive = false;
        cp.GetComponent<CarResetter>().enabled = false;
        cp.GetComponent<CarMovement>().Turn(0);
        cp.GetComponent<CarMovement>().Accelerate(0);
        if (cp == playersCar)
            startedCounting = false;
    }

    private void Update()
    {
        if (startedCounting)
        {
            raceTimer += Time.deltaTime;
            raceUI.SetRaceTime(raceTimer);
        }
    }

    void FixedUpdate()
    {
        if (!playersCar)
            return;
        for (int i = 0; i < AICars.Count; i++)
        {
            distances[i] = AICars[i].GetDistance();
        }
        int position = 1;
        foreach (float f in distances)
        {
            if (playersCar.GetDistance() < f)
                position++;
        }
        playersCar.position = position;
        if (startedCounting)
            raceUI.SetPosition(position);
    }
}
