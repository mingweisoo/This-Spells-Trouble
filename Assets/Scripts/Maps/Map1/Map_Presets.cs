using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Map_Presets : ScriptableObject
{
    [System.Serializable]
    public class serializableClass_outer {
        public List<tile_data> tiles;
    }

    [System.Serializable]
    public class tile_data  {
        public int x;
        public int y;
    }
    
    public int shrinkTime;
    public List<serializableClass_outer> map_preset = new List<serializableClass_outer>();

}
