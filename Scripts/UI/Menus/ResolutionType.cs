using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ResolutionType
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

}


