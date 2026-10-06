namespace FunctionalTestingLab;

public class TestResult
{
    public string TestName { get; set; } = "";
    public bool Passed { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ExpectedValue { get; set; }
    public string? ActualValue { get; set; }
}

public class CalculatorTests
{
    private readonly Calculator _calculator = new();
    private readonly List<TestResult> _results = new();

    public void RunAllTests()
    {
        Console.WriteLine("ЗАПУСК ФУНКЦИОНАЛЬНОГО ТЕСТИРОВАНИЯ\n");

        TestAdd();
        TestSubtract();
        TestMultiply();
        TestDivide();
        TestIsEven();
        TestFactorial();
        TestIsPalindrome();

        PrintSummary();
    }

    private void AssertEqual<T>(T expected, T actual, string testName)
    {
        var result = new TestResult
        {
            TestName = testName
        };

        if (Equals(expected, actual))
        {
            result.Passed = true;
            Console.WriteLine($"✓ {testName} - ПРОЙДЕН");
        }
        else
        {
            result.Passed = false;
            result.ExpectedValue = expected?.ToString();
            result.ActualValue = actual?.ToString();
            result.ErrorMessage =
                $"Ожидалось: {expected}, Получено: {actual}";

            Console.WriteLine(
                $"✗ {testName} - НЕ ПРОЙДЕН: {result.ErrorMessage}");
        }

        _results.Add(result);
    }

    private void AddPassedTest(string testName)
    {
        Console.WriteLine($"✓ {testName} - ПРОЙДЕН");

        _results.Add(new TestResult
        {
            TestName = testName,
            Passed = true
        });
    }

    private void AddFailedTest(string testName, string message)
    {
        Console.WriteLine($"✗ {testName} - НЕ ПРОЙДЕН: {message}");

        _results.Add(new TestResult
        {
            TestName = testName,
            Passed = false,
            ErrorMessage = message
        });
    }

    private void PrintSummary()
    {
        Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ТЕСТИРОВАНИЯ ===");

        int passed = _results.Count(r => r.Passed);
        int failed = _results.Count(r => !r.Passed);

        Console.WriteLine($"Всего тестов: {_results.Count}");
        Console.WriteLine($"Пройдено: {passed}");
        Console.WriteLine($"Не пройдено: {failed}");
        Console.WriteLine(
            $"Процент прохождения: {100.0 * passed / _results.Count:F2}%");

        if (failed > 0)
        {
            Console.WriteLine("\n=== СПИСОК ОБНАРУЖЕННЫХ ДЕФЕКТОВ ===");

            foreach (var result in _results.Where(r => !r.Passed))
            {
                Console.WriteLine(
                    $"- {result.TestName}: {result.ErrorMessage}");
            }
        }
    }

    private void TestAdd()
    {
        Console.WriteLine("\n--- Тестирование Add ---");

        AssertEqual(8, _calculator.Add(5, 3),
            "TC-ADD-01: 5 + 3 = 8");

        AssertEqual(5, _calculator.Add(5, 0),
            "TC-ADD-02: 5 + 0 = 5");

        AssertEqual(-8, _calculator.Add(-5, -3),
            "TC-ADD-03: -5 + -3 = -8");

        AssertEqual(2, _calculator.Add(5, -3),
            "TC-ADD-04: 5 + -3 = 2");

        AssertEqual(int.MaxValue, _calculator.Add(int.MaxValue, 0),
            "TC-ADD-05: MaxInt + 0");
    }

    private void TestSubtract()
    {
        Console.WriteLine("\n--- Тестирование Subtract ---");

        AssertEqual(2, _calculator.Subtract(5, 3),
            "TC-SUB-01: 5 - 3 = 2");

        AssertEqual(5, _calculator.Subtract(5, 0),
            "TC-SUB-02: 5 - 0 = 5");

        AssertEqual(8, _calculator.Subtract(5, -3),
            "TC-SUB-03: 5 - (-3) = 8");

        AssertEqual(-2, _calculator.Subtract(3, 5),
            "TC-SUB-04: 3 - 5 = -2");
    }

