using System;
using System.IO;

namespace calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет!Это консольное приложение - калькулятор матриц.\n" +
                "Мой калькулятор работает с матрицами размер которых не превышает 7Х7\n" +
                "Калькулятор поддерживает ввод из файла.\n" +
                "Требования к файлу:\n" +
               @"Файл должен находится на пути C:\temp\matrix.txt" +
               "\nПустые строки в файле не допускаются." +
                "\nВ каждой строке матрицы элементы должны разделяться пробелами.\n" +
                "Если файла по указанному пути не существует или данные в нем не корректны,\n" +
                "программа выводит сообщение об ошибке и завершает выполнение.\n");
            // "Игровой"цикл.
            while (true)
            {
                Console.WriteLine("Вы хотите ввести матрицу из файла? Введите yes или no");
                while (true)
                {
                    var answer = Console.ReadLine();
                    if (answer == "no")
                    {
                        int countOfStrings = UserInputOfStrings();
                        int countOfColumns = UserInputOfColumn();
                        double[,] matrix = UserInputTypeOfFillings(countOfStrings, countOfColumns);
                        DoOperation(matrix, countOfStrings, countOfColumns);
                        break;
                    }
                    else if (answer == "yes")
                    {
                        FileFilling();
                        break;
                    }
                    else Console.WriteLine("Ваш ответ не корректен.Введите yes или no");
                }
            }
        }
        /// <summary>
        /// Запускает выбранную пользователем операцию с матрицей.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>

        static void DoOperation(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("\nВыберите операцию,которую хотите совершить \n" +
                        "Введите 1 если хотите найти след матрицы\n" +
                        "Введите 2 если хотите транспонировать матрицу\n" +
                        "Введите 3 если хотите сложить 2 матрицы\n" +
                        "Введите 4 если хотите вычислить разность 2-х матриц\n" +
                        "Введите 5 если хотите вычислить произведение 2-х матриц\n" +
                        "Введите 6 если хотите умножить матрицу на число\n" +
                        "Введите 7 если хотите найти определитель матрицы");
            while (true)
            {
                var numberOfOperation = Console.ReadLine();
                if (numberOfOperation == "1")
                {
                    Operation1(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else if (numberOfOperation == "2")
                {
                    Operation2(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else if (numberOfOperation == "3")
                {
                    Operation3(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else if (numberOfOperation == "4")
                {
                    Operation4(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else if (numberOfOperation == "5")
                {
                    Operation5(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else if (numberOfOperation == "6")
                {
                    Operation6(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else if (numberOfOperation == "7")
                {
                    Operation7(matrix, countOfStrings, countOfColumns);
                    break;
                }
                else Console.WriteLine("Вы ввели некорректное значение.Пожалуйста введите номер операции от 1 до 7");
            }
        }
        /// <summary>
        /// Совершает первую операцию(нахождение следа матрицы).
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation1(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            if (countOfStrings == countOfColumns)
            {
                Console.WriteLine("Исходная матрица:");
                BeatifulOutput(matrix);
                Console.WriteLine("Матричный след: {0}", FindMatrixTrace(matrix, countOfStrings, countOfColumns));
                if (End()) Environment.Exit(0);

            }
            else Console.WriteLine("Вы не можете произвести эту операцию с неквадратной матрицей.Пожалуйста введите квадратную матрицу");
        }
        /// <summary>
        /// Совершает вторую операцию(транспонирование матрицы).
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation2(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("Исходная матрица:");
            BeatifulOutput(matrix);
            Console.WriteLine("Транспонированая матрица:");
            BeatifulOutput(TranspositionMatrix(matrix, countOfStrings, countOfColumns));
            if (End()) Environment.Exit(0);
        }
        /// <summary>
        /// Совершает третью операцию(сумма двух матриц).
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation3(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("Для операции сложения нужна вторая матрица такого же размера");
            var matrix2 = UserInputTypeOfFillings(countOfStrings, countOfColumns);
            Console.WriteLine("Первая матрица:");
            BeatifulOutput(matrix);
            Console.WriteLine("Вторая матрица:");
            BeatifulOutput(matrix2);
            Console.WriteLine("Их сумма: ");
            BeatifulOutput(SumOfMatrices(matrix, matrix2, countOfStrings, countOfColumns));
            if (End()) Environment.Exit(0);
        }
        /// <summary>
        /// Совершает четвертую операцию(разность двух матриц).
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation4(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("Для операции вычитания нужна вторая матрица такого же размера");
            var matrix2 = UserInputTypeOfFillings(countOfStrings, countOfColumns);
            Console.WriteLine("Первая матрица:");
            BeatifulOutput(matrix);
            Console.WriteLine("Вторая матрица:");
            BeatifulOutput(matrix2);
            Console.WriteLine("Их разность: ");
            BeatifulOutput(DifferenceOfMatrices(matrix, matrix2, countOfStrings, countOfColumns));
            if (End()) Environment.Exit(0);
        }
        /// <summary>
        /// Совершает пятую операцию(произведение двух матриц).
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation5(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("Для операции умножения нужна вторая матрица с количеством строк равным количеству столбцов первой матрицы");
            var countOfColumns2 = UserInputOfColumn();
            var matrix2 = UserInputTypeOfFillings(countOfColumns, countOfColumns2);
            Console.WriteLine("Первая матрица:");
            BeatifulOutput(matrix);
            Console.WriteLine("Вторая матрица:");
            BeatifulOutput(matrix2);
            Console.WriteLine("Их произведение: ");
            BeatifulOutput(MultiplicationOfMatrices(matrix, matrix2, countOfStrings, countOfColumns2));
            if (End()) Environment.Exit(0);
        }
        /// <summary>
        /// Совершает шестую операцию(произведение матрицы на число).
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation6(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("Для этой операции необходимо ввести число на которое будем умножать матрицу.");
            double number;
            while (true)
            {
                var sNumber = Console.ReadLine();
                if (double.TryParse(sNumber, out number)) break;
                else Console.WriteLine("Вы ввели некорректное значение.Пожалуйста,введите вещественное число");
            }
            Console.WriteLine("Исходная матрица:");
            BeatifulOutput(matrix);
            Console.WriteLine("Матрица умноженная на число: {0}", number);
            BeatifulOutput(MultiplicationOfMatriceByNumber(matrix, number, countOfStrings, countOfColumns));
            if (End()) Environment.Exit(0);
        }
        /// <summary>
        /// Совершает седьмую операцию(нахождение определителя матрицы).
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        static void Operation7(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            if (countOfStrings == countOfColumns)
            {
                Console.WriteLine("Исходная матрица:");
                BeatifulOutput(matrix);
                Console.WriteLine("Определитель матрицы = {0}", FindDeterminantOfMatrix(matrix));
                if (End()) Environment.Exit(0);
            }
            else Console.WriteLine("Вы не можете произвести эту операцию с неквадратной матрицей.Пожалуйста введите квадратную матрицу");
        }
        /// <summary>
        /// Узнает у пользователя хочет ли он закончить игру.
        /// </summary>
        /// <returns>False при вводе пользователем exit, True при любом другом вводе</returns>
        static bool End()
        {
            Console.WriteLine("\nВы хотите произвести еще какую-либо операцию? Если нет введите exit ");
            var answer = Console.ReadLine();
            if (answer == "exit") return true;
            else
            {
                Console.Clear();
                return false;
            }
        }
        /// <summary>
        /// Принятие от пользователя количество строк в матрице.
        /// </summary>
        /// <returns>Количество строк в матрице</returns>
        static int UserInputOfStrings()
        {
            Console.Write("\nВведите количество строк в матрице: ");
            int countOfStrings;
            while (true)
            {
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out countOfStrings) && (countOfStrings >= 1) && (countOfStrings <= 7))
                {
                    break;
                }
                else
                {
                    Console.Write("Вы ввели не корректное значение.Пожалуйста введите целое число строк в диапозоне от 1 до 7: ");
                }
            }
            return countOfStrings;
        }
        /// <summary>
        /// Принятие от пользователя количество столбцов в матрице.
        /// </summary>
        /// <returns>Количество столбцов в матрице</returns>
        static int UserInputOfColumn()
        {
            Console.Write("Введите количество столбцов в матрице: ");
            int countOfColumns;
            while (true)
            {
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out countOfColumns) && (countOfColumns >= 1) && (countOfColumns <= 7))
                {
                    break;
                }
                else
                {
                    Console.Write("Вы ввели не корректное значение.Пожалуйста введите целое число столбцов в диапозоне от 1 до 7: ");
                }
            }
            return countOfColumns;
        }
        /// <summary>
        /// Заполнение матрицы таким способом, который введет пользователь(random simple,random hard или console).
        /// </summary>
        /// <param name="countOfStrings">Количество строк в матрице</param>
        /// <param name="countOfColumns">Количество столбцов в матрице </param>
        /// <returns>Матрица</returns>
        static double[,] UserInputTypeOfFillings(int countOfStrings, int countOfColumns)
        {
            Console.WriteLine("\nКаким образом вы хотите заполнить матрицу?\n" +
                "Введите сonsole для ручного ввода,\n" +
                "random simple для случайной генерации матрицы с элементами от 0 до 9,\n" +
                "random hard для случайной генерации матрицы с любыми вещественными элементами");
            double[,] matrix;
            while (true)
            {
                var userInput = Console.ReadLine();
                if (userInput == "random hard")
                {
                    matrix = RandomHardFilling(countOfStrings, countOfColumns);
                    break;
                }
                else if (userInput == "random simple")
                {
                    matrix = RandomSimpleFilling(countOfStrings, countOfColumns);
                    break;
                }
                else if (userInput == "console")
                {
                    matrix = ConsoleFilling(countOfStrings, countOfColumns);
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректное значение. Пожалуйста, введите console, random simple или random hard");
                }
            }
            return matrix;
        }

        /// <summary>
        /// Получение матрицы из файла и проведение с ней выбранной пользователем операцией.
        /// </summary>
        static void FileFilling()
        {
            string path = @"C:\temp\matrix.txt";
            int countOfStrings = 0;
            int countOfColumns = 0;
            //выдаст сообщение об ошибке если по указанному пути файла не существует
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    // В цикле считается количество строк и столбцов матрицы.
                    while ((line = sr.ReadLine()) != null)
                    {
                        countOfStrings++;
                        countOfColumns = GetCountOfColumnsFile(line);
                    }
                }
            }
            catch
            {
                Console.WriteLine("!Файл не найден!");
                Environment.Exit(0);
            }
            if ((countOfColumns > 7) && (countOfStrings > 7))
            {
                Console.WriteLine("!Размер матрицы в файле превышает 7Х7!");
                Environment.Exit(0);
            }
            double[,] matrix = MatrixFromFile(countOfStrings, countOfColumns, path);
            DoOperation(matrix, countOfStrings, countOfColumns);
        }
        /// <summary>
        /// Нахождение количества столбцов матрицы,считываемой из файла.
        /// </summary>
        /// <param name="line">Строка матрицы</param>
        /// <returns>Количесвто столбцов</returns>
        static int GetCountOfColumnsFile(string line)
        {
            string[] arr = line.Split(' ');
            return arr.Length;
        }
        /// <summary>
        /// Считывание матрицы из файла.
        /// </summary>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количесвто столбцов</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Матрица</returns>
        static double[,] MatrixFromFile(int countOfStrings, int countOfColumns, string path)
        {
            double[,] matrix = new double[countOfStrings, countOfColumns];
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 0; i < countOfStrings; i++)
                {
                    string line = sr.ReadLine();
                    string[] arr2 = line.Split(' ');
                    for (int j = 0; j < countOfColumns; j++)
                    {
                        double element = 0;
                        // Выводит сообщение об ошибке если данные не корректны.
                        try
                        {
                            element = double.Parse(arr2[j]);
                        }
                        catch
                        {
                            Console.WriteLine("!Данные в файле не корректны!");
                            Environment.Exit(0);

                        }
                        matrix[i, j] = element;
                    }
                }
            }
            return matrix;
        }
        /// <summary>
        /// Рандомное заполнение матрицы вещественными числами.
        /// </summary>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        /// <returns>Матрицу с рандомными элементами</returns>
        static double[,] RandomHardFilling(int countOfStrings, int countOfColumns)
        {
            double[,] matrix = new double[countOfStrings, countOfColumns];
            var rand = new Random();
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    matrix[i, j] = rand.NextDouble() + rand.Next();
                }
            }
            return matrix;
        }
        /// <summary>
        /// Рандомное заполнение матрицы целыми числами в диапазоне от 0 до 9.
        /// </summary>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        /// <returns>Матрица с рандомными элементами</returns>
        static double[,] RandomSimpleFilling(int countOfStrings, int countOfColumns)
        {
            double[,] matrix = new double[countOfStrings, countOfColumns];
            var rand = new Random();
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    matrix[i, j] = rand.Next(10);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Заполнение матрицы с консоли.
        /// </summary>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        /// <returns>Матрица с элементами, введеными с консоли</returns>
        static double[,] ConsoleFilling(int countOfStrings, int countOfColumns)
        {
            double[,] matrix = new double[countOfStrings, countOfColumns];
            double elementOfMatrix;
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    while (true)
                    {
                        Console.WriteLine("Введите элемент с индексами {0} и {1}", i, j);
                        var userInput = Console.ReadLine();
                        if (double.TryParse(userInput, out elementOfMatrix) == false)
                        {
                            Console.WriteLine("Вы ввели не корректные данные.Пожалуйста, введите вещественное число");
                        }
                        else break;
                    }
                    matrix[i, j] = elementOfMatrix;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Вывод матрицы по строкам.
        /// </summary>
        /// <param name="matrix">Матрица,которую нужно вывести</param>
        static void BeatifulOutput(double[,] matrix)
        {
            int countOfStrings = matrix.GetLength(0);
            int countOfColumns = matrix.GetLength(1);
            for (int i = 0; i < countOfStrings; i++)
            {
                string s = "";
                for (int j = 0; j < countOfColumns; j++)
                {
                    s = s + matrix[i, j] + " ";
                    if (j == (countOfColumns - 1))
                    {
                        Console.WriteLine(s);
                    }
                }
            }
        }
        /// <summary>
        /// Вычисление следа матрицы.
        /// </summary>
        /// <param name="matrix">Квадратная матрица, у которой нужно вычислить след</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        /// <returns>След матрицы</returns>
        static double FindMatrixTrace(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            double trace = 0;
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    if (i == j)
                    {
                        trace += matrix[i, j];
                    }
                }
            }
            return trace;
        }
        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        /// <param name="matrix">Матрица,которую нужно транспонировать</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количесвто столбцов</param>
        /// <returns>Транспонированая матрица</returns>
        static double[,] TranspositionMatrix(double[,] matrix, int countOfStrings, int countOfColumns)
        {
            double[,] transMatrix = new double[countOfColumns, countOfStrings];
            for (int i = 0; i < countOfColumns; i++)
            {
                for (int j = 0; j < countOfStrings; j++)
                {
                    transMatrix[i, j] = matrix[j, i];
                }
            }
            return transMatrix;
        }
        /// <summary>
        /// Вычисление суммы двух матриц(матрицы должны быть одного размера).
        /// </summary>
        /// <param name="matrix1">Первая матрица</param>
        /// <param name="matrix2">Вторая матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количесвто столбцов</param>
        /// <returns>Сумма матриц</returns>
        static double[,] SumOfMatrices(double[,] matrix1, double[,] matrix2, int countOfStrings, int countOfColumns)
        {
            double[,] sumOfMatrices = new double[countOfStrings, countOfColumns];
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    sumOfMatrices[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return sumOfMatrices;
        }
        /// <summary>
        /// Вычисление разности матриц(матрицы должны быть одного размера).
        /// </summary>
        /// <param name="matrix1">Первая матрица</param>
        /// <param name="matrix2">Вторая матрица</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        /// <returns>Разность матриц</returns>
        static double[,] DifferenceOfMatrices(double[,] matrix1, double[,] matrix2, int countOfStrings, int countOfColumns)
        {
            double[,] differenceOfMatrices = new double[countOfStrings, countOfColumns];
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    differenceOfMatrices[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return differenceOfMatrices;
        }
        /// <summary>
        /// Перемножение двух матриц.
        /// </summary>
        /// <param name="matrix1">Первая матрица</param>
        /// <param name="matrix2">Вторая матрица</param>
        /// <param name="countOfString">Количство строк первой матрицы</param>
        /// <param name="countOfColumns2">Количество столбцов второй матрицы</param>
        /// <returns>Произведение двух матриц</returns>
        static double[,] MultiplicationOfMatrices(double[,] matrix1, double[,] matrix2, int countOfString, int countOfColumns2)
        {
            double[,] multiplicationMatrix = new double[countOfString, countOfColumns2];
            for (int i = 0; i < countOfString; i++)
            {
                for (int j = 0; j < countOfColumns2; j++)
                {
                    double[] stringOfMatrix1 = GetStringOfMatrix(matrix1, i);
                    double[] columnOfMatrix2 = GetColumnOfMatrix(matrix2, j);
                    multiplicationMatrix[i, j] = ElementOfMultiplicationMatrix(stringOfMatrix1, columnOfMatrix2);
                }
            }
            return multiplicationMatrix;
        }
        /// <summary>
        /// Получение текущий строки матрицы.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="numberOfString">Номер строки</param>
        /// <returns>Текущая строка матрицы</returns>
        static double[] GetStringOfMatrix(double[,] matrix, int numberOfString)
        {
            int countOfColumns = matrix.GetLength(1);
            double[] stringOfMatrix = new double[countOfColumns];
            for (int i = 0; i < countOfColumns; i++)
            {
                stringOfMatrix[i] = matrix[numberOfString, i];
            }
            return stringOfMatrix;
        }
        /// <summary>
        /// Получение текущего столбца матрицы. 
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="numberOfColumn">Номер столбца</param>
        /// <returns>Текущий столбец матрицы</returns>
        static double[] GetColumnOfMatrix(double[,] matrix, int numberOfColumn)
        {
            int countOfStrings = matrix.GetLength(0);
            double[] columnOfMatrix = new double[countOfStrings];
            for (int i = 0; i < countOfStrings; i++)
            {
                columnOfMatrix[i] = matrix[i, numberOfColumn];
            }
            return columnOfMatrix;
        }
        /// <summary>
        /// Создание элементов матрицы равной произведению двух других матриц.
        /// </summary>
        /// <param name="stringOfMatrix1">Строка первой матрицы</param>
        /// <param name="columnOfMatrix2">Столбец второй матрицы</param>
        /// <returns>Элемент матрицы произведения</returns>
        static double ElementOfMultiplicationMatrix(double[] stringOfMatrix1, double[] columnOfMatrix2)
        {
            var lengthOfString = stringOfMatrix1.Length;
            double sum = 0;
            for (int i = 0; i < lengthOfString; i++)
            {
                sum += stringOfMatrix1[i] * columnOfMatrix2[i];
            }
            return sum;
        }
        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <param name="matrix">Матрица,которую умножают</param>
        /// <param name="number">Число,на которое умножают</param>
        /// <param name="countOfStrings">Количество строк</param>
        /// <param name="countOfColumns">Количество столбцов</param>
        /// <returns>Матрица,умноженная на число</returns>
        static double[,] MultiplicationOfMatriceByNumber(double[,] matrix, double number, int countOfStrings, int countOfColumns)
        {
            double[,] newMatrix = new double[countOfStrings, countOfColumns];
            for (int i = 0; i < countOfStrings; i++)
            {
                for (int j = 0; j < countOfColumns; j++)
                {
                    newMatrix[i, j] = number * matrix[i, j];
                }
            }
            return newMatrix;
        }
        /// <summary>
        /// Нахождение определителя матрицы.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <returns>Определитель матрицы</returns>
        static double FindDeterminantOfMatrix(double[,] matrix)
        {
            double[] componentsOfDeterminant = GetComponentsOfDeterminant(matrix);
            double sum = 0;
            foreach (var component in componentsOfDeterminant)
            {
                sum += component;
            }
            return sum;
        }
        /// <summary>
        /// Создание всех перестановок элементов матрицы.
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <returns>Массив с перестановками</returns>
        static double[] GetComponentsOfDeterminant(double[,] matrix)
        {
            int countOfStrings = matrix.GetLength(0);
            int[,] combinations = GetCombinations(countOfStrings);
            int countOfStrings2 = combinations.GetLength(0);
            double[] componentsOfDeterminant = new double[countOfStrings2];
            int[] temp = new int[countOfStrings];
            for (int i = 0; i < countOfStrings2; i++)
            {
                for (int j = 0; j < countOfStrings; j++)
                {
                    temp[j] = combinations[i, j];
                }
                int countOfInversion = GetCountOfInversion(temp);
                double proz = 1;
                for (int j = 0; j < countOfStrings; j++)
                {
                    proz *= matrix[combinations[i, j], j];
                }
                componentsOfDeterminant[i] = Math.Pow(-1, countOfInversion) * proz;
            }
            return componentsOfDeterminant;
        }
        /// <summary>
        /// Получение количества инверсий в перестановке.
        /// </summary>
        /// <param name="combination">Перестановка</param>
        /// <returns>Количество инверсий</returns>
        static int GetCountOfInversion(int[] combination)
        {
            int countOfInversion = 0;
            for (int i = 0; i < combination.Length; i++)
            {
                for (int j = i + 1; j < combination.Length; j++)
                {
                    if (combination[j] < combination[i])
                    {
                        countOfInversion++;
                    }
                }
            }
            return countOfInversion;
        }
        /// <summary>
        /// Сбор всех перестановок(индексов элементов матрицы) в двумернный массив.
        /// </summary>
        /// <param name="n">Длина перестановки</param>
        /// <returns>Двумернный массив перестановок</returns>
        static int[,] GetCombinations(int n)
        {
            int[,] combinations = new int[Factorial(n), n];
            int countOfCombination = (int)Math.Pow(n, n);
            int curCombination = 0;
            for (int i = 0; i < countOfCombination; i++)
            {
                int[] digitalsForCombination = GetDigitalsForCombination(i, n);
                if (NotFindPovtor(digitalsForCombination))
                {
                    for (int j = 0; j < n; j++)
                    {
                        combinations[curCombination, j] = digitalsForCombination[j];

                    }
                    curCombination++;
                }
            }
            return combinations;
        }
        /// <summary>
        /// Нахождение повторов в массиве.
        /// </summary>
        /// <param name="number">Массив</param>
        /// <returns>True если повторы не найдены, иначе False </returns>
        static bool NotFindPovtor(int[] number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = i + 1; j < number.Length; j++)
                {
                    if (number[i] == number[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Создание перестановок(индексов)переводом числа в систему исчисления,равную длине перестановки.
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="n">Длина перестановки</param>
        /// <returns>Перестановка</returns>
        static int[] GetDigitalsForCombination(int number, int n)
        {
            int[] digitalsForCombination = new int[n];
            int curIndex = n;
            while (number >= n)
            {
                curIndex--;
                digitalsForCombination[curIndex] = number % n;
                number = number / n;
            }
            curIndex--;
            digitalsForCombination[curIndex] = number;
            return digitalsForCombination;
        }
        /// <summary>
        /// Подсчет факториала.
        /// </summary>
        /// <param name="n">Число у которого нужно посчитать факториал</param>
        /// <returns>Факториал числа</returns>
        static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }
    }
}
