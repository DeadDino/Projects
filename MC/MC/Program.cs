using System;
using System.Dynamic;
using System.IO;

namespace MC {
    class Program {
        static Random ran = new Random();
        static void Main(string[] args) {
            // Название консоли.
            Console.Title = "Калькулятор дробе... тьфу, матриц";

            Console.WriteLine("Добро пожаловать !");
            Console.WriteLine();

            // Список команд.
            Rules();

            Console.ReadKey();

        }
        /// <summary>
        /// Список возможных команд.
        /// </summary>
        private static void Rules() {
            Console.WriteLine("Список возможных команд: \n1. нахождение следа матрицы;");
            Console.WriteLine("2. транспонирование матрицы;");
            Console.WriteLine("3. сумма двух матриц;");
            Console.WriteLine("4. разность двух матриц;");
            Console.WriteLine("5. произведение двух матриц;");
            Console.WriteLine("6. умножение матрицы на число;");
            Console.WriteLine("7. нахождение определителя матрицы");
            Console.WriteLine("end - выход из программы");
            Console.WriteLine("help - список возможных команд");
            Console.WriteLine("Для того, чтобы выйти из команды и выбрать другую, введите end (вместо того, что просят ввести)");
            Console.WriteLine();
            // Выбор команды.
            Commands();
        }
        /// <summary>
        ///  Метод, отвечающий за выбор команд.
        /// </summary>
        private static void Commands() {
            Console.Write("Выберете команду (номер команды): ");
            string a = Console.ReadLine();
            Console.WriteLine();
            // Определение введённой команды.
            switch (a) {
                case "1":
                    // Нахождение следа матрицы.
                    Findtrack();
                    break;
                case "2":
                    // Транспонирование матрицы.
                    Tranposition();
                    break;
                case "3":
                    // Сумма двух матриц. 
                    Summ();
                    break;
                case "4":
                    // Разность двух матриц.
                    Difference();
                    break;
                case "5":
                    // Произведение двух матриц.
                    Multiplication();
                    break;
                case "6":
                    // Умножение матрицы на число.
                    MultiplicationByNumber();
                    break;
                case "7":
                    // Нахождение определителя матрицы.
                    FindDeterminant();
                    break;
                case "help":
                    // Список возможных команд.
                    Rules();
                    break;
                case "end":
                    // Выход из программы.
                    End();
                    break;
                default:
                    Console.WriteLine("С какой стати, вы меня извините, вы вводите команду, которой не существует? Я скандал ТАКОЙ учиню ");
                    Console.WriteLine();
                    Commands();
                    break;
            }
        }

        /// <summary>
        /// Выход из программы.
        /// </summary>
        private static void End() {
            Console.WriteLine("Уверены, что хотите выйти?");

            string ans;
            do {
                Console.Write("Введите yes или no: ");
                ans = Console.ReadLine();
            } while (ans != "yes" && ans != "no");

            if (ans == "yes") {
                Console.WriteLine("Удачки)");
                return;
            }
            else {
                Console.WriteLine();
                // Выбор команды.
                Commands();
            }
        }

