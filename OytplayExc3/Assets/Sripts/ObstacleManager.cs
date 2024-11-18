using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //init data
    public GameObject _obstacle;
    public ObstacleManager _parentManager;

    public Vector3 _size = new Vector3(2f, 2f, 2f);

    //used for calculatinf distance between obstacles
    public float _minSpawnDist = 10;
    public float _maxSpawnDist = 20f;

    //used for random pos generation
    public Vector3 minSpawnPosition = new Vector3(-100f, -100f, -100f); 
    public Vector3 maxSpawnPosition = new Vector3(100f, 100f, 100f); 

    private void Start()
    {
        //get manager/marent of obstecles to be generated
        if(!_parentManager)
            FindObjectOfType<ObstacleManager>();

        //generate obstacles
        for (int i = 0; i < 100; i++)
        {
            GameObject _instantiatedObstcale = Instantiate(_obstacle, GenerateRandomPos(), Quaternion.identity);
            _instantiatedObstcale.transform.SetParent(transform); //make obstacles children of manager 
            _instantiatedObstcale.transform.localScale = _size; //scale up or down, designer's choice
        }
    }

    private Vector3 GenerateRandomPos()
    {
        //generate rand pos using data initialised
        Vector3 _randPos = new Vector3(Random.Range(minSpawnPosition.x, maxSpawnPosition.x), Random.Range(minSpawnPosition.y, maxSpawnPosition.y), Random.Range(minSpawnPosition.z, maxSpawnPosition.z));

        //ensure obstacles have a certain distance betwene them -> no overlapping
        while (Vector3.Distance(transform.position, _randPos) < _minSpawnDist)
        {
            _randPos = GenerateRandomPos(); //gen rand pos
        }

        return _randPos; //return pos
    }
}
