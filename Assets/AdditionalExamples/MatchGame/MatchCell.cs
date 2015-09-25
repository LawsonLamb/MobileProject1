﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gamelogic.Grids.Examples
{
//We use this enum to be able to make comparisons for matches.
	public enum TileColor
	{
		Blue,
		Green,
		Yellow,
		Red
	}

	public static class TileColorExtensions
	{
		public static Color Color(this TileColor tileColor)
		{
			switch (tileColor)
			{
				case TileColor.Blue:
					return ExampleUtils.Colors[0];
				case TileColor.Green:
					return ExampleUtils.Colors[1];
				case TileColor.Yellow:
					return ExampleUtils.Colors[2];
				case TileColor.Red:
					return ExampleUtils.Colors[3];
				default:
					throw new ArgumentOutOfRangeException("tileColor");
			}
		}

		public static TileColor SelectRandom()
		{
			int tileIndex = Random.Range(0, 4);

			return (TileColor) tileIndex;
		}
	}

	public class MatchCell : SpriteCell
	{
		public int TileColor { get; set; }

		//We use this to block input while the cell is moving
		public bool IsMoving { get; set; }
	}
}