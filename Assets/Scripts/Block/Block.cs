using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Block : MonoBehaviour
{
    [Header("UpDown")]
    [SerializeField] private List<Block> upBlocks;
    [SerializeField] private List<Block> downBlock;
    [SerializeField] private Transform[] getUpDownPositions = new Transform[4];
    [Header("DATA")]
	[SerializeField] private Image icon;
	[SerializeField] private Image bG;
	[SerializeField] private Button button;
    [SerializeField] private Color colorWhenCovered;
    [SerializeField] private int id = 0;
    [SerializeField] private float speedMove = 0.8f;
    [SerializeField] private Node node;

	private void Start()
	{
		GetBlockUp();
		GetBlockDown();
//		SetColor(colorWhenCovered);
		CheckConver();
		button.onClick.AddListener (Click);
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

	public void SetIcone(Sprite sprite){
		icon.sprite = sprite;
	}

    public void Click()
    {
        if (upBlocks.Count > 0)
        {
            return;
        }
		int duplicateBlock =0;
		int index = 0;
		if (!HolderManager.Instance.AddBlock (this, ref duplicateBlock, ref index)) {
			return;
		}
		Tween tween = MoveToHolder(HolderManager.Instance.HoldersPosition[index].position);
		if (duplicateBlock == HolderManager.Instance.CountFinish-1) {
			HolderManager.Instance.FinishBlock (id,index);
			tween.OnComplete (() => HolderManager.Instance.DestroyBlockFinish ());
		}
			
//            RemoveDownBlock();

    }
    public Tween MoveToHolder(Vector2 position)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
		Tween tween = transform.DOMove(new Vector3(position.x, position.y, -0.5f), speedMove);
        return tween;
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
//        SetColor(Color.white);
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
//        GetBlockList(downBlock, Vector3.forward);
    }
    private void GetBlockDown()
    {
//        GetBlockList(upBlocks, -Vector3.forward);
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
