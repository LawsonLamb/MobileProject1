﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids.Examples
{
	public class MatchGrid : GridBehaviour<RectPoint>
	{
		public float animationTimePerCell = .1f;

		public Color[] cellColors =
		{
			ExampleUtils.Colors[0],
			ExampleUtils.Colors[1],
			ExampleUtils.Colors[2],
			ExampleUtils.Colors[3]
		};

		public bool useAcceleration = false;

		private RectGrid<MatchCell> matchGrid;

		public override void InitGrid()
		{
			matchGrid = (RectGrid<MatchCell>) Grid.CastValues<MatchCell, RectPoint>();
			matchGrid.Apply(InitCell);
		}

		private void InitCell(MatchCell cell)
		{
			int colorIndex = Random.Range(0, cellColors.Length);

			cell.TileColor = colorIndex; // TileColorExtensions.SelectRandom();
			cell.Color = cellColors[colorIndex];
			cell.IsMoving = false;
		}

		public void OnClick(RectPoint clickedPoint)
		{
			if (matchGrid.Values.Any(c => c == null || c.IsMoving)) //If any cell is moving, ignore input
			{
				return;
			}

			var connectedSet = Algorithms.GetConnectedSet(
				matchGrid,
				clickedPoint,
				CheckMatch);

			if (connectedSet.Count < 3)
			{
				return; // nothing more to do
			}

			DestroyMatchedCells(connectedSet);

			IGrid<int, RectPoint> emptyCellsBelowCount = CountEmptyCellsBelowEachCell();
			StartMovingCells(emptyCellsBelowCount);

			int[] emptyCellsBelowTopCount = CountEmptyCellsBelowTop();
			MakeNewCellsAndStartMovingThem(emptyCellsBelowTopCount);
		}

		private bool CheckMatch(RectPoint p, RectPoint q)
		{
			if (matchGrid[p] == null) return false;
			if (matchGrid[q] == null) return false;

			return matchGrid[p].TileColor == matchGrid[q].TileColor;
		}

		private void DestroyMatchedCells(IEnumerable<RectPoint> connectedSet)
		{
			foreach (var rectPoint in connectedSet)
			{
				var matchCell = matchGrid[rectPoint];

				if (matchCell != null)
				{
					Destroy(matchCell.gameObject);
				}

				matchGrid[rectPoint] = null;
			}
		}

		//Start moving cells that have empty cells below them
		private void StartMovingCells(IGrid<int, RectPoint> emptyCellsBelowCount)
		{
			foreach (var point in emptyCellsBelowCount.WhereCell(c => c > 0).Where(point => matchGrid[point] != null))
			{
				StartCoroutine(MoveCell(point, matchGrid[point], emptyCellsBelowCount[point]));
			}
		}

		private void MakeNewCellsAndStartMovingThem(int[] emptyCellsBelowTopCount)
		{
			for (int columnIndex = 0; columnIndex < matchGrid.Width; columnIndex++)
			{
				for (int i = 0; i < emptyCellsBelowTopCount[columnIndex]; i++)
				{
					var point = new RectPoint(columnIndex, matchGrid.Height + i);
					var newCell = MakeNewCell(point);

					StartCoroutine(MoveCell(point, newCell, emptyCellsBelowTopCount[columnIndex]));
				}
			}
		}

		private int[] CountEmptyCellsBelowTop()
		{
			var topRowEmptyCellsBelowCount = new int[matchGrid.Width];

			for (int columnIndex = 0; columnIndex < matchGrid.Width; columnIndex++)
			{
				var point = new RectPoint(columnIndex, matchGrid.Height);
				var pointBelow = point + RectPoint.South;
				int count = 0;

				while (matchGrid.Contains(pointBelow))
				{
					if (matchGrid[pointBelow] == null)
					{
						count++;
					}

					pointBelow += RectPoint.South;
				}

				topRowEmptyCellsBelowCount[columnIndex] = count;
			}
			return topRowEmptyCellsBelowCount;
		}

		private IGrid<int, RectPoint> CountEmptyCellsBelowEachCell()
		{
			var emptyCellsBelowCount = matchGrid.CloneStructure<int>();

			foreach (var point in matchGrid)
			{
				var pointBelow = point + RectPoint.South;
				int count = 0;

				while (matchGrid.Contains(pointBelow))
				{
					if (matchGrid[pointBelow] == null)
					{
						count++;
					}

					pointBelow += RectPoint.South;
				}

				emptyCellsBelowCount[point] = count;
			}
			return emptyCellsBelowCount;
		}

		private MatchCell MakeNewCell(RectPoint point)
		{
			var newCell = Instantiate(GridBuilder.CellPrefab).GetComponent<MatchCell>();

			newCell.transform.parent = transform;
			newCell.transform.localScale = Vector3.one;
			newCell.transform.localPosition = Map[point];

			InitCell(newCell);

			newCell.name = "-"; //set the name to empty until the cell has been put in the grid

			return newCell;
		}

		private IEnumerator MoveCell(RectPoint start, MatchCell cell, int numberOfCellsToMove)
		{
			return useAcceleration
				? MoveCellWithAcceleration(start, cell, numberOfCellsToMove)
				: MoveCellWithoutAcceleration(start, cell, numberOfCellsToMove);
		}

		private IEnumerator MoveCellWithoutAcceleration(RectPoint start, MatchCell cell, int numberOfCellsToMove)
		{
			cell.IsMoving = true;

			float totalTime = animationTimePerCell*numberOfCellsToMove;
			float time = 0;
			float t = 0;

			RectPoint destination = start + RectPoint.South*numberOfCellsToMove;

			while (t < 1)
			{
				time += Time.deltaTime;
				t = time/totalTime;

				var newPosition = Vector3.Lerp(Map[start], Map[destination], t);
				cell.transform.localPosition = newPosition;

				yield return null;
			}

			cell.transform.localPosition = Map[destination];
			matchGrid[destination] = cell;
			cell.name = destination.ToString(); //This allows us to see what is going on if we render gizmos

			cell.IsMoving = false;
		}

		private IEnumerator MoveCellWithAcceleration(RectPoint start, MatchCell cell, int numberOfCellsToMove)
		{
			cell.IsMoving = true;


			float displacement = 0;
			float t = 0;
			float speed = 0;
			const float acceleration = 10000;

			RectPoint destination = start + RectPoint.South*numberOfCellsToMove;
			float totalDisplacement = (Map[destination] - Map[start]).magnitude;

			while (t < 1)
			{
				speed += Time.deltaTime*acceleration;
				displacement += Time.deltaTime*speed;
				t = displacement/totalDisplacement;

				var newPosition = Map[start] + Vector3.down*displacement;
				cell.transform.localPosition = newPosition;

				yield return null;
			}

			cell.transform.localPosition = Map[destination];
			matchGrid[destination] = cell;
			cell.name = destination.ToString(); //This allows us to see what
			//is going on if we render gizmos

			cell.IsMoving = false;
		}
	}
}