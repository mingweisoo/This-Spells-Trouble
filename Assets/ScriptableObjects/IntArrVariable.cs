using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntArrVariable", menuName = "ScriptableObjects/IntArrVariable", order = 2)]
public class IntArrVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    private int[] _arr = {-1, -1, -1, -1};

    public int GetValue(int index) {
        return _arr[index];
    }

    public void SetValue(int index, int value) {
        _arr[index] = value;
    }

    // overload
    public void SetValue(IntArrVariable arr) {
        _arr = arr._arr;
    }

    public int GetLength() {
        return _arr.Length;
    }
}
