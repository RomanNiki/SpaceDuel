﻿using Models.Player.Interfaces;
using UnityEngine;

public sealed class Sun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IDamageVisitor visitor))
        {
            visitor.Visit(this);
        }
    }
}