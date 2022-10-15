using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolArrVariable", menuName = "ScriptableObjects/BoolArrVariable", order = 2)]
public class BoolArrVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    private bool[] _arr = {false, false, false, false};
    
    // public bool[] Arr{
    //     get{
    //         return _arr;
    //     }
    // }

    public bool GetValue(int index) {
        return _arr[index];
    }

    public void SetValue(int index, bool value)
    {
        _arr[index] = value;
    }

    // overload
    public void SetValue(BoolArrVariable arr)
    {
        _arr = arr._arr;
    }

    public int GetLength() {
        return _arr.Length;
    }

    public int GetNumTrue() {
        int numTrue = 0;
        for (int i = 0; i < _arr.Length; i++) {
            if (_arr[i] == true) {
                numTrue += 1;
            }
        }
        return numTrue;
    }
}
