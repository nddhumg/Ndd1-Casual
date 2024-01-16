using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int row, colum;
    public Vector2 position;
    public bool isBlock = true;
    public Node(Vector2 position, int row, int colum)
    {
        this.position = position;
        this.row = row;
        this.colum = colum;
    }
    //0.Left Top   1.Top   2.Top Right   3.Right   4.Right Down   5.Down   6.Left Down   7.Left   
    [NonSerialized]
    public Node[] arrayRelatedNode = new Node[8];
    public void SetRelatedNode()
    {
        bool random = UnityEngine.Random.Range(0, 2) == 0 ? false : true;
        isBlock = random;
        if (isBlock == false)
        {
            return;
        }
        BlockManager.Instance.Spawn(position).GetComponent<Block>().Node = this;
        foreach (Node node in arrayRelatedNode)
        {
            if (node != null)
            {
                node.isBlock = false;
            }
        }
    }
}
