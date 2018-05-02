using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnMissedObjects : MonoBehaviour
{

    //SpawnManager spawnManager;
    SpawnedCellController spawnedCellController;

	// Use this for initialization
	void Start ()
    {
        //spawnManager = GameObject.Find("Managers").GetComponent<SpawnManager>();
        spawnedCellController = transform.parent.GetComponent<SpawnedCellController>();
	}

    private void OnTriggerEnter(Collider other)// activated during collision
    {
        if(other.GetComponent<SpawnedObjectMovement>().EndPos == transform.position)
        {
            EventManagerOld.CallDespawnObject(other.transform);
            //spawnManager.RemoveSpawnedObjectFromList(other.transform);
            //spawnedCellController.ObjectsMovingTowardCell.Remove(other.transform);
            Destroy(other.gameObject, .1f);

            if (!spawnedCellController.CheckForImunization(other.transform))//Condition: if cell isImmunized = false
            {
                if (spawnedCellController.DNAProperty.CellHealth <= 0) //condition : if cell health less than or equal to zero
                    DestroyCell();
            }
        }
    }

    /// <summary> DestroyCell:
    /// removes object informatin from list
    /// destroyes the parent object attached to the transform.
    /// </summary>
    void DestroyCell()
    {
        EventManagerOld.CallDespawnCell(transform.parent);
        //Destroy(transform.parent.gameObject, .1f);
    }
}