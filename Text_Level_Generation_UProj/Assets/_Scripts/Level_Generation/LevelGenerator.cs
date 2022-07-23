using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private Vector3 levelRotation;
    private Vector3 _startPosition;
    private GameObject _levelGo;
    private void Start() => Initialization(); 
    
    private void Initialization() {
        level.Initialization();
        _levelGo = new GameObject();
        _levelGo.name = "Level";
        _levelGo.transform.SetParent(transform);
        _startPosition = transform.position;
        int levelWidth = level.GetWidthAndHeight().Item1;
        // Start the loop on the third line since the first two is for the width and height number
        for (int y = 2; y < level.GetLevelStrings().Length; y++)
        {
            // Now on each line, we loop on every char of the string in the text, and stop when we reach the width number
            for (int x = 0; x < levelWidth; x++)
            {
                // We convert the whole horizontal line at the height position into char array
                string rowLineText = level.GetLevelStrings()[y];
                char[] chars = rowLineText.ToCharArray();
                // Then we find out what the char/symbol in that position
                char charText = chars[x];
                // Finally we place the prefab based on the char position from the whole text
                PlacePrefab(charText, x, y);
            }
        }
        _levelGo.transform.rotation = Quaternion.Euler(levelRotation);
    }
    private void PlacePrefab(char symbol, int xPosition, int yPosition) 
    {
        level.GetObjectCollection().TryGetValue(symbol, out GameObject obj);
        Debug.Log($"OBJ NULL : {obj}");
        //You might want to leave a space empty...
        if (obj == null) { return; }
        Vector2 position = _startPosition + new Vector3(xPosition, 2 - yPosition);
        GameObject temp = Instantiate(obj, position, Quaternion.identity);
        temp.name = $"OBJECT {xPosition}:{yPosition}";
        temp.transform.SetParent(_levelGo.transform);
    }
}
