using System;
using System.Diagnostics;

namespace AlgoritmosOrdenado
{
    public class ArraySort
    {
        public int[] array;
        public int[] arrayIncreasing;
        public int[] arrayDecreasing;

        public ArraySort(int elements, Random random)
        {
            array = new int[elements];
            arrayIncreasing = new int[elements];
            arrayDecreasing = new int[elements];
            for (int i = 0; i < elements; i++)
            {
                array[i] = random.Next();
            }
            Array.Copy(array, arrayIncreasing, elements);
            Array.Sort(arrayIncreasing);

            Array.Copy(arrayIncreasing, arrayDecreasing, elements);
            Array.Reverse(arrayDecreasing);
        }

        public void Benchmark(Action<int[]> function)
        {
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine(function.Method.Name);

            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Random: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
            stopwatch.Reset();

            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Increasing: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
            stopwatch.Reset();

            Array.Reverse(temp);
            stopwatch.Start();
            function(temp);
            stopwatch.Stop();
            Console.WriteLine("Decreasing: " + stopwatch.ElapsedMilliseconds + "ms " + stopwatch.ElapsedTicks + "ticks");
        }

        public void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if(arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        public void BubbleSortEarlyExit(int[] arr)
        {
            bool isOrdered = true;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                isOrdered = true;
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if(arr[j] > arr[j + 1])
                    {
                        isOrdered = false;
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
                if (isOrdered)
                    return;
            }
        }
        public void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }
        public void QuickSort(int[] arr, int left, int right)
        {
            if(left < right)
            {
                int pivot = QuickSortIndex(arr, left, right);
                QuickSort(arr, left, pivot);
                QuickSort(arr, pivot + 1, right);
            }
        }
        public int QuickSortIndex(int[] arr, int left, int right)
        {
            int pivot = arr[(left + right) / 2];

            while (true)
            {
                while(arr[left] < pivot)
                {
                    left++;
                }
                while(arr[right] > pivot)
                {
                    right--;
                }
                if(right <= left)
                {
                    return right;
                }
                else
                {
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    right--;left++;
                }
            }
        }
        public void InsertSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
            
        }

        public int[] MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);
                MergeArray(array, left, middle, right);
            }
            return arr;
        }
        public void MergeArraySort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }
        public void MergeArray(int[] arr, int left, int middle, int right)
        {
            var leftArrayLength = middle - left + 1;
            var rightArrayLength = right - middle;
            var leftTempArray = new int[leftArrayLength];
            var rightTempArray = new int[rightArrayLength];
            int i, j;
            for (i = 0; i < leftArrayLength; ++i)
                leftTempArray[i] = arr[left + i];
            for (j = 0; j < rightArrayLength; ++j)
                rightTempArray[j] = arr[middle + 1 + j];
            i = 0;
            j = 0;
            int k = left;
            while (i < leftArrayLength && j < rightArrayLength)
            {
                if (leftTempArray[i] <= rightTempArray[j])
                {
                    arr[k++] = leftTempArray[i++];
                }
                else
                {
                    arr[k++] = rightTempArray[j++];
                }
            }
            while (i < leftArrayLength)
            {
                arr[k++] = leftTempArray[i++];
            }
            while (j < rightArrayLength)
            {
                arr[k++] = rightTempArray[j++];
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many numbers do you want?");
            int elements = int.Parse(Console.ReadLine());
            Console.WriteLine("What seed do you want to use?");
            int seed = int.Parse(Console.ReadLine());
            Random random = new Random(seed);
            ArraySort arraySort = new ArraySort(elements, random);

            //arraySort.Benchmark(arraySort.BubbleSort);
            arraySort.Benchmark(arraySort.BubbleSortEarlyExit);
            arraySort.Benchmark(arraySort.QuickSort);
            arraySort.Benchmark(arraySort.InsertSort);
            arraySort.Benchmark(arraySort.MergeArraySort);

        }
    }
}
