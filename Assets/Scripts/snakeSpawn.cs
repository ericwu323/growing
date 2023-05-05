using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeSpawn : MonoBehaviour
{
    public GameObject snake;
    private Transform trans;
    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        trans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //spawnsnake();
    }
    public void spawnsnake()
    {
        Vector3 randomPoint = new Vector3(Random.Range(0f, 50f),
                                   0,
                                   Random.Range(0f, 50f));
        randomPoint = trans.TransformPoint(randomPoint);
        GameObject hi = Instantiate(snake, trans.position, Quaternion.identity);
        hi.GetComponent<enemy>().points[0] = center;
    }
    IEnumerator wait()
    {
        spawnsnake();
        yield return new WaitForSeconds(3f);
        
    }
}
