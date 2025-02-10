using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField] private ItemSkinSO itemSkinSO;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void ApplySkin(BonusItem.eBonusType type)
    {
        spriteRenderer.sprite = itemSkinSO.BonusItemSprite((int)type);
    }
    public void ApplySkin(NormalItem.eNormalType type)
    {
        spriteRenderer.sprite = itemSkinSO.NormalItemSprite((int)type);
    }

    public void SortingOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }
}
