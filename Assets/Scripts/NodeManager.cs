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
		SpawnNode();
	}

	public static NodeManager Instance { 
		get { return instance; } 
	}

	public List<Node> Nodes{
		get{ return nodes;}
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
        Vector2 positionSpawn = startingPosition;
        for (int columCurrent = 1; columCurrent <= colum; columCurrent++)
        {
            for (int rowCurrent = 1; rowCurrent <= row; rowCurrent++)
            {
                nodes.Add(new Node(positionSpawn, rowCurrent, columCurrent));
                positionSpawn.y -= distance;
            }
            positionSpawn.y = startingPosition.y;
            positionSpawn.x += distance;
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
