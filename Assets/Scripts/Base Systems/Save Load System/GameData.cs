using System.Collections.Generic;


[System.Serializable]
public class GameData 
{
    public int gameLevel;
    public List<int> SceneAvailablePool;
    public List<int> SceneUtiliziedPool;
    
    public GameData()
    {
        gameLevel = 1;
        SceneAvailablePool = new List<int>();
        SceneUtiliziedPool = new List<int>();
    }
}
