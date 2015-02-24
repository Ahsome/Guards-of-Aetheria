using System;

namespace GuardsOfAetheria
{
    class Utility
    {
        public int SelectOption(string[] options) //if (CursorTop > maxline) then (new page)
        {
            var menuSelected = 1;
            var startLine = Console.CursorTop;
            decimal numberOfLines = 23 - startLine;
            var totalLines = Convert.ToInt32(numberOfLines);
            var pages = Convert.ToInt32(Math.Round(((options.Length + numberOfLines) / (2 * numberOfLines)), MidpointRounding.AwayFromZero));
            var pageNumber = 1;
            var oldPageNumber = 0;
            var possibleOptions = options.Length - (totalLines * (pageNumber - 1));
            if (totalLines < possibleOptions)
            {
                possibleOptions = totalLines;
            }
            for (var i = 0; i < possibleOptions; i++)
            {
                Console.SetCursorPosition(2, startLine + i + 1);
                Console.Write("                       ");
                Console.SetCursorPosition(2, startLine + i + 1);
                Console.Write(options[i]);
            }
            while (true)
            {
                Console.SetCursorPosition(0, menuSelected + startLine);
                Console.Write('>');
                while (true)
                {
                    var lastPage = false;
                    var nextPage = false;
                    var input = Console.ReadKey(true).Key;
                    if (input == ConsoleKey.Enter) { return (menuSelected + (totalLines * (pageNumber - 1))); }
                    Console.SetCursorPosition(0, menuSelected + startLine);
                    Console.Write(' ');
                    if (input == ConsoleKey.UpArrow) { menuSelected--; }
                    if (input == ConsoleKey.DownArrow) { menuSelected++; }
                    if (menuSelected < 1 ) { lastPage = true; pageNumber--; }
                    if (menuSelected > possibleOptions) { nextPage = true; pageNumber++; }
                    if (pageNumber < 1) { pageNumber = pages; }
                    if (pageNumber > pages) { pageNumber = 1; }
                    if (lastPage || nextPage)
                    {
                        possibleOptions = options.Length - (totalLines * (pageNumber - 1));
                        if (totalLines < possibleOptions)
                        {
                            possibleOptions = totalLines;
                        }
                        if (lastPage) { menuSelected = possibleOptions; }
                        if (nextPage) { menuSelected = 1; }
                        for (var i = 0; i < possibleOptions; i++)
                        {
                            Console.SetCursorPosition(2, startLine + i + 1);
                            Console.Write("                       ");
                            Console.SetCursorPosition(2, startLine + i + 1);
                            Console.Write(options[i]);
                        }
                    }
                    Console.SetCursorPosition(0, menuSelected + startLine);
                    Console.Write('>');
                    oldPageNumber = pageNumber;
                }
            }
        }
        
        public void UpdateExp()
        {
            int expNeeded = Convert.ToInt16(Math.Pow(1.05, Player.Instance.Level) * 1000);
            if (Player.Instance.Experience > expNeeded)
            {
                Player.Instance.Level++;
                Player.Instance.Experience = Player.Instance.Experience - expNeeded;
            }
        }
        public void NumberInventoryItems()
        {
            //integer, 10^5? * most important etc
        }
        public int InventorySelect()
        {
            int menuSelected = 0;
            return menuSelected;
        }
        public void InventoryToStringArray(int[][] input, string[] output)
        {
            var general = new General();
            var weapons = new Weapon();
             for (var i = 0; i < input.Length; i++)
            {
                output[i] = general.Prefixes[input[i][4]][input[i][5]] + weapons.Weapons[input[i][1]][input[i][2]][input[i][3]];
            }
        }

        public int SpaceLeft()
        {
            int spaceLeft = Player.Instance.InventorySpace;
            for (var i = 1; i < 51; i++)
            {
                if (Player.Instance.Inventory[1][i][1] != 0)
                {
                    spaceLeft--;
                }
                if (Player.Instance.Inventory[2][i][1] != 0)
                {
                    spaceLeft = spaceLeft - Player.Instance.Inventory[1][i][7];
                }
            }
            return spaceLeft;
        }
    }
    class Quicksort //From http://www.softwareandfinance.com/CSharp/QuickSort_Recursive.html
    {
        static public int Partition(int[] numbers, int left, int right)
        {
            var pivot = numbers[left];
            while (true)
            {
                while (numbers[left] < pivot)
                    left++;

                while (numbers[right] > pivot)
                    right--;

                if (left < right)
                {
                    int temp = numbers[right];
                    numbers[right] = numbers[left];
                    numbers[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        static public void QuickSort_Recursive(int[] arr, int left, int right)
        {
            // For Recursion
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                    QuickSort_Recursive(arr, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort_Recursive(arr, pivot + 1, right);
            }
        }
    }
}
