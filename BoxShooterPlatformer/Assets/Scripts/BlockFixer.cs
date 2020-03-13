using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFixer : MonoBehaviour
{

    private const float BLOCK_EDGE_SIZE = 0.1f;
    private const float CORNER_TRIM_SIZE = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] blockArr = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in blockArr) {
            // N
            GameObject n = Instantiate(block, new Vector3(block.transform.position.x, block.transform.position.y + (block.transform.localScale.y/2f) - (BLOCK_EDGE_SIZE/2), block.transform.position.z), block.transform.rotation);
            n.transform.localScale = new Vector3(block.transform.localScale.x, BLOCK_EDGE_SIZE, block.transform.localScale.z);
            n.tag = "Ground";
            
            // E
            GameObject e = Instantiate(block, new Vector3(block.transform.position.x + (block.transform.localScale.x/2f) - (BLOCK_EDGE_SIZE/2), block.transform.position.y, block.transform.position.z), block.transform.rotation);
            e.transform.localScale = new Vector3(BLOCK_EDGE_SIZE, block.transform.localScale.y-CORNER_TRIM_SIZE, block.transform.localScale.z);
            e.tag = "Side";

            // S
            GameObject s = Instantiate(block, new Vector3(block.transform.position.x, block.transform.position.y - (block.transform.localScale.y/2f) + (BLOCK_EDGE_SIZE/2), block.transform.position.z), block.transform.rotation);
            s.transform.localScale = new Vector3(block.transform.localScale.x, BLOCK_EDGE_SIZE, block.transform.localScale.z);
            s.tag = "Ceiling";
            // W
            GameObject w = Instantiate(block, new Vector3(block.transform.position.x - (block.transform.localScale.x/2f) + (BLOCK_EDGE_SIZE/2), block.transform.position.y, block.transform.position.z), block.transform.rotation);
            w.transform.localScale = new Vector3(BLOCK_EDGE_SIZE, block.transform.localScale.y-CORNER_TRIM_SIZE, block.transform.localScale.z);
            w.tag = "Side";
        }        
    }
}
