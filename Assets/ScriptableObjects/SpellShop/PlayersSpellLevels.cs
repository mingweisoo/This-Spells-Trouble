using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersSpellLevels", menuName = "ScriptableObjects/PlayersSpellLevels", order = 3)]
public class PlayersSpellLevels : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "Players' spell levels. -1 indicates player has not bought that spell.";
#endif

    private int[,] _arr = new int[4, System.Enum.GetValues(typeof(Spell)).Length - 1];

    public int GetSpellLevel(int playerID, Spell spell) {
        int spellInt = (int) spell;
        int spellLevel;
        if (spellInt < 0) {
            spellLevel = -1;
        } else {
            spellLevel = _arr[playerID, spellInt];
        }
        return spellLevel;
    }

    public void SetSpellLevel(int playerID, Spell spell, int spellLevel) {
        int spellInt = (int) spell;
       _arr[playerID, spellInt] = spellLevel;
    }

    // overload
    public void SetValue(PlayersSpellLevels arr) {
        _arr = arr._arr;
    }

}
