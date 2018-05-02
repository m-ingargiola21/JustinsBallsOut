using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedCellController : MonoBehaviour,ISubscriber
{
    public enum PossibleImmunizations { NotImmune, RedImmune, BlueImmune, GreenImmune }

    [SerializeField] Transform pointAtCamera;
    [SerializeField] Transform raycasterObject;
    [SerializeField] List<GameObject> immunization1;
    [SerializeField] List<GameObject> immunization2;
    [SerializeField] List<GameObject> immunization3;
    List<List<GameObject>> immunizations;
    Vector3 spawnPos;
    CellData DNA; // cell class information.
    public CellData DNAProperty
    { get { return DNA; } set { DNA = value; } }

    float spawnDistance = 10;
    bool isRedImmune;
    bool isBlueImmune;
    bool isGreenImmune;
    bool canAddSpawns;

    List<Transform> objectsMovingTowardCell;
    public List<Transform> ObjectsMovingTowardCell
    {
        get { return objectsMovingTowardCell; }
        set { objectsMovingTowardCell = value; }
    }

    public bool CanAddSpawns { get { return canAddSpawns; } }

    private void Awake()
    {
        canAddSpawns = true;
        DNA = new CellData();
        DNA.Setup();
    }
    // Use this for initialization
    private void Start ()
    {
        objectsMovingTowardCell = new List<Transform>();
        //InvokeRepeating("TestAddingImmunizations", 0f, 2f);
        immunizations = new List<List<GameObject>>()
        {
            immunization1,
            immunization2,
            immunization3
        };
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (objectsMovingTowardCell.Count == 5)
        {
            canAddSpawns = false;
            transform.GetComponent<CellTimer>().StopTimer(); //Added to slow down spawns for now
        }
        else
            canAddSpawns = true;
	}

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }



    /// <summary> ReOrientForSpawning:
    /// When cells want to spawn objects, makes sure that the spawned object isn't between the player and the cell(not infront of the cell so it doesn't get shot)
    /// Sorry I remembered what those numbers are and they aren't arbitrary or at least they don't pertain to the camera issue.  This script actually does 2 things
    /// The first you have mentioned about but the second deals with the below xAxis and yAxis. That is to make sure the object also does not spawn directly Behind
    /// the Cell.  So 1, not inbetween the camera and cell, 2 would be not behind the cell...  I think there is a better way to do this and I will change it to reflect
    /// that soon.  (basically take the direction toward the camera and subtract 180 from it to get pointing away from the camera, then do a similar range as below
    /// instead of arbitrary numbers that are subject to change upon rotation).
    /// </summary>
    /// <returns></returns>
    public void ReOrientForSpawning()
    {

        float xAxis = 0.0f;
        float xAxisAdjustedFront;
        float xAxisAdjustedBack;
        float yAxis = 0.0f;
        float yAxisAdjustedFront;
        float yAxisAdjustedBack;

        float pointAtCameraxAxisAdjusted;
        float pointAtCamerayAxisAdjusted;
        float pointBehindCellxAxisAdjusted;
        float pointBehindCellyAxisAdjusted;

        xAxis = Random.Range(0f, 359.9f);
        xAxisAdjustedFront = xAxis + 360f;
        xAxisAdjustedBack = xAxisAdjustedFront - 180f;
        yAxis = Random.Range(0f, 359.9f);
        yAxisAdjustedFront = yAxis + 360f;
        yAxisAdjustedBack = yAxisAdjustedFront - 180f;

        pointAtCameraxAxisAdjusted = pointAtCamera.eulerAngles.x + 360f;
        pointBehindCellxAxisAdjusted = pointAtCameraxAxisAdjusted - 180f;
        pointAtCamerayAxisAdjusted = pointAtCamera.eulerAngles.y + 360f;
        pointBehindCellyAxisAdjusted = pointAtCamerayAxisAdjusted - 180f;


        while ((xAxisAdjustedFront > pointAtCameraxAxisAdjusted - 5f && xAxisAdjustedFront < pointAtCameraxAxisAdjusted + 5f) ||
               (xAxisAdjustedBack > pointBehindCellxAxisAdjusted - 20f && xAxisAdjustedBack < pointBehindCellxAxisAdjusted + 20f))
        {
            xAxis = Random.Range(0f, 359.9f);
            xAxisAdjustedFront = xAxis + 360f;
            xAxisAdjustedBack = xAxisAdjustedFront - 180f;
        }

        while ((yAxisAdjustedFront > pointAtCamerayAxisAdjusted - 5f && yAxisAdjustedFront < pointAtCamerayAxisAdjusted + 5f) ||
               (yAxisAdjustedBack > pointBehindCellyAxisAdjusted - 20f && yAxisAdjustedBack < pointBehindCellyAxisAdjusted + 20f))
        {
            yAxis = Random.Range(0f, 359.9f);
            yAxisAdjustedFront = yAxis + 360f;
            yAxisAdjustedBack = yAxisAdjustedFront - 180f;
        }

        raycasterObject.eulerAngles = new Vector3(xAxis, yAxis, 0);

        if (objectsMovingTowardCell.Count < 5)
            EventManagerOld.CallObjectSpawn(SpawnTester(), transform);

    }

    /// <summary> SpawnTester:
    /// Calculates spawn distance for cell spawned objects, based on cell position and direction.
    /// </summary>
    private Vector3 SpawnTester()
    {
        //new Vector3(hitPoint.x, hitPoint.y + 15, hitPoint.z)
        DNA.CellPos = raycasterObject.position;
        DNA.CellDirection = raycasterObject.forward;
        spawnPos = DNA.CellPos + DNA.CellDirection * spawnDistance;
        return spawnPos;
    }

    /// <summary> CheckForImunization:
    /// work in progress TBD
    /// </summary>
    /// <param name="hitObjectTransform"></param>
    public bool CheckForImunization(Transform hitObjectTransform)
    {
        CheckCurrentImmune();
        switch (hitObjectTransform.name)
        {
            case "BlueObjectToSpawn(Clone)":
                if (isBlueImmune)
                {
                    DNA.IsImmunized = true;
                    RemoveImmunization(PossibleImmunizations.BlueImmune);
                }
                else
                {
                    DNA.TakeDamage();
                    DNA.IsImmunized = false;
                }
                break;
            case "GreenObjectToSpawn(Clone)":
                if (isGreenImmune)
                {
                    DNA.IsImmunized = true;
                    RemoveImmunization(PossibleImmunizations.GreenImmune);
                }
                else
                {
                    DNA.TakeDamage();
                    DNA.IsImmunized = false;
                }
                break;
            case "RedObjectToSpawn(Clone)":
                if (isRedImmune)
                {
                    DNA.IsImmunized = true;
                    RemoveImmunization(PossibleImmunizations.RedImmune);
                }
                else
                {
                    DNA.TakeDamage();
                    DNA.IsImmunized = false;
                }
                break;
        }

        return DNA.IsImmunized;
    }

    void RemoveImmunization(PossibleImmunizations immunization)
    {
        bool removedImmunization = false;

        foreach (List<GameObject> immune in immunizations)
        {
            if(!removedImmunization)
            {
                for (int i = 0; i < immune.Count; i++)
                {
                    if(immune[i].activeSelf)
                    {
                        if (i == 0)
                            break;
                        else if (i == (int)immunization)
                        {
                            switch(immunization)
                            {
                                case PossibleImmunizations.RedImmune:
                                    isRedImmune = false;
                                    break;
                                case PossibleImmunizations.BlueImmune:
                                    isBlueImmune = false;
                                    break;
                                case PossibleImmunizations.GreenImmune:
                                    isGreenImmune = false;
                                    break;
                            }

                            for (int j = 0; j < immune.Count; j++)
                            {
                                if (j == 0)
                                    immune[j].SetActive(true);
                                else
                                    immune[j].SetActive(false);
                            }

                            removedImmunization = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    void CheckCurrentImmune()
    {
        foreach (List<GameObject> immune in immunizations)
        {
            for (int i = 0; i < immune.Count; i++)
            {
                if(immune[i].activeSelf)
                {
                    if (i == 0)
                        break;
                    else if (i == (int)PossibleImmunizations.RedImmune)
                        isRedImmune = true;
                    else if (i == (int)PossibleImmunizations.BlueImmune)
                        isBlueImmune = true;
                    else if (i == (int)PossibleImmunizations.GreenImmune)
                        isGreenImmune = true;
                }
            }
        }
    }

    void HandleImmunizationVisual(Transform transforToCheckForImmunization)
    {



    }

    /// <summary>
    /// See below, except this is what will be called when the player interacts with the cell in the game
    /// the passed paramter is the Enum which is limited to four options and it is cast to an int to use it.
    /// </summary>
    /// <param name="immunization"></param>
    public void AddImmunization(PossibleImmunizations immunization)
    {
        //int immuneCount = 0;
        int emptyImmunePointer = 0;
        
        bool anyEmptyImmune = true;

        if (anyEmptyImmune)
        {
            foreach (List<GameObject> immune in immunizations)
            {
                if (immune[0].activeSelf)
                {
                    anyEmptyImmune = true;
                    emptyImmunePointer = immunizations.IndexOf(immune);
                    break;
                }

                if (immunizations.IndexOf(immune) == immunizations.Count - 1)
                {
                    anyEmptyImmune = false;
                    break;
                }
            }

            if (anyEmptyImmune)
            {
                for (int i = 0; i < immunizations[emptyImmunePointer].Count; i++)
                {
                    if (i == (int)immunization)
                    {
                        immunizations[emptyImmunePointer][i].SetActive(true);
                    }
                    else
                    {
                        immunizations[emptyImmunePointer][i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 1; i < immunizations.Count; i++)
                {
                    if (i == 1)
                    {
                        CycleThroughImmunizations(i);
                    }

                    if (i == 2)
                    {
                        CycleThroughImmunizations(i);
                    }
                }

                foreach (GameObject immu in immunizations[immunizations.Count - 1])
                {
                    immu.SetActive(false);
                }

                immunizations[immunizations.Count - 1][(int)immunization].SetActive(true);
            }
        }
    }

    /// <summary>
    /// This is a test, it takes the cell when it spawns (with three empty immune slots)
    /// then it populates the immunization slots with a random immunization every 2 seconds
    /// if the cell i full it cycles the immunizations dropping off the oldest and keeping the newest three
    /// </summary>
    void TestAddingImmunizations()
    {
        int randomImmune;
        int immuneCount = 0;
        int emptyImmunePointer = 0;

        randomImmune = Random.Range(1, 4);

        bool anyEmptyImmune = true;

        if (anyEmptyImmune)
        {
            foreach (List<GameObject> immune in immunizations)
            {
                if (immune[0].activeSelf)
                {
                    anyEmptyImmune = true;
                    emptyImmunePointer = immunizations.IndexOf(immune);
                    break;
                }

                if (immunizations.IndexOf(immune) == immunizations.Count - 1)
                {
                    anyEmptyImmune = false;
                    break;
                }
            }

            if (anyEmptyImmune)
            {
                for (int i = 0; i < immunizations[emptyImmunePointer].Count; i++)
                {
                    if (i == randomImmune)
                    {
                        immunizations[emptyImmunePointer][i].SetActive(true);
                    }
                    else
                    {
                        immunizations[emptyImmunePointer][i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 1; i < immunizations.Count; i++)
                {
                    if(i == 1)
                    {
                        CycleThroughImmunizations(i);
                    }

                    if (i == 2)
                    {
                        CycleThroughImmunizations(i);
                    }
                }

                foreach (GameObject immu in immunizations[immunizations.Count - 1])
                {
                    immu.SetActive(false);
                }

                immunizations[immunizations.Count - 1][randomImmune].SetActive(true);
            }
        }
    }

    /// <summary>
    /// Moves the immunizations down one in order to cycle through the three possible options
    /// for example if the player is red, red, blue immune and becomes green immune it will 
    /// make them red, blue, green immune.
    /// int i is a refference to a for loop that is pointing at immunizations (the List<List<GameObject>>)
    /// </summary>
    /// <param name="i"></param>
    void CycleThroughImmunizations(int i)
    {
        int activeObjectPointer = 0;

        foreach (GameObject immu in immunizations[i - 1])
        {
            immu.SetActive(false);
        }

        for (int j = 0; j < immunizations[i].Count; j++)
        {
            if (immunizations[i][j].activeSelf)
                activeObjectPointer = j;
        }

        immunizations[i - 1][activeObjectPointer].SetActive(true);
    }

    void DespawnCell(Transform cellToDespawn)
    {
        if(cellToDespawn == transform)
        {
            foreach (Transform t in objectsMovingTowardCell)
            {
                if(t != null)
                    DestroyObject(t.gameObject, .1f);
            }
            DestroyObject(gameObject, .1f);
        }
    }

    public void Subscribe()
    {
        EventManagerOld.SpawnEvents += ReOrientForSpawning;
        EventManagerOld.OnDespawnCell += DespawnCell;
    }

    public void Unsubscribe()
    {
        EventManagerOld.SpawnEvents -= ReOrientForSpawning;
        EventManagerOld.OnDespawnCell -= DespawnCell;
    }
}
