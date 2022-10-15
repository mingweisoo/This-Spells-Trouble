using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackArr", menuName = "ScriptableObjects/KnockbackArr", order = 2)]
public class KnockbackArr : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "Players' knockback percentage. -1 indicates player has not joined the game.";
#endif

    private float[] _arr = {-1, -1, -1, -1};

    public float GetValue(int index) {
        return _arr[index];
    }

    public void SetValue(int index, float value) {
        if (value > 100) {
            _arr[index] = 100;
        } else if (value < 0) {
            _arr[index] = 0;
        } else {
            _arr[index] = value;
        }
    }

    // overload
    public void SetValue(KnockbackArr arr) {
        _arr = arr._arr;
    }

    public void ApplyChange(int index, float value) {
        float temp = _arr[index] + value;
        if (temp > 300) {
            _arr[index] = 300;
        } else if (temp < 0) {
            _arr[index] = 0;
        } else {
            _arr[index] = temp;
        }
    }

    public int GetLength() {
        return _arr.Length;
    }
}
