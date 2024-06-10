using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectMap", menuName = "ScriptableMap", order = 1)]
public class ScriptableObjectMap : ScriptableObject
{
	[ContextMenuItem("Fill", "Fill")]
	public const int MAP_SIZE = 10;

	[HideInInspector]
	public int[] ground = new int[MAP_SIZE * MAP_SIZE];
	public MapObject[] mapObj;
	public MapEnemies[] mapEnemies;

	public Vector2 playerPos;

	public TypeRot playerRot;

	public enum TypeRot
	{
        up = 0,
        right = 270,
        left = 90,
        down = 180,
    }

	[Serializable]
	public class MapObject
	{
		public Vector2 pos;
		public Type type;
		public TypeRot rot;

        public enum TypeRot
        {
            up = 0,
            right = 270,
            left = 90,
            down = 180,
        }

        public enum Type
		{
			None = 0,
			Rock0 = 1,
			Rock1 = 2,
			Rock2 = 3,
			Rock3 = 4,
			Rock4 = 5,
			Rock5 = 6,
			Rock6 = 7,
			LandMine = 8,
		}
	}

	[Serializable]
	public class MapEnemies
	{
		public Vector2 pos;
		
		

		public Type type;
		public RotationType rotType;

		public enum Type
		{
			None = 0,
			BasicTank = 1,
			OrbTank = 2,
			SowerTank = 3,
		}

		public enum RotationType
		{
			up = 0,
			right = 270,
			left = 90,
			down = 180,
		}
	}

	public void Fill()
	{
		for (int i = 0; i < ground.Length; i++)
		{
			ground[i] = 1;
		}
	}

	
}
