using System;
using System.Linq;

namespace MineField.Lib
{
    public class MineFieldProcessor : IMineFieldProcessor
    {
        public char[,] GetHintField(char[,] mineField)
        {
            ValidateMineField(mineField);

            var hintField = (char[,])mineField.Clone();

            ForEachNotMinedCell(mineField, (x, y) =>
            {
                int mines = CountMinesInOneCellRadius(mineField, x, y);

                hintField[x, y] = GetFirstCharDigit(mines);
            });

            return hintField;
        }

        static void ForEachNotMinedCell(char[,] mineField, Action<int, int> act)
        {
            for (int x = 0; x < mineField.GetLength(0); x++)
            {
                for (int y = 0; y < mineField.GetLength(1); y++)
                {
                    if (!IsMine(mineField[x, y]))
                        act(x, y);
                }
            }
        }

        static int CountMinesInOneCellRadius(char[,] mineField, int centralX, int centralY)
        {
            int GetLeftBoundary(int dimValue) => 0 > dimValue - 1 ? 0 : dimValue - 1;
            int GetRightBoundary(int dimValue, int dimLength) => dimLength - 1 < dimValue + 1 ? dimLength - 1 : dimValue + 1;

            int mines = 0;

            for (int x = GetLeftBoundary(centralX); x <= GetRightBoundary(centralX, mineField.GetLength(0)); x++)
            {
                for (int y = GetLeftBoundary(centralY); y <= GetRightBoundary(centralY, mineField.GetLength(1)); y++)
                {
                    if (IsMine(mineField[x, y]))
                        mines++;
                }
            }

            return mines;
        }

        static void ValidateMineField(char[,] mineField)
        {
            if (mineField is null)
                throw new ArgumentNullException(nameof(mineField));

            if (mineField.Length == 0)
                throw new ArgumentException($"{nameof(mineField)} is empty");

            foreach (var cell in mineField)
            {
                if (!IsMine(cell) && !IsEmptyCell(cell))
                    throw new ArgumentException($"In {nameof(mineField)} cell {cell} has invalid value");
            }
        }

        static bool IsMine(char cell)
        {
            return string.Compare(cell.ToString(), "*", StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        static bool IsEmptyCell(char cell)
        {
            return string.Compare(cell.ToString(), ".", StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        static char GetFirstCharDigit(int number)
        {
            return number.ToString().First();
        }
    }
}
