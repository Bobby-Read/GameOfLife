﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
	class LifeModel
	{
		public int MaxX, MaxY;
		public BaseCell[,] Grid;

		public LifeModel(int x, int y)
		{
			MaxX = x;
			MaxY = y;

			Grid = new BaseCell[x, y];

			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					Grid[i, j] = ChooseCell();
				}
			}

			for (int i = 0; i < x; i++)
			{
				Grid[0, i] = new OffCell();
				Grid[i, 0] = new OffCell();
				Grid[x - 1, i] = new OffCell();
				Grid[i, y - 1] = new OffCell();
			}
		}

		private BaseCell ChooseCell()
		{
			Random r = new Random();
			Type[] types = new[] { typeof(OffCell), typeof(OnCell) };//, typeof(SlowPlague) };

			var type = types[r.Next(0, types.Length)];

			return (BaseCell)Activator.CreateInstance(type);
		}

		public void StepForward()
		{
			var oldGrid = (BaseCell[,]) Grid.Clone();

			for (int i = 1; i < MaxX - 1; i++)
			{
				for (int j = 1; j < MaxY - 1; j++)
				{
					Grid[i, j].StepForward(i, j, oldGrid, Grid);
				}
			}
		}
	}
}
