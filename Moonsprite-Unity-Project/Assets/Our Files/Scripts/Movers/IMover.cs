using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover
{
    public float MoveSpeed
    {
        get;
        set;
    }

    public void Move(Vector2 moveVector);
}
