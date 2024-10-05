using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        int firstVariable = random.Next();  // Первая переменная со случайным значением
        int secondVariable = random.Next(); // Вторая переменная со случайным значением
        int thirdVariable = 0;               // Третья переменная, которую будем заполнять

        // Получаем указатели на переменные
        byte[] firstBytes = BitConverter.GetBytes(firstVariable);
        byte[] secondBytes = BitConverter.GetBytes(secondVariable);

        // Копируем байты из первой и второй переменной в третью
        byte[] combinedBytes = new byte[sizeof(int)];
        Array.Copy(firstBytes, 0, combinedBytes, 0, 2);  // Первые 2 байта из первой переменной
        Array.Copy(secondBytes, 0, combinedBytes, 2, 2); // Следующие 2 байта из второй переменной

        // Преобразуем полученный массив байт обратно в int
        thirdVariable = BitConverter.ToInt32(combinedBytes, 0);

        // Выводим значения переменных
        Console.WriteLine($"Первая переменная: {firstVariable}");
        Console.WriteLine($"Вторая переменная: {secondVariable}");
        Console.WriteLine($"Третья переменная (составная): {thirdVariable}");

        // Проверяем корректность вычислений
        CheckIntegrity(firstVariable, secondVariable, thirdVariable);
    }

    static void CheckIntegrity(int first, int second, int third)
    {
        // Проверяем, что первые два байта thirdVariable совпадают с первой переменной
        byte[] firstBytes = BitConverter.GetBytes(first);
        byte[] thirdBytes = BitConverter.GetBytes(third);

        bool isValid = true;
        for (int i = 0; i < 2; i++)
        {
            if (firstBytes[i] != thirdBytes[i])
            {
                isValid = false;
                break;
            }
        }

        // Проверяем, что вторые два байта thirdVariable совпадают со второй переменной
        byte[] secondBytes = BitConverter.GetBytes(second);
        for (int i = 2; i < 4; i++)
        {
            if (secondBytes[i - 2] != thirdBytes[i])
            {
                isValid = false;
                break;
            }
        }

        if (isValid)
        {
            Console.WriteLine("Корректность вычислений подтверждена.");
        }
        else
        {
            Console.WriteLine("Ошибка: данные не совпадают.");
        }
    }
}
