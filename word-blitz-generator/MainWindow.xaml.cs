using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Path = System.Windows.Shapes.Path;

namespace word_blitz_generator
{
    public partial class MainWindow : Window
    {
        public List<string> DictionaryList;
        public IDictionary<int, bool> HistoryList = new Dictionary<int, bool>();
        public DataGrid dataGrid;

        public MainWindow()
        {
            InitializeComponent();
            LoadDictionary();

            dataGrid = (DataGrid) FindName("WordsList");
        }

        public void LoadDictionary()
        {
           DictionaryList = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory+"/../../words3.txt").ToList();
        }

        private void Letter_OnChanged(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text.Length == 0)
            {
                return;
            }

            if (e.Source is TextBox s)
            {
                s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }

            e.Handled = true;
        }

        private async void LastLetter_OnKeyUp(object sender, KeyEventArgs e)
        {
            await launchSearch();
        }

        private async Task launchSearch()
        {
   
            var letter1 = ((TextBox) FindName("Letter1"))?.Text;
            var letter2 = ((TextBox) FindName("Letter2"))?.Text;
            var letter3 = ((TextBox) FindName("Letter3"))?.Text;
            var letter4 = ((TextBox) FindName("Letter4"))?.Text;
            var letter5 = ((TextBox) FindName("Letter5"))?.Text;
            var letter6 = ((TextBox) FindName("Letter6"))?.Text;
            var letter7 = ((TextBox) FindName("Letter7"))?.Text;
            var letter8 = ((TextBox) FindName("Letter8"))?.Text;
            var letter9 = ((TextBox) FindName("Letter9"))?.Text;
            var letter10 = ((TextBox)FindName("Letter10"))?.Text;
            var letter11 = ((TextBox)FindName("Letter11"))?.Text;
            var letter12 = ((TextBox)FindName("Letter12"))?.Text;
            var letter13 = ((TextBox)FindName("Letter13"))?.Text;
            var letter14 = ((TextBox)FindName("Letter14"))?.Text;
            var letter15 = ((TextBox)FindName("Letter15"))?.Text;
            var letter16 = ((TextBox)FindName("Letter16"))?.Text;

            string[,] inputedElements = new string[,]
            {
                { letter1 , letter2, letter3, letter4 },
                { letter5 , letter6, letter7, letter8 },
                { letter9 , letter10, letter11, letter12 },
                { letter13 , letter14, letter15, letter16 }
            };

            for (var line = 0; line < 4; line++)
            {
                for (var index = 0; index < 4; index++)
                {
                    var currentWord = "";

                    HistoryList.Clear();
                    HistoryList.Add(0, false);
                    HistoryList.Add(1, false);
                    HistoryList.Add(2, false);
                    HistoryList.Add(3, false);
                    HistoryList.Add(4, false);
                    HistoryList.Add(5, false);
                    HistoryList.Add(6, false);
                    HistoryList.Add(7, false);
                    HistoryList.Add(8, false);
                    HistoryList.Add(9, false);
                    HistoryList.Add(10, false);
                    HistoryList.Add(11, false);
                    HistoryList.Add(12, false);
                    HistoryList.Add(13, false);
                    HistoryList.Add(14, false);
                    HistoryList.Add(15, false);

                    await recurse(inputedElements, currentWord, HistoryList, line, index);
                }
            }
        }

        private async Task recurse(string[,] array, string currentWord, IDictionary<int, bool> history, int i, int j)
        {
            history = new Dictionary<int, bool>(history);
            history[(i * 4) + j] = true;

            currentWord += array[i, j];

            if (currentWord.Length > 6)
            {
                return;
            }

            if (currentWord.Length > 4 && DictionaryList.Contains(currentWord, StringComparer.OrdinalIgnoreCase))
            {
                dataGrid.Items.Add(new { WORD = currentWord, POINTS = "0" });
            }

            if (j != 3) {//east
		        if (!history[(i * 4) + j + 1])
                    await recurse(array, currentWord, history, i, j + 1);
		        if (i != 0) {//north-east
			        if (!history[(i * 4) + j - 3])
                        await recurse(array, currentWord, history, i - 1, j + 1);
		        }
	        }
	        if (j != 0) {//west
		        if (!history[(i * 4) + j - 1])
                    await recurse(array, currentWord, history, i, j - 1);
		        if (i != 3) {//south-west
			        if (!history[(i * 4) + j + 3])
                        await recurse(array, currentWord, history, i + 1, j - 1);
		        }
	        }
	        if (i != 0) {//north
		        if (!history[(i * 4) + j - 4])
                    await recurse(array, currentWord, history, i - 1, j);
		        if (j != 0) {//north-west
			        if (!history[(i * 4) + j - 5])
                        await recurse(array, currentWord, history, i - 1, j - 1);
		        }
	        }
	        if (i != 3) {//south
		        if (!history[(i * 4) + j + 4])
                    await recurse(array, currentWord, history, i + 1, j);
		        if (j != 3) {//south-east
			        if (!history[(i * 4) + j + 5])
                        await recurse(array, currentWord, history, i + 1, j + 1);
		        }
	        }
        }
    }
}
