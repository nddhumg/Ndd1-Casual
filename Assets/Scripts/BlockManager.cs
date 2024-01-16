using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private List<Block> blocks;
    [SerializeField] private SOBlockImages dataImage;
    [SerializeField] private int indexDone = 3;
    [SerializeField] private GameObject prefab;
    private static BlockManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetRandomBlock();
    }
    public static BlockManager Instance => instance;
    public GameObject Spawn(Vector3 position)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        blocks.Add(obj.GetComponent<Block>());
        return obj;
    }

    void CheckIndexBlock()
    {
        if (blocks.Count % 3 == 0)
            return;
        Debug.LogError("Block count % 3 != 0");
    }
    void SetRandomBlock()
    {
        int i = 0;
        while (blocks.Count > 0)
        {
            i++;
            if (i >= 1000)
                return;
            int ranImage = UnityEngine.Random.Range(0, dataImage.sprites.Length);
            for (int index = 0; index < indexDone; index++)
            {
                int ranBlock = UnityEngine.Random.Range(0, blocks.Count);
                Block block = blocks[ranBlock];
                block.ID = ranImage;
                block.SetIcon(dataImage.sprites[ranImage]);
                blocks.Remove(block);
            }
        }
    }
}
