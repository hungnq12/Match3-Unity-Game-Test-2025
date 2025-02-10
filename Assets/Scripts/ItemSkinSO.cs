using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ItemSkinSO : ScriptableObject
{
    [SerializeField] private Sprite[] normalItemSprites; //dictionary<item type, sprite>
    [SerializeField] private Sprite[] bonusItemSprites;
    public Sprite NormalItemSprite(int id) => normalItemSprites[id];
    public Sprite BonusItemSprite(int id) => bonusItemSprites[id];

    #if UNITY_EDITOR
    [Button]
    private void UseFishTexture()
    {
        List<Sprite> sprites = new List<Sprite>();
        string assetPath = Constants.FISH_TEXTURES_PATH;
        string[] spritePath = Directory.GetFiles(assetPath, "*.png", SearchOption.AllDirectories);
        for (int i = 0; i < spritePath.Length; i++)
        {
            Sprite pngAsset = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath[i]);
            sprites.Add(pngAsset);
        }
        normalItemSprites = sprites.ToArray();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
    #endif
}
