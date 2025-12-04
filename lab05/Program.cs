/*
Выведите алгоритм решения следующей задачи: 
"Дано 2 списка с равным количеством чисел. 
Вычислите попарную сумму элементов из этих списков 
(первый элемент из первого списка, первый из второго; 
второй элемент из первого списка, второй из второго; ...)"

input: 2 массива (A, B)
output: массив попарной суммы C

Логика алгоритма:

1. проверка: равны ли длины массива?
    да-> шаг 2
    нет-> выкинуть ArgumentException / использовать функцию Debug.Assert
2. Для каждого индекса i от 0 до (длина массива – 1):
    Вычислить сумму: A[i] + B[i].
    Добавить её в C.

вспомогательная функция печати(input: массив сумм С, output: элементы массива)

*/
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] A = { 2, 5, 7 };
        int[] B = { 4, 1, 9 };

        int[] C = new int[A.Length];

        PairwiseSumFor(A, B, C);

        Console.WriteLine("Результат (цикл for):");
        PrintArray(C);

        PairwiseSumWhile(A, B, C);

        Console.WriteLine("Результат (цикл while):");
        PrintArray(C);
    }

    static void PairwiseSumFor(int[] a, int[] b, int[] output)
    {
        if (a.Length != b.Length || b.Length != output.Length)
            throw new ArgumentException("Длины массивов должны совпадать.");

        for (int i = 0; i < a.Length; i++)
        {
            output[i] = a[i] + b[i];
        }
    }

    static void PairwiseSumWhile(int[] a, int[] b, int[] output)
    {
        Debug.Assert(a.Length == b.Length && b.Length == output.Length,
            "Длины массивов должны совпадать.");

        int i = 0;
        while (true)
        {
            if (i < a.Length)
            {
                output[i] = a[i] + b[i];
                i++;
                continue;
            }
            break;
        }
        
    }


    static void PrintArray(int[] arr)
    {
        foreach (int x in arr)
            Console.Write(x + " ");
        Console.WriteLine();
    }
}

    