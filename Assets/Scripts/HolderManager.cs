using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HolderManager : MonoBehaviour {
	[SerializeField] private List<Transform> holdersPosition;
	[SerializeField] private List<Block> blocksHolder = new List<Block>();
	[SerializeField] private List<Block> blocksFinish = new List<Block>();
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
			holdersPosition.Add(tf);
		}
	}

	public static HolderManager Instance{
		get 
		{
			return instance;
		}
	}

	public  int CountFinish{
		get 
		{
			return countFinish;
		}
	}
	public List<Transform> HoldersPosition{
		get{ 
			return holdersPosition;
		}
	}
	public bool AddBlock(Block block,ref int duplicateCardNumber,ref int index){
		if (blocksHolder.Count >= countHolder) {
			Debug.LogError ("Maximum block list,can't add ", gameObject);
			return false;
		}
		index = 0;
		foreach (Block blockInList in blocksHolder){
			if (blockInList.ID == block.ID) {
				++duplicateCardNumber;
			}
			else if(duplicateCardNumber != 0){
				blocksHolder.Insert (index, block);
				BlockSort ();
				return true;
			}
			index ++;
		}
		blocksHolder.Add (block);
		return true;
	}

	public void FinishBlock(int id,int index){
		for (int i = index; i > index - 3; i--) {
			blocksFinish.Add (blocksHolder[i]);
			blocksHolder.RemoveAt (i);
		}
	}

	public void DestroyBlockFinish(){
		for (int i = countFinish-1; i >= 0; i--) {
			Destroy (blocksFinish[i].gameObject);
			blocksFinish.RemoveAt (i);
		}
		BlockSort ();
	}

	private void BlockSort(){
		int temp = 0;
		foreach (Block block in blocksHolder)
		{
			block.MoveToHolder(holdersPosition[temp].position);
			++temp;
		}
	}

}
