using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    public delegate void BlockOutOfBoundsDelegate();
    public static event BlockOutOfBoundsDelegate OnBlockOutOfBounds;
    public delegate void NextBlockGenerateDelegate(int blockIndex);
    public static event NextBlockGenerateDelegate OnNextBlockGenerated;

    public GameObject[] blockPrefabs;
    public Transform blockSpawnPosition;
    public Block activeBlock;
    private int nextBlockIndex;
    public float verticalMoveLength = 0.5f;

    public float horizontalMoveLength = 0.5f;

    private void OnEnable()
    {
        Block.OnBlockHit += Block_OnBlockHit;
    }
    private void OnDisable()
    {
        Block.OnBlockHit -= Block_OnBlockHit;
    }

    private void Block_OnBlockHit(Block block, GameObject otherObject)
    {
        if (GameManager.instance.state != GameManager.State.Play)
        {
            return;
        }

        // check if block is same type, then remove both and give score
        // check if other block is floor, then do nothing.
        activeBlock = SpawnRandomBlock(nextBlockIndex);
        GenerateNextBlock();

        if (block.transform.position.y > blockSpawnPosition.position.y)
        {
            OnBlockOutOfBounds?.Invoke();
        }
        Block otherBlock = otherObject.GetComponent<Block>();
        if (otherBlock != null && block.blockType == otherBlock.blockType)
        {
            // remove blocks
            block.Remove();
            otherBlock.Remove();
        }
        
    }

    public Block SpawnRandomBlock(int index = -1)
    {
        if (index == -1 )
        {
            index = UnityEngine.Random.Range(0, blockPrefabs.Length);
        }
        GameObject prefab = blockPrefabs[index];
        GameObject block = Instantiate(prefab, blockSpawnPosition.position, Quaternion.identity, transform);
        return block.GetComponent<Block>();
    }

    internal void Init()
    {
        // Remove blocks
        Block[] blocks = transform.GetComponentsInChildren<Block>();
        for (int i = 0; i < blocks.Length; i++)
        {
            Destroy(blocks[i].gameObject);
        }

        InitBlocks();

    }

    private void InitBlocks()
    {

        // Spawn one random block
        activeBlock = SpawnRandomBlock();
        GenerateNextBlock();

    }

    private void GenerateNextBlock()
    {
        
        nextBlockIndex = UnityEngine.Random.Range(0, blockPrefabs.Length);
        OnNextBlockGenerated?.Invoke(nextBlockIndex);
    }
}
