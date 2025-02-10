using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class Item
{
    public Cell Cell { get; private set; }

    public ItemView View { get; private set; }


    public virtual void SetView()
    {
        /*string prefabname = GetPrefabName();

        if (!string.IsNullOrEmpty(prefabname))
        {
            GameObject prefab = Resources.Load<GameObject>(prefabname);
            if (prefab)
            {
                View = GameObject.Instantiate(prefab).transform;
            }
        }*/
        
        View = PoolManager.Instance.GetObject<ItemView>();
    }

    protected virtual string GetPrefabName() { return string.Empty; }

    public virtual void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal void AnimationMoveToPosition()
    {
        if (View == null) return;

        View.transform.DOMove(Cell.transform.position, 0.2f);
    }

    public void SetViewPosition(Vector3 pos)
    {
        if (View)
        {
            View.transform.position = pos;
        }
    }

    public void SetViewRoot(Transform root)
    {
        if (View)
        {
            View.transform.SetParent(root);
        }
    }

    public void SetSortingLayerHigher()
    {
        if (View == null) return;

        /*SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 1;
        }*/
        View.SortingOrder(1);
    }


    public void SetSortingLayerLower()
    {
        if (View == null) return;

        /*SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 0;
        }*/
        View.SortingOrder(0);

    }

    internal void ShowAppearAnimation()
    {
        if (View == null) return;

        Vector3 scale = View.transform.localScale;
        View.transform.localScale = Vector3.one * 0.1f;
        View.transform.DOScale(scale, 0.1f);
    }

    internal virtual bool IsSameType(Item other)
    {
        return false;
    }

    internal virtual void ExplodeView()
    {
        if (View)
        {
            View.transform.DOScale(0.1f, 0.1f).OnComplete(
                () =>
                {
                    //GameObject.Destroy(View.gameObject);
                    PoolManager.Instance.ReturnObject(View);
                    View = null;
                }
                );
        }
    }



    internal void AnimateForHint()
    {
        if (View)
        {
            View.transform.DOPunchScale(View.transform.localScale * 0.1f, 0.1f).SetLoops(-1);
        }
    }

    internal void StopAnimateForHint()
    {
        if (View)
        {
            View.transform.DOKill();
        }
    }

    internal void Clear()
    {
        Cell = null;

        if (View)
        {
            //GameObject.Destroy(View.gameObject);
            PoolManager.Instance.ReturnObject(View);
            View = null;
        }
    }
}
