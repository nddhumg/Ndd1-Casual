using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int row, colum;
    public Vector2 position;
    public Node(Vector2 position, int row, int colum)
    {
        this.position = position;
        this.row = row;
        this.colum = colum;
    }
}
