using Microsoft.VisualStudio.TestPlatform.TestHost;
using MergeSort;

namespace MergeSort.Tests
{
    public class UnitTest1
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 3, 5, 2, 3, 6 };
            int[] expectedArray = { 1, 2, 3, 3, 5, 6 };
            int left = 0;
            int right = array.Length-1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_WithDuplicates_ReturnsMergedSortedArray()
        {
            int[] array = { 2, 2, 4, 1, 2};
            int[] expectedArray = { 1, 2, 2, 2, 4 };

            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            MergeSortClass.Merge(array, left, middle, right);

            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void Merge_NegativeNumbers_ReturnsMergedSortedArray()
        {
            int[] array = { -5, -2, 0, -4, -1, 3 };
            int[] expectedArray = { -5, -4, -2, -1, 0, 3 };

            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            MergeSortClass.Merge(array, left, middle, right);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_LargeNumber_ReturnsMergedSortedArray()
        {
            int[] array = { 1, 2, 999999, 3, 4, 10};
            int[] expectedArray = { 1, 2, 3, 4, 10, 999999 };

            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            MergeSortClass.Merge(array, left, middle, right);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void Merge_TwoElements_ReturnsMergedSortedArray()
        {
            int[] array = { 44, 4 };
            int[] expectedArray = { 4, 44 };

            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            MergeSortClass.Merge(array, left, middle, right);

            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void Merge_AlreadySorted_ReturnsSameArray()
        {
            int[] array = { 1, 2, 3, 4, 5, 6 };
            int[] expectedArray = { 1, 2, 3, 4, 5, 6 };

            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            MergeSortClass.Merge(array, left, middle, right);

            Assert.Equal(expectedArray, array);
        }

        //MergeSort

        [Fact]
        public void MergeSort_RandomArray_ReturnsSortedArray()
        {
            int[] array = { 5, 2, 8, 1, 9, 3 };
            int[] expectedArray = { 1, 2, 3, 5, 8, 9 };

            MergeSortClass.MergeSort(array, 0, array.Length - 1);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void MergeSort_ArrayWithDuplicates_ReturnsSortedArray()
        {
            int[] array = { 4, 2, 4, 1, 2, 3 };
            int[] expectedArray = { 1, 2, 2, 3, 4, 4 };

            MergeSortClass.MergeSort(array, 0, array.Length - 1);

            Assert.Equal(expectedArray, array);
        }

        [Fact]
        public void MergeSort_EmptyArray_ReturnsEmptyArray()
        {
            int[] array = { };
            int[] expectedArray = { };

            MergeSortClass.MergeSort(array, 0, array.Length - 1);

            Assert.Equal(expectedArray, array);
        }
    }
}
