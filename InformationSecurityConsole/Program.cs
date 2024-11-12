using System;
using System.Linq;
using System.Security.Policy;

public class MatrixPermutation
{
    public static void PermuteMatrix(char[,] matrix, string rowKey, string colKey)
    {
        if (matrix == null || rowKey == null || colKey == null)
        {
            throw new ArgumentNullException();
        }

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        // Преобразуем ключи в массивы индексов
        int[] rowPermutation = rowKey.Select((c, i) => new { c, i }).OrderBy(x => x.c).Select(x => x.i).ToArray();
        int[] colPermutation = colKey.Select((c, i) => new { c, i }).OrderBy(x => x.c).Select(x => x.i).ToArray();

        // Создаем временную матрицу для хранения переставленных данных
        char[,] tempMatrix = new char[rows, cols];

        // Переставляем строки
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                tempMatrix[rowPermutation[i], j] = matrix[i, j];
            }
        }

        // Переставляем столбцы
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, colPermutation[j]] = tempMatrix[i, j];
            }
        }
    }

    public static void ReversePermuteMatrix(char[,] matrix, string rowKey, string colKey)
    {
        if (matrix == null || rowKey == null || colKey == null)
        {
            throw new ArgumentNullException();
        }

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        // Преобразуем ключи в массивы индексов
        int[] rowPermutation = rowKey.Select((c, i) => new { c, i }).OrderBy(x => x.c).Select(x => x.i).ToArray();
        int[] colPermutation = colKey.Select((c, i) => new { c, i }).OrderBy(x => x.c).Select(x => x.i).ToArray();

        // Создаем временную матрицу для хранения переставленных данных
        char[,] tempMatrix = new char[rows, cols];

        // Обратная перестановка столбцов
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                tempMatrix[i, j] = matrix[i, colPermutation[j]];
            }
        }

        // Обратная перестановка строк
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[rowPermutation[i], j] = tempMatrix[i, j];
            }
        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Зашифровать матрицу");
            Console.WriteLine("2. Расшифровать матрицу");
            Console.Write("Введите номер действия: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1 || choice == 2)
            {
                Console.WriteLine("Введите размер матрицы (строки столбцы):");
                string[] dimensions = Console.ReadLine().Split(' ');
                int rows = int.Parse(dimensions[0]);
                int cols = int.Parse(dimensions[1]);

                char[,] matrix = new char[rows, cols];
                Console.WriteLine("Введите элементы матрицы (по строкам):");
                for (int i = 0; i < rows; i++)
                {
                    string row = Console.ReadLine();
                    if (row.Length < cols)
                    {
                        Console.WriteLine("Недостаточно символов для заполнения строки. Добавьте пробелы.");
                        continue; // Продолжаем цикл ввода матрицы
                    }

                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = row[j];
                    }
                }

                Console.WriteLine("Введите ключ для перестановки строк:");
                string rowKey = Console.ReadLine();
                if (rowKey.Length != rows)
                {
                    Console.WriteLine("Длина ключа для перестановки строк должна быть равна количеству строк матрицы.");
                    continue; // Продолжаем цикл ввода матрицы
                }

                Console.WriteLine("Введите ключ для перестановки столбцов:");
                string colKey = Console.ReadLine();
                if (colKey.Length != cols)
                {
                    Console.WriteLine("Длина ключа для перестановки столбцов должна быть равна количеству столбцов матрицы.");
                    continue; // Продолжаем цикл ввода матрицы
                }

                Console.WriteLine("\nИсходная матрица:");
                PrintMatrix(matrix); // Вывод исходной матрицы

                if (choice == 1)
                {
                    PermuteMatrix(matrix, rowKey, colKey);
                    Console.WriteLine("\nЗашифрованная матрица:");
                    PrintMatrix(matrix);
                }
                else if (choice == 2)
                {
                    ReversePermuteMatrix(matrix, rowKey, colKey);
                    Console.WriteLine("\nРасшифрованная матрица:");
                    PrintMatrix(matrix);
                }

                // После завершения операции шифрования/расшифровки
                // предлагаем пользователю продолжить или выйти
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Продолжить работу");
                Console.WriteLine("2. Выйти из программы");
                Console.Write("Введите номер действия: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (choice == 2)
                {
                    Console.WriteLine("Выход из программы.");
                    break; // Выходим из цикла while
                }
            }
            else
            {
                Console.WriteLine("Некорректный выбор действия.");
            }
        }
    }

    public static void PrintMatrix(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

