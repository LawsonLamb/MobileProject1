using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Grid : MonoBehaviour {
	

	
	private GameObject[,] cells = new GameObject[5,5];

	public GameObject this[int row, int column]
	{
		get
		{
			try
			{
				return cells[row, column];
			}
			catch (Exception ex)
			{
				
				throw;
			}
		}
		set
		{
			cells[row, column] = value;
		}
	}

	public void Swap(GameObject g1, GameObject g2)
	{
		//hold a backup in case no match is produced
		backupG1 = g1;
		backupG2 = g2;
		
		var g1Shape = g1.GetComponent<Cell>();
		var g2Shape = g2.GetComponent<Cell>();
		
		//get array indexes
		int g1Row = g1Shape.Row;
		int g1Column = g1Shape.Column;
		int g2Row = g2Shape.Row;
		int g2Column = g2Shape.Column;
		
		//swap them in the array
		var temp = cells[g1Row, g1Column];
		cells[g1Row, g1Column] = cells[g2Row, g2Column];
		cells[g2Row, g2Column] = temp;
		
		//swap their respective properties
		//Cell.SwapColumnRow(g1Shape, g2Shape);
		
	}
	


	public void UndoSwap()
	{
		if (backupG1 == null || backupG2 == null)
			throw new Exception("Backup is null");
		
		Swap(backupG1, backupG2);
	}
	
	private GameObject backupG1;
	private GameObject backupG2;
	
	
	
	
	/// <summary>
	/// Returns the matches found for a list of GameObjects
	/// MatchesInfo class is not used as this method is called on subsequent collapses/checks, 
	/// not the one inflicted by user's drag
	/// </summary>
	/// <param name="gos"></param>
	/// <returns></returns>
	/// 
	/*
	public IEnumerable<GameObject> GetMatches(IEnumerable<GameObject> gos)
	{
		List<GameObject> matches = new List<GameObject>();
		foreach (var go in gos)
		{
			matches.AddRange(GetMatches(go).MatchedCandy);
		}
		return matches.Distinct();
	}

	/// <summary>
	/// Returns the matches found for a single GameObject
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public MatchesInfo GetMatches(GameObject go)
	{
		MatchesInfo matchesInfo = new MatchesInfo();

		var horizontalMatches = GetMatchesHorizontally(go);
		if (ContainsDestroyRowColumnBonus(horizontalMatches))
		{
			horizontalMatches = GetEntireRow(go);
			if (!BonusTypeUtilities.ContainsDestroyWholeRowColumn(matchesInfo.BonusesContained))
				matchesInfo.BonusesContained |= BonusType.DestroyWholeRowColumn;
		}
		matchesInfo.AddObjectRange(horizontalMatches);

		var verticalMatches = GetMatchesVertically(go);
		if (ContainsDestroyRowColumnBonus(verticalMatches))
		{
			verticalMatches = GetEntireColumn(go);
			if (!BonusTypeUtilities.ContainsDestroyWholeRowColumn(matchesInfo.BonusesContained))
				matchesInfo.BonusesContained |= BonusType.DestroyWholeRowColumn;
		}
		matchesInfo.AddObjectRange(verticalMatches);

		return matchesInfo;
	}

	private bool ContainsDestroyRowColumnBonus(IEnumerable<GameObject> matches)
	{
		if (matches.Count() >= Constants.MinimumMatches)
		{
			foreach (var go in matches)
			{
				if (BonusTypeUtilities.ContainsDestroyWholeRowColumn
					(go.GetComponent<Shape>().Bonus))
					return true;
			}
		}

		return false;
	}

	private IEnumerable<GameObject> GetEntireRow(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		int row = go.GetComponent<Shape>().Row;
		for (int column = 0; column < Constants.Columns; column++)
		{
			matches.Add(cells[row, column]);
		}
		return matches;
	}

	private IEnumerable<GameObject> GetEntireColumn(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		int column = go.GetComponent<Shape>().Column;
		for (int row = 0; row < Constants.Rows; row++)
		{
			matches.Add(cells[row, column]);
		}
		return matches;
	}

	/// <summary>
	/// Searches horizontally for matches
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	private IEnumerable<GameObject> GetMatchesHorizontally(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var shape = go.GetComponent<Shape>();
		//check left
		if (shape.Column != 0)
			for (int column = shape.Column - 1; column >= 0; column--)
			{
				if (cells[shape.Row, column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(cells[shape.Row, column]);
				}
				else
					break;
			}

		//check right
		if (shape.Column != Constants.Columns - 1)
			for (int column = shape.Column + 1; column < Constants.Columns; column++)
			{
				if (cells[shape.Row, column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(cells[shape.Row, column]);
				}
				else
					break;
			}

		//we want more than three matches
		if (matches.Count < Constants.MinimumMatches)
			matches.Clear();

		return matches.Distinct();
	}

	/// <summary>
	/// Searches vertically for matches
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	private IEnumerable<GameObject> GetMatchesVertically(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var shape = go.GetComponent<Shape>();
		//check bottom
		if (shape.Row != 0)
			for (int row = shape.Row - 1; row >= 0; row--)
			{
				if (cells[row, shape.Column] != null &&
					cells[row, shape.Column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(cells[row, shape.Column]);
				}
				else
					break;
			}

		//check top
		if (shape.Row != Constants.Rows - 1)
			for (int row = shape.Row + 1; row < Constants.Rows; row++)
			{
				if (cells[row, shape.Column] != null && 
					cells[row, shape.Column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(cells[row, shape.Column]);
				}
				else
					break;
			}


		if (matches.Count < Constants.MinimumMatches)
			matches.Clear();

		return matches.Distinct();
	}

	/// <summary>
	/// Removes (sets as null) an item from the array
	/// </summary>
	/// <param name="item"></param>
	public void Remove(GameObject item)
	{
		cells[item.GetComponent<Shape>().Row, item.GetComponent<Shape>().Column] = null;
	}

	/// <summary>
	/// Collapses the array on the specific columns, after checking for empty items on them
	/// </summary>
	/// <param name="columns"></param>
	/// <returns>Info about the GameObjects that were moved</returns>
	public AlteredCandyInfo Collapse(IEnumerable<int> columns)
	{
		AlteredCandyInfo collapseInfo = new AlteredCandyInfo();


		///search in every column
		foreach (var column in columns)
		{
			//begin from bottom row
			for (int row = 0; row < Constants.Rows - 1; row++)
			{
				//if you find a null item
				if (cells[row, column] == null)
				{
					//start searching for the first non-null
					for (int row2 = row + 1; row2 < Constants.Rows; row2++)
					{
						//if you find one, bring it down (i.e. replace it with the null you found)
						if (cells[row2, column] != null)
						{
							cells[row, column] = cells[row2, column];
							cells[row2, column] = null;

							//calculate the biggest distance
							if (row2 - row > collapseInfo.MaxDistance) 
								collapseInfo.MaxDistance = row2 - row;

							//assign new row and column (name does not change)
							cells[row, column].GetComponent<Shape>().Row = row;
							cells[row, column].GetComponent<Shape>().Column = column;

							collapseInfo.AddCandy(cells[row, column]);
							break;
						}
					}
				}
			}
		}

		return collapseInfo;
	}

	/// <summary>
	/// Searches the specific column and returns info about null items
	/// </summary>
	/// <param name="column"></param>
	/// <returns></returns>
	public IEnumerable<ShapeInfo> GetEmptyItemsOnColumn(int column)
	{
		List<ShapeInfo> emptyItems = new List<ShapeInfo>();
		for (int row = 0; row < Constants.Rows; row++)
		{
			if (cells[row, column] == null)
				emptyItems.Add(new ShapeInfo() { Row = row, Column = column });
		}
		return emptyItems;
	}
	*/
}
