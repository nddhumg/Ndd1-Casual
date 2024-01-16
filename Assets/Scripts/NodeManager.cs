using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private int colum = 0;
    [SerializeField] private int row = 0;
    [SerializeField] private float distance = 0;
    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private List<Node> nodes;
    private static NodeManager instance;
    public static NodeManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("NodeManager not only");
        }

    }
    private void Start()
    {
        GenerateBlock();
    }
    public int GetIndexByMatrix(int row, int colum)
    {
        if (row == 0 || colum == 0)
        {
            return -1;
        }
        if (row > this.row || colum > this.colum)
        {
            return -1;
        }
        return colum == 1 ? row - 1 : (colum - 1) * this.row + row - 1;
    }
    public int GetIndexByMatrix(Node node)
    {
        int row = node.row;
        int colum = node.colum;
        return GetIndexByMatrix(row, colum);
    }
    void SpawnNode()
    {
        Vector2 temp = startingPosition;
        for (int columCurrent = 1; columCurrent <= colum; columCurrent++)
        {
            for (int rowCurrent = 1; rowCurrent <= row; rowCurrent++)
            {
                nodes.Add(new Node(temp, rowCurrent, columCurrent));
                temp.y -= distance;
            }
            temp.y = startingPosition.y;
            temp.x += distance;
        }
        SetUpArrayRelatedNode();
    }
    private void GenerateBlock()
    {
        SpawnNode();
        foreach (Node node in nodes)
        {
            if (node.isBlock == true)
            {
                node.SetRelatedNode();
            }
        }
    }
    private void SetUpArrayRelatedNode()
    {
        foreach (Node node in nodes)
        {

            int row = node.row;
            int colum = node.colum;
            int[] rowRelateNode = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] columRelateNode = { -1, 0, 1, 1, 1, 0, -1, -1 };
            for (int index = 0; index < node.arrayRelatedNode.Length; index++)
            {
                node.arrayRelatedNode[index] = GetNodeByIndex(GetIndexByMatrix(row + rowRelateNode[index], colum + columRelateNode[index]));
            }
        }
    }
    private Node GetNodeByIndex(int index)
    {
        try
        {
            return nodes[index];
        }
        catch
        {
            return null;
        }
    }

}
