using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesRandomizer : MonoBehaviour
{
    [SerializeField] private int scenesNonRepeatCount = 1;
    [SerializeField] private int maxSceneCount;
    [SerializeField] private List<int> scenePool;
    [SerializeField] private List<int> sceneUtiliziedPool;

    private void Awake()
    {
        maxSceneCount = SceneManager.sceneCountInBuildSettings - 1;
        scenesNonRepeatCount = scenesNonRepeatCount >= maxSceneCount ? maxSceneCount - 1 : scenesNonRepeatCount;

        for (int i = 1; i < maxSceneCount+1; i++)
        {
            scenePool.Add(i);
        }
    }

    public int GetRandomSceneIndex()
    {
       return GetRandomValue<int>(ref scenePool, ref sceneUtiliziedPool, scenesNonRepeatCount);
    }

    private T GetRandomValue<T>(ref List<T> actual, ref List<T> used, int nonRepeatCount)
    {
        T returnedValue;

        if (actual.Count == 0)
        {
            if (used.Count > 0)
            {
                actual.Add(used[0]);
                used.Remove(used[0]);
            }
            else
            {
                Debug.LogError($"It's impossible to get a random value if the list of values is empty");
            }
        }
        
        int returnedValueIndex = Random.Range(0, actual.Count);
        returnedValue = actual[returnedValueIndex];

        if (used.Count > nonRepeatCount)
        {
            actual.Add(used[0]);
            used.Remove(used[0]);
        }
        used.Add(returnedValue);
        actual.Remove(returnedValue);
        return returnedValue;
    }


}
