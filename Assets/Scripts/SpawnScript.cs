using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
    public ObjectPoolerScript popcorn;
    public ObjectPoolerScript medium;



    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Awake()
    {

        StartCoroutine("Script");

        


    }
    IEnumerator Script (){
        //Phase 1: 0-30sec
        PresetA();
        yield return new WaitForSeconds(11f);
        //PresetA();


    }
    void PresetA()
    {
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 0f), 1f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 0f), 2f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 0f), 3f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 0f), 4f));

        StartCoroutine(Spawn(popcorn, new Vector2(9f, 2f), 7f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 1f), 8f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 0f), 9f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, -1f), 10f));

        StartCoroutine(Spawn(popcorn, new Vector2(9f, -1f), 12f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 0f), 13f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 1f), 14f));
        StartCoroutine(Spawn(popcorn, new Vector2(9f, 2f), 15f));

       


    }

    IEnumerator Spawn(ObjectPoolerScript objPool, Vector2 position, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject NewEnemy = objPool.GetPooledObject();
        NewEnemy.SetActive(true);
        NewEnemy.transform.position = position;
    }


}
