using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HolderManager : MonoBehaviour
{
    [SerializeField] private List<Transform> holders;
    [SerializeField] private List<Block> blocksHolder = new List<Block>(6);
    [SerializeField] private int countHolderCurrent = 0;
    [SerializeField] private int countHolder = 6;
    [SerializeField] private int countFinish = 3;

    private static HolderManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("HolderManager not only");
        }
    }
    private void Start()
    {
        foreach (Transform tf in transform)
        {
            holders.Add(tf);
        }
    }
    public List<Transform> Holders => holders;
    public int CountHolderCurrent => countHolderCurrent;
    public static HolderManager Instance => instance;
    public void AddCountHolderCurrent()
    {
        ++countHolderCurrent;
    }
    public void AddBlock(Block block)
    {
        if (blocksHolder.Count >= countHolder)
        {
            return;
        }
        blocksHolder.Add(block);
        BlockPile();
    }
    public void CheckFinish()
    {
        if (blocksHolder.Count < 3)
            return;
        List<Block> blockFinish = GetBlockFinish();
        if (blockFinish == null)
        {
            return;
        }
        foreach (Block block in blockFinish)
        {
            --countHolderCurrent;
            blocksHolder.Remove(block);
            Destroy(block.gameObject);
        }
        BlockPile();
    }

    private List<Block> GetBlockFinish()
    {
        Dictionary<int, List<Block>> blockDictionary = new Dictionary<int, List<Block>>();
        foreach (Block block in blocksHolder)
        {
            int id = block.ID;
            if (blockDictionary.ContainsKey(id))
            {
                blockDictionary[id].Add(block);
            }
            else
            {
                blockDictionary.Add(id, new List<Block> { block });
            }
        }
        foreach (var temp in blockDictionary)
        {
            if (temp.Value.Count >= countFinish)
            {
                return temp.Value;
            }
        }
        return null;
    }
    private void BlockPile()
    {
        int temp = 0;
        foreach (Block block in blocksHolder)
        {
            block.MoveToHolder(holders[temp].position);
            ++temp;
        }
    }
}
