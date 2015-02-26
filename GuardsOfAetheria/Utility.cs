using System;

namespace GuardsOfAetheria
{
    class Utility
    {
        public int SelectOption(string[] options, bool showMenu = false )
        {
            var menuSelected = 1;
            int pageScroll = -1;
            if (Options.Instance.CurrentSettings[0] == Options.Settings.Pages) { pageScroll = 0; }
            if (Options.Instance.CurrentSettings[0] == Options.Settings.Scroll) { pageScroll = 1; }
            var startLine = Console.CursorTop;
            decimal numberOfLines = 22 - startLine;
            var totalLines = Convert.ToInt32(numberOfLines);
            var pages = Convert.ToInt32(Math.Round(((options.Length + numberOfLines) / (2 * numberOfLines)), MidpointRounding.AwayFromZero));
            var pageNumber = 1;
            var scroll = options.Length - 1 > totalLines ? true : false;
            var possibleOptions = options.Length - (totalLines * (pageNumber - 1));
            if (totalLines < possibleOptions) { possibleOptions = totalLines; }
            for (var i = 0; i < possibleOptions; i++)
            {
                Console.SetCursorPosition(2, startLine + i + 1);
                Console.Write("                       ");
                Console.SetCursorPosition(2, startLine + i + 1);
                Console.Write(options[i]);
            }
            if (pages > 1 && pageScroll == 0)
            {
                Console.SetCursorPosition(50, startLine);
                Console.Write("{0}/{1}", pageNumber, pages);
            }
            if (showMenu)
            {
                Console.SetCursorPosition(10, 23);
                Console.Write("[M] Player Menu");
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
                    switch (input)
                    {
                        case ConsoleKey.UpArrow:
                            menuSelected--;
                            break;
                        case ConsoleKey.DownArrow:
                            menuSelected++;
                            break;
                        case ConsoleKey.M:
                            if (showMenu) { PlayerMenu(); }
                            break;
                    }
                    if (scroll)
                    {
                        if (menuSelected < 1) { lastPage = true; }
                        if (menuSelected > possibleOptions) { nextPage = true; }
                    }
                    if (!scroll)
                    {
                        if (menuSelected < 1) { menuSelected = possibleOptions; }
                        if (menuSelected > possibleOptions) { menuSelected = 1; }
                    }
                    if (lastPage || nextPage)
                    {
                        if (pageScroll == 0)
                        {
                            possibleOptions = options.Length - (totalLines * (pageNumber - 1));
                            if (totalLines < possibleOptions) { possibleOptions = totalLines; }
                            if (lastPage) { menuSelected = possibleOptions; pageNumber--; }
                            if (nextPage) { menuSelected = 1; pageNumber++; }
                            if (pageNumber < 1) { pageNumber = pages; }
                            if (pageNumber > pages) { pageNumber = 1; }
                            if (pages > 1)
                            {
                                Console.SetCursorPosition(50, startLine);
                                Console.Write("{0}/{1}", pageNumber, pages);
                            }
                        }
                        if (pageScroll == 1)
                        {
                            if (pageNumber < 1) { pageNumber = options.Length; }
                            if (pageNumber > options.Length) { pageNumber = 1; }
                            if (lastPage) { menuSelected = 1; pageNumber--; }
                            if (nextPage) { menuSelected = possibleOptions; pageNumber++; }
                        }
                        for (var i = 0; i < possibleOptions; i++)
                        {
                            Console.SetCursorPosition(2, startLine + i + 1);
                            Console.Write("                       "); // TODO: make it better
                            Console.SetCursorPosition(2, startLine + i + 1);
                            if (pageScroll == 0)
                            {
                                Console.Write(options[i + (totalLines * (pageNumber - 1))]);
                            }
                            if (pageScroll != 1) continue;
                            var currentNumber = pageNumber + i;
                            if (currentNumber - 1 > options.Length)
                            {
                                currentNumber = currentNumber - options.Length;
                            }
                            Console.Write(options[currentNumber]);
                        }
                    }
                    Console.SetCursorPosition(0, menuSelected + startLine);
                    Console.Write('>');
                }
            }
        }

        public void PlayerMenu()
        {
            Console.Clear();
            Console.WriteLine("Inventory");
            SelectOption(Player.Instance.InventoryNameAll);
            //TODO: Add more options
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
        public void PrioritiseInventoryItems()
        {
            Player.Instance.InventoryOld = Player.Instance.Inventory;
            //TODO: integer, 10^5? * most important etc
            //TODO: quicksort
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
                if (Player.Instance.Inventory[3][i][1] != 0)
                {
                    spaceLeft--;
                }
                if (Player.Instance.Inventory[4][i][1] != 0)
                {
                    spaceLeft--;
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