        /// <summary>
        /// Список возможных способов ввода матриц.
        /// </summary>
        private static void Rulesofselectinginput() {
            Console.WriteLine("Иногда нам нужно сделать выбор, от которого зависит наша дальнейшпя судьба, но сейчас просто выбери путь ввода данных.");
            Console.WriteLine("Они могут:\n1.генерироваться случайным образом");
            Console.WriteLine("2.поступать из консоли");
            Console.WriteLine("3.считываться из  существующего файла");
            Console.WriteLine();

        }
        /// <summary>
        /// Проверка на верность введенных параметров строк и столбцов для матрицы.
        /// </summary>
        /// <param name="number1"> Количество строк матрицы. </param>
        /// <param name="number2"> Количество столбцов. </param>
        /// <returns></returns>
        private static bool CheckNumbers(out uint number1, out uint number2) {
            if (!uint.TryParse(Console.ReadLine(), out number1) | !uint.TryParse(Console.ReadLine(), out number2)
                || number1 > 10 || number2 > 10 || number1 == 0 || number2 == 0) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, отвечающий за выбор типа ввода.
        /// </summary>
        /// <param name="length"> Количество строк. </param>
        /// <param name="width"> Количество столбцов. </param>
        /// <returns></returns>
        private static int[,] SelectingInput(uint length, uint width) {
            Rulesofselectinginput();


            Console.WriteLine("Введите нужный тип ввода (его номер)");

            string selectedNumber = Console.ReadLine();
            while (selectedNumber != "1" & selectedNumber != "2" & selectedNumber != "3") {
                Console.WriteLine("Такой команды нет, радость моя.");
                selectedNumber = Console.ReadLine();
            }
            switch (selectedNumber) {
                // Определение введённой команды.
                case "1":
                    // Данные генерируются случайным образом.
                    return GenerateArray(length, width);

                case "2":
                    // Данные вводятся с консоли.
                    return ConsoleInputArray(length, width);

                case "3":
                    // Данные вводятся с файла.
                    return FileInputArray();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Метод, принимающий данные с файла.
        /// </summary>
        /// <returns></returns>
        private static int[,] FileInputArray() {
            Console.WriteLine("Введите путь к существующему файлу формата .txt, с которого вы хотите считать" +
                                    " матрицу. В нем должны содержаться целые числа через пробел");
            string fileName = Console.ReadLine();
            string fullPath = Path.GetFullPath(fileName);
            // Расширение файла.
            string extension = Path.GetExtension(fullPath);
            while (!File.Exists(fullPath) || extension != ".txt") {
                Console.WriteLine("Некорретные данные. Попробуйте еще раз.");
                Console.WriteLine("Введите путь к существующему файлу формата .txt, с которого вы хотите считать" +
                                    " матрицу. В нем должны содержаться целые числа через пробел");
                fileName = Console.ReadLine();
                fullPath = Path.GetFullPath(fileName);
                extension = Path.GetExtension(fullPath);
            }
            bool readSuccsess = false;
            while (!readSuccsess) {
                try {

                    string[] lines = File.ReadAllLines(fileName);
                    // Объявляем двумерный массив.
                    int[,] num = new int[lines.Length, lines[0].Split(' ').Length];
                    // Инициализируем данный массив.
                    for (int i = 0; i < lines.Length; i++) {
                        string[] temp = lines[i].Split(' ');
                        for (int j = 0; j < temp.Length; j++)
                            num[i, j] = Convert.ToInt32(temp[j]);
                    }
                    readSuccsess = true;
                    return num;
                }
                catch (Exception ex) {
                    Console.WriteLine("Проблемы с файлом. Возможно в нем находятся не только целые числа или некорректный размер матрицы.");
                    Console.WriteLine(ex.Message);
                    FileInputArray();
                }
            }
            return null;
        }
        /// <summary>
        /// Метод, подающий в массив рандомные числа.
        /// </summary>
        /// <param name="length"> Количетсво строк. </param>
        /// <param name="k"> Количество столбцов. </param>
        /// <returns></returns>
        private static int[,] GenerateArray(uint length, uint width) {
            // Объявляем двумерный массив.
            int[,] randomArr = new int[length, width];

            // Инициализируем данный массив.
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < width; j++) {
                    randomArr[i, j] = ran.Next(-100, 101);
                    Console.Write($"{randomArr[i, j]}\t");
                }
                Console.WriteLine();
            }

            return randomArr;
        }
        /// <summary>
        /// Метод, подающий в массив числа с консоли.
        /// </summary>
        /// <param name="length"> Количетсво строк. </param>
        /// <param name="width"> Количество столбцов. </param>
        /// <returns></returns>
        private static int[,] ConsoleInputArray(uint length, uint width) {
            int[,] newArr = new int[length, width];
            Console.WriteLine("Вводите целые числа от -100 до 100.");
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < width; j++) {
                    int flag1 = 0;
                    while (flag1 == 0) {
                        if (!int.TryParse(Console.ReadLine(), out int arr) || arr > 100 || arr < -100) {
                            Console.WriteLine("Некорректный ввод");
                        }
                        else {
                            newArr[i, j] = arr;
                            flag1 = 1;

                        }
                    }

                    Console.Write("{0}\t", newArr[i, j]);
                }
                Console.WriteLine();
            }
            return newArr;
        }
        /// <summary>
        /// Метод, считающий след матрицы.
        /// </summary>
        private static void Findtrack() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10)" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            int track = 0;
            try {
                int[,] arr = SelectingInput(num1, num2);
                for (int i = 0; i < arr.GetLength(0); i++) {
                    for (int j = 0; j < arr.GetLength(1); j++) {
                        if (i == j) {
                            track += arr[i, j];
                        }
                    }
                }
                Console.WriteLine("След матрицы: " + track);
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                Findtrack();
            }
        }
        /// <summary>
        /// Метод, транспонирующий матрицу.
        /// </summary>
        private static void Tranposition() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10)" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            int[,] arr = SelectingInput(num1, num2);
            int[,] trans = new int[arr.GetLength(1), arr.GetLength(0)];
            try {
                Console.WriteLine("Транспонированная матрица: ");
                for (int i = 0; i < arr.GetLength(1); i++) {
                    for (int j = 0; j < arr.GetLength(0); j++) {
                        trans[i, j] = arr[j, i];
                        Console.Write(trans[i, j] + " \t ");
                    }
                    Console.WriteLine();
                }
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                Tranposition();
            }
        }

        /// <summary>
        /// Метод, складывающий матрицы.
        /// </summary>
        private static void Summ() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матриц,которые хотите сложить(количество строк и столбцов от 1 до 10)." +
                "По очевидным причинам количество строк и столбцов в обеих матрицах должны совпадать, поэтому вводите их только 1 раз" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            Console.WriteLine("\n Выберите откуда хотите получить первую матрицу:");
            int[,] arr1 = SelectingInput(num1, num2);
            Console.WriteLine("\n Выберите откуда хотите получить вторую матрицу:");
            int[,] arr2 = SelectingInput(num1, num2);
            int[,] arrSum = new int[arr1.GetLength(0), arr1.GetLength(1)];
            Console.WriteLine("\n Сумма матриц:");
            try {
                Console.WriteLine("\n Сумма матриц:");
                for (int i = 0; i < arr1.GetLength(0); i++) {
                    for (int j = 0; j < arr1.GetLength(1); j++) {
                        arrSum[i, j] = arr1[i, j] + arr2[i, j];
                        Console.Write(arrSum[i, j] + " \t ");
                    }
                    Console.WriteLine();

                }
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                Summ();
            }
        }

        /// <summary>
        /// Метод, отнимающий матрицы.
        /// </summary>
        private static void Difference() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матриц,которые хотите вычесть(количество строк и столбцов от 1 до 10)." +
                "По очевидным причинам количество строк и столбцов в обеих матрицах должны совпадать, поэтому вводите их только 1 раз" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            Console.WriteLine("\n Выберите откуда хотите получить первую матрицу(Уменьшаемое):");
            int[,] arr1 = SelectingInput(num1, num2);
            Console.WriteLine("\n Выберите откуда хотите получить вторую матрицу(ВЫчитаемое):");
            int[,] arr2 = SelectingInput(num1, num2);
            int[,] arrDiff = new int[arr1.GetLength(0), arr1.GetLength(1)];

            try {
                Console.WriteLine("\n Разность матриц:");
                for (int i = 0; i < arr1.GetLength(0); i++) {
                    for (int j = 0; j < arr1.GetLength(1); j++) {
                        arrDiff[i, j] = arr1[i, j] - arr2[i, j];
                        Console.Write(arrDiff[i, j] + " \t ");
                    }
                    Console.WriteLine();

                }
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                Difference();
            }
        }

        /// <summary>
        /// Метод, перемножающий матрицы.
        /// </summary>
        private static void Multiplication() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10)" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            Console.WriteLine("\n Выберите откуда хотите получить первую матрицу:");
            int[,] arr1 = SelectingInput(num1, num2);
            uint num3;
            uint num4;
            Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10)" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num3, out num4)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            Console.WriteLine("\n Выберите откуда хотите получить вторую матрицу, но помни, что количество столбцов в первой должно совпадать с количеством строк во второй:");
            int[,] arr2 = SelectingInput(num3, num4);
            if (arr1.GetLength(1) != arr2.GetLength(0)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Количество столбцов первой матрицы должно быть равно количетсву строк второй матрицы");
                Console.WriteLine("Еще раз");
                Multiplication();
            }
            int[,] arrMulti = new int[arr1.GetLength(0), arr2.GetLength(1)];
            try {
                Console.WriteLine("\nПроизвдение матриц: \n");
                for (int i = 0; i < arr1.GetLength(0); i++) {
                    for (int j = 0; j < arr2.GetLength(1); j++) {
                        for (int k = 0; k < arr2.GetLength(0); k++) {
                            arrMulti[i, j] += arr1[i, k] * arr2[k, j];
                        }
                        Console.Write(arrMulti[i, j] + " \t ");
                    }
                    Console.WriteLine();
                }
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                Multiplication();
            }
        }

        /// <summary>
        /// Метод, усножающий матрицу на число.
        /// </summary>
        private static void MultiplicationByNumber() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10)" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            int[,] arr = SelectingInput(num1, num2);

            double numMulti;
            while (!double.TryParse(Console.ReadLine(), out numMulti)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите вещественное число, на которое хотите умножить матрицу");
            }
            double[,] arrSummByNumber = new double[arr.GetLength(0), arr.GetLength(1)];
            try {
                Console.WriteLine("\nМатрица, умноженная на введенное число: \n");
                for (int i = 0; i < arr.GetLength(0); i++) {
                    for (int j = 0; j < arr.GetLength(1); j++) {
                        arrSummByNumber[i, j] = arr[i, j] * numMulti;
                        Console.Write(arrSummByNumber[i, j] + " \t ");
                    }
                    Console.WriteLine();

                }
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                MultiplicationByNumber();
            }
        }

        /// <summary>
        /// Метод, считающий определитель.
        /// </summary>
        private static void FindDeterminant() {
            uint num1;
            uint num2;
            Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10)" +
                "(Если будет выбран метод ввода с файла, то эти данные не влияют. Матрица будет такая же, как в файле)" +
                "(матрица должна быть квадратной):");
            while (!CheckNumbers(out num1, out num2)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Введите размер матрицы(количество строк и столбцов от 1 до 10):");
            }
            int[,] arr = SelectingInput(num1, num2);
            if (arr.GetLength(1) != arr.GetLength(0)) {
                Console.WriteLine("Некорректный ввод");
                Console.WriteLine("Матрица должна быть квадратная");
                Console.WriteLine("Еще раз");
                FindDeterminant();
            }
            try {
                Console.WriteLine("\n Детерминант: \n");
                if (arr.GetLength(0) == 1) {
                    Console.WriteLine(arr[1, 1]);
                }
                if (arr.GetLength(0) == 2) {
                    Console.WriteLine(arr[1, 1] * arr[2, 2] - arr[1, 2] * arr[2, 1]);
                }
                if (arr.GetLength(0) == 3) {
                    Console.WriteLine(arr[1, 1] * arr[2, 2] * arr[3, 3] + arr[1, 2] * arr[2, 3] * arr[3, 1] + arr[1, 3] * arr[2, 1] * arr[3, 2] - arr[1, 3] * arr[3, 1] * arr[2, 2] - arr[1, 1] * arr[2, 3] * arr[3, 2] - arr[3, 1] * arr[2, 1] * arr[1, 2]);
                }
                if (arr.GetLength(0) > 3) {
                    Console.WriteLine();
                }
                Commands();
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка ввода данных. Попробуйте еще раз:");
                Console.WriteLine(ex.Message);
                FindDeterminant();
            }

        }

       
    }

}