    private void TestMultiply()
    {
        Console.WriteLine("\n--- Тестирование Multiply ---");

        AssertEqual(15, _calculator.Multiply(5, 3),
            "TC-MUL-01: 5 * 3 = 15");

        AssertEqual(0, _calculator.Multiply(5, 0),
            "TC-MUL-02: 5 * 0 = 0");

        AssertEqual(5, _calculator.Multiply(5, 1),
            "TC-MUL-03: 5 * 1 = 5");

        AssertEqual(-15, _calculator.Multiply(-5, 3),
            "TC-MUL-04: -5 * 3 = -15");

        AssertEqual(15, _calculator.Multiply(-5, -3),
            "TC-MUL-05: -5 * -3 = 15");
    }

    private void TestDivide()
    {
        Console.WriteLine("\n--- Тестирование Divide ---");

        AssertEqual(2.0, _calculator.Divide(6, 3),
            "TC-DIV-01: 6 / 3 = 2");

        AssertEqual(2.5, _calculator.Divide(5, 2),
            "TC-DIV-02: 5 / 2 = 2.5");

        try
        {
            _calculator.Divide(5, 0);

            AddFailedTest(
                "TC-DIV-03: Деление на ноль",
                "Ожидалось DivideByZeroException");
        }
        catch (DivideByZeroException)
        {
            AddPassedTest(
                "TC-DIV-03: Деление на ноль вызывает исключение");
        }

        AssertEqual(0.0, _calculator.Divide(0, 5),
            "TC-DIV-04: 0 / 5 = 0");
    }

    private void TestIsEven()
    {
        Console.WriteLine("\n--- Тестирование IsEven ---");

        AssertEqual(true, _calculator.IsEven(4),
            "TC-EVEN-01: 4 - четное");

        AssertEqual(false, _calculator.IsEven(5),
            "TC-EVEN-02: 5 - нечетное");

        AssertEqual(true, _calculator.IsEven(0),
            "TC-EVEN-03: 0 - четное");

        AssertEqual(true, _calculator.IsEven(-4),
            "TC-EVEN-04: -4 - четное");

        AssertEqual(false, _calculator.IsEven(-5),
            "TC-EVEN-05: -5 - нечетное");
    }

    private void TestFactorial()
    {
        Console.WriteLine("\n--- Тестирование Factorial ---");

        AssertEqual(1, _calculator.Factorial(0),
            "TC-FACT-01: 0! = 1");

        AssertEqual(1, _calculator.Factorial(1),
            "TC-FACT-02: 1! = 1");

        AssertEqual(120, _calculator.Factorial(5),
            "TC-FACT-03: 5! = 120");

        try
        {
            _calculator.Factorial(-5);

            AddFailedTest(
                "TC-FACT-04: Факториал -5",
                "Ожидалось ArgumentException");
        }
        catch (ArgumentException)
        {
            AddPassedTest(
                "TC-FACT-04: Отрицательное число вызывает ArgumentException");
        }
    }

    private void TestIsPalindrome()
    {
        Console.WriteLine("\n--- Тестирование IsPalindrome ---");

        AssertEqual(true,
            _calculator.IsPalindrome("казак"),
            "TC-PAL-01: казак");

        AssertEqual(true,
            _calculator.IsPalindrome("А роза упала на лапу Азора"),
            "TC-PAL-02: палиндром с пробелами");

        AssertEqual(false,
            _calculator.IsPalindrome("привет"),
            "TC-PAL-03: привет - не палиндром");

        AssertEqual(false,
            _calculator.IsPalindrome(""),
            "TC-PAL-04: пустая строка");

        AssertEqual(true,
            _calculator.IsPalindrome("a"),
            "TC-PAL-05: один символ");

        AssertEqual(false,
            _calculator.IsPalindrome(null),
            "TC-PAL-06: null");

        AssertEqual(true,
            _calculator.IsPalindrome("12321"),
            "TC-PAL-07: палиндром с цифрами");

        AssertEqual(true,
            _calculator.IsPalindrome("Казак"),
            "TC-PAL-08: смешанный регистр");

        AssertEqual(true,
            _calculator.IsPalindrome("А, роза упала на лапу Азора!"),
            "TC-PAL-09: строка со знаками препинания");
    }
}