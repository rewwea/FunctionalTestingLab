namespace FunctionalTestingLab;

public class Calculator
{
    // Сложение двух целых чисел
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Вычитание
    public int Subtract(int a, int b)
    {
        return a - b;
    }

    // Умножение
    public int Multiply(int a, int b)
    {
        return a * b;
    }

    // Деление
    public double Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Деление на ноль запрещено");
        }

        return (double)a / b;
    }

    // Проверка числа на четность
    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    // Факториал числа
    public int Factorial(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException(
                "Факториал определен только для неотрицательных чисел");
        }

        if (n == 0)
        {
            return 1;
        }

        int result = 1;

        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    // Проверка строки на палиндром
    public bool IsPalindrome(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        string cleaned = text.ToLower()
            .Replace(" ", "")
            .Replace(",", "")
            .Replace(".", "");

        cleaned = new string(
            cleaned.Where(c => char.IsLetterOrDigit(c)).ToArray()
        );

        char[] arr = cleaned.ToCharArray();
        Array.Reverse(arr);

        string reversed = new string(arr);

        return cleaned == reversed;
    }
}