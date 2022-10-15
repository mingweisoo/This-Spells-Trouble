using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName =  "PlayerInputsArr", menuName =  "ScriptableObjects/PlayerInputsArr", order =  1)]
public class PlayerInputsArr : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    private PlayerInput[] _arr = {null, null, null, null};

    public PlayerInput GetValue(int index) {
        return _arr[index];
    }

    public void SetValue(int index, PlayerInput value)
    {
        _arr[index] = value;
    }

    // overload
    public void SetValue(PlayerInputsArr arr)
    {
        _arr = arr._arr;
    }

    public int GetLength() {
        return _arr.Length;
    }

    public int GetNumPlayers() {
        int numPlayers = 0;
        for (int i = 0; i < _arr.Length; i++) {
            if (_arr[i] != null) {
                numPlayers += 1;
            }
        }
        return numPlayers;
    }
}
