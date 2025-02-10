using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContainer : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private ItemView _itemViewPrefab;

    private void Start()
    {
        PoolManager.Instance.CreatePool(_cellPrefab, 10);
        PoolManager.Instance.CreatePool(_itemViewPrefab, 10);
    }
}
