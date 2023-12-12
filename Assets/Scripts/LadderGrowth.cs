using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGrowth : MonoBehaviour
{
    public GameObject treeLadder;
    // Start is called before the first frame update
    void Start()
    {
        treeLadder.transform.localScale = new Vector3(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        treeLadder.transform.localScale = Vector3.Lerp(treeLadder.transform.localScale, new Vector3(5, 5, 1), Time.deltaTime - 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            treeLadder.transform.localScale = new Vector3(5, 5, 1);
        }
    }
}
