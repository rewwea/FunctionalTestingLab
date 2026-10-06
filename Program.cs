using FunctionalTestingLab;

Console.WriteLine(
    "Лабораторная работа №23. Функциональное тестирование");

Console.WriteLine(
    "====================================================\n");

var tests = new CalculatorTests();

tests.RunAllTests();

Console.WriteLine("\nНажмите любую клавишу для завершения...");
Console.ReadKey();