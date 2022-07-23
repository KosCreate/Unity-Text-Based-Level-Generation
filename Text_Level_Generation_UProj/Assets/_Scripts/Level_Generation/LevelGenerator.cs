using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private Vector3 startPosition;
    private GameObject levelGO;
    private void Start() => Initialization(); 
    
    private void Initialization() {
        level.Initialization();
        levelGO = new GameObject();
        levelGO.name = "Level";
        startPosition = transform.position;
        // Start the loop on the third line since the first two is for the width and height number
        for (int y = 2; y < level.GetLevelStrings().Length; y++)
        {
            // Now on each line, we loop on every char of the string in the text, and stop when we reach the width number
            for (int x = 0; x < level.GetWidthAndHeight().Item1; x++)
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
    }
    private void PlacePrefab(char symbol, int xPosition, int yPosition)
    {
            GameObject obj;
            level.GetObjectCollection().TryGetValue(symbol, out obj);
            // Here we offset the xPosition and yPosition against the start position. 
            // We add 2 to the Y position because of the width and height number. 
            // And we deduct the Y position here because the level is generated from the top left, 
            // so we are going from top to down for the level horizontal line.
            Vector2 position = startPosition + new Vector3(xPosition, 2 - yPosition);
            GameObject temp = Instantiate(obj, position, Quaternion.identity);
            temp.name = $"OBJECT {xPosition}:{yPosition}"; 
            temp.transform.SetParent(levelGO.transform);
    }
}
