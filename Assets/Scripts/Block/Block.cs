using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class Block : MonoBehaviour
{
    [Header("UpDown")]
    [SerializeField] private List<Block> upBlocks;
    [SerializeField] private List<Block> downBlock;
    [SerializeField] private Transform[] getUpDownPositions = new Transform[4];
    [Header("DATA")]
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer bG;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Color colorWhenCovered;
    [SerializeField] private int id = 0;
    [SerializeField] private float speedMove = 1;
    [SerializeField] private Node node;

	private void Start()
	{
		GetBlockUp();
		GetBlockDown();
		SetColor(colorWhenCovered);
		CheckConver();
	}

	public Sprite Icone
	{
		set{
			icon.sprite = value;
		}
	}

	public int ID
	{
		get{ return id; }
		set{ id = value; }
	}

    public void RemoveUpBlock(Block block)
    {
        upBlocks.Remove(block);
        CheckConver();
    }

	public void SetNode(Node node){
		this.node = node;
	}
    public void Click()
    {
        if (upBlocks.Count > 0)
        {
            return;
        }
        try
        {
            MoveFirst();
            RemoveDownBlock();
            boxCollider.enabled = false;
            HolderManager.Instance.AddCountHolderCurrent();
        }
        catch {
		}

    }
    public Tween MoveToHolder(Vector2 position)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        Tween tween = transform.DOMove(new Vector3(position.x, position.y, -0.5f), speedMove);
        return tween;
    }
    private void MoveFirst()
    {
	    Tween tween = MoveToHolder(HolderManager.Instance.Holders[HolderManager.Instance.CountHolderCurrent].position);
        tween.OnComplete(AddHolder);
    }
    private void AddHolder()
    {
        HolderManager.Instance.AddBlock(this);
    }
    private void SetColor(Color color)
    {
        icon.color = color;
        bG.color = color;
    }
    private void CheckConver()
    {
        if (upBlocks.Count != 0)
            return;
        SetColor(Color.white);
    }
    private void RemoveDownBlock()
    {
        foreach (Block block in downBlock)
        {
            block.RemoveUpBlock(this);
        }
    }
    private void GetBlockUp()
    {
        GetBlockList(downBlock, Vector3.forward);
    }
    private void GetBlockDown()
    {
        GetBlockList(upBlocks, -Vector3.forward);
    }
    private void GetBlockList(List<Block> blocks, Vector3 direction)
    {
        foreach (Transform positionGet in getUpDownPositions)
        {
            Ray ray = new Ray(positionGet.position, direction * 2);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Block block = hit.transform.GetComponent<Block>();
                if (block == null)
                    continue;
                if (blocks.Contains(block))
                    continue;
                blocks.Add(block);
            }
        }
    }
}
