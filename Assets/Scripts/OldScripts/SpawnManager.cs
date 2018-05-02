using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour,ISubscriber
{

    [SerializeField] Transform cellSpawnPrefab;
    [SerializeField] Transform greenSpawnPrefab;
    [SerializeField] Transform redSpawnPrefab;
    [SerializeField] Transform blueSpawnPrefab;
    [SerializeField] float spawnedObjectLerpReduction = 0.025f;
    Transform clone;
    List<Transform> spawnedObjects;
    List<Transform> spawnedCells;
    int lerpReductionMultiplier;
    int spawnRandomChoice;
    SpawnedCellController spawnedCellController;

    private void Awake()
    {
        spawnedObjects = new List<Transform>();
        spawnedCells = new List<Transform>();
        lerpReductionMultiplier = 1;
        
    }
    private void Start()
    {
        InvokeRepeating("IncreaseLerpReductionMultiplier", 5.0f, 5.0f);
    }
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void CheckCellsToSpawnObjects(Vector3 objectSpawnPoint, Transform objectEndPoint)
    {
        foreach (Transform cell in spawnedCells)
        {
            if(objectEndPoint == cell)
            {
                clone = Instantiate(PickObjectToSpawn(), objectSpawnPoint, Quaternion.identity);
                clone.GetComponent<SpawnedObjectMovement>().EndPos = objectEndPoint.position;

                spawnedCellController = objectEndPoint.GetComponent<SpawnedCellController>();
                spawnedCellController.ObjectsMovingTowardCell.Add(clone);
            }
        }
    }

    /// <summary> PickObjectToSpawn:
    /// randomly chooses which object to spawn and returns a transform with the value of the prefab.
    /// </summary>
    private Transform PickObjectToSpawn()
    {
        Transform returnTransform;
        spawnRandomChoice = Random.Range(1, 3);//max is exclusive, which I think means in order to include #3 we make the max four.

        if (spawnRandomChoice == 1)
            returnTransform = greenSpawnPrefab;
        else if (spawnRandomChoice == 2)
            returnTransform = redSpawnPrefab;
        else
            returnTransform = blueSpawnPrefab;

        return returnTransform;

    }

    /// <summary> SpawnCorrectObject:
    /// instantiates a transform and adds the data to spawnedCells list while starting the timer.
    /// </summary>
    /// <param name="hitPoint"></param>
    private void SpawnCell(Vector3 hitPoint, Transform endPoint)
    {
        clone = Instantiate(cellSpawnPrefab, hitPoint, Quaternion.identity);
        clone.transform.LookAt(new Vector3(hitPoint.x, hitPoint.y, Camera.main.transform.position.z));

        spawnedCells.Add(clone);

        clone.GetComponent<CellTimer>().StartTimer();

    }

    /// <summary> RemoveSpawnedObjectFromList:
    /// If the spawnedObjectTransform is included in the list spawnedObjects then it is removed when function is called
    /// </summary>
    /// <param name="spawnedObjectTransform"></param>
    public void RemoveSpawnedObjectFromList(Transform spawnedObjectTransform)
    {

        if (spawnedObjects.Contains(spawnedObjectTransform))
            spawnedObjects.Remove(spawnedObjectTransform);

    }

    /// <summary> RemoveSpawnedCellFromList:
    /// removes the Spawned Cell Tranform if included in the transform list spawnedCells.
    /// </summary>
    /// <param name="spawnedCellTransform"></param>
    public void RemoveSpawnedCellFromList(Transform spawnedCellTransform)
    {

        if (spawnedCells.Contains(spawnedCellTransform))
            spawnedCells.Remove(spawnedCellTransform);

    }

    /// <summary> IncreaseLerpReductionMultiplier:
    /// increases the multiplier value by one
    /// </summary>
    private void IncreaseLerpReductionMultiplier()
    {
        lerpReductionMultiplier++;
    }


    /// <summary> ResetLerpReduction:
    /// returns variable to initial value.
    /// </summary>
    public void ResetLerpReduction()
    {
        lerpReductionMultiplier = 1;
    }
    /// <summary>
    /// Interface function that adds a function from this class to the event manger
    /// </summary>
    public void Subscribe()
    {
        EventManagerOld.OnCellSpawn += SpawnCell;
        EventManagerOld.OnDespawnObject += RemoveSpawnedObjectFromList;
        EventManagerOld.OnDespawnCell += RemoveSpawnedCellFromList;
        EventManagerOld.OnObjectSpawn += CheckCellsToSpawnObjects;
    }
    
    public void Unsubscribe()
    {
        EventManagerOld.OnCellSpawn -= SpawnCell;
        EventManagerOld.OnDespawnObject -= RemoveSpawnedObjectFromList;
        EventManagerOld.OnDespawnCell -= RemoveSpawnedCellFromList;
        EventManagerOld.OnObjectSpawn -= CheckCellsToSpawnObjects;
    }
}
