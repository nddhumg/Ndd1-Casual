using System.Collections;
using System.Collections.Generic;
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
		Spawn ();
    }

	public static BlockManager Instance { get { return instance; } }

	private void Spawn()
    {
		if (NodeManager.Instance.Nodes.Count == 0) {
			Invoke (nameof(Spawn), 0.5f);
			return;
		}
		foreach (Node node in NodeManager.Instance.Nodes) {
			GameObject obj = Instantiate (prefab, node.position, Quaternion.identity);
			blocks.Add (obj.GetComponent<Block> ());
		}
		CheckIndexBlock ();
		SetRandomBlock ();
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
				Block block;
				try
				{
                	 block = blocks[ranBlock];
				}catch{
					return;
				}
                block.ID = ranImage;
				block.Icone = dataImage.sprites[ranImage];
                blocks.Remove(block);
            }
        }
    }

}
