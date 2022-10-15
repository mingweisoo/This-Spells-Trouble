using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell{
    nullSpell = -1,
	fireball = 0,
	teleport = 1,
    lightning = 2,
    tornado = 3,
    rush = 4,
    arc = 5,
    splitter = 6,
    boomerang = 7,
    laser = 8,
    cloud = 9,
    minethrow = 10,
    groundattack = 11,
    iceattack = 12,
    shockwave = 13,
    wall = 14

}

[CreateAssetMenu(fileName = "PlayersSpells", menuName = "ScriptableObjects/PlayersSpells", order = 2)]
public class PlayersSpells : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "Players' chosen spells. -1 indicates player has not tied a spell to that slot.";
#endif

    private int[,] _arr = new int[4, 4] { {-1, -1, -1, -1}, 
                                            {-1, -1, -1, -1}, 
                                            {-1, -1, -1, -1}, 
                                            {-1, -1, -1, -1} };

    public Spell GetSpell(int playerID, int slot) {
        int spellInt = _arr[playerID, slot];
        return (Spell) spellInt;
    }

    public void SetSpell(int playerID, int slot, Spell spell) {
        int spellInt = (int) spell;
       _arr[playerID, slot] = spellInt;
    }

    // overload
    public void SetSpell(PlayersSpells arr) {
        _arr = arr._arr;
    }

    public int GetNumSpells() {
        int numSpells = System.Enum.GetValues(typeof(Spell)).Length - 1;
        return numSpells;
    }
}
