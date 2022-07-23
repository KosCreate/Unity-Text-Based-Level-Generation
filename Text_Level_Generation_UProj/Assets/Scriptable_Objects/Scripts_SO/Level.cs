using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level Generation/Level", order = 0)]
public class Level : ScriptableObject
{
    public TextAsset levelTextAsset;
    public char[] symbols;
    public List<GameObject> prefabs = new List<GameObject>();
    public Dictionary<char, GameObject> objectsCollection = new Dictionary<char, GameObject>();
    private int width, height;
    private string[] levelStrings;
    public void Initialization()
    {
        string widthT = levelTextAsset.text[0].ToString() + levelTextAsset.text[1].ToString();
        string heightT = levelTextAsset.text[4].ToString() + levelTextAsset.text[5].ToString();
        width = Int32.Parse(widthT);
        height = Int32.Parse(heightT);
        levelStrings = levelTextAsset.text.Split('\n');
        for (int i = 0; i < prefabs.Count; i++) {
            objectsCollection.Add(symbols[i], prefabs[i]);
        }
    }

    public (int, int) GetWidthAndHeight() {
        return (width, height);
    }

    public string[] GetLevelStrings() {
        return levelStrings;
    }

    public Dictionary<char, GameObject> GetObjectCollection() {
        return objectsCollection;
    }
}