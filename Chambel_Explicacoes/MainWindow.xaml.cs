using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;

namespace Chambel_Explicacoes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string pasta = Directory.GetCurrentDirectory();
        public MainWindow()
        {
            InitializeComponent();
            //Carregar dados das explicacoes
            RefreshDaLista();
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            AdicionarPag ADD = new AdicionarPag();
            ADD.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Pagar_Click(object sender, RoutedEventArgs e)
        {
            string[] data = File.ReadAllLines(pasta + "\\DATA.EXPLIC");
            foreach (string linha in data)
            {
                string dia = listBox.SelectedItem.ToString().Replace('/', '-');
                if (linha.Contains(dia) == true)
                {
                    //Apresenta os dados dessa linha
                    string textotodo = File.ReadAllText(pasta + "\\DATA.EXPLIC");
                    string textobom = textotodo.Replace(linha, linha.Replace("n", "p"));
                    File.WriteAllText(pasta + "\\DATA.EXPLIC", textobom);
                }
                else
                {
                    //nada
                }
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                //chopa mos
            }
            else
            {

                string[] data = File.ReadAllLines(pasta + "\\DATA.EXPLIC");
                foreach (string linha in data)
                {
                    string dia = listBox.SelectedItem.ToString().Replace('/', '-');
                    if (linha.Contains(dia) == true)
                    {
                        //Apresenta os dados dessa linha
                        string[] elementos = linha.Split('-');
                        string mes = elementos[1];
                        string ano = elementos[2];
                        string preco = elementos[3];
                        string pago = elementos[4];
                        Dia.Content = listBox.SelectedItem.ToString();
                        Valor.Content = elementos[3] + " €";
                        if (pago == "n")
                        {
                            Pago.Content = "Nao";
                        }
                        else
                        {
                            Pago.Content = "Sim";
                        }
                    }
                    else
                    {
                        //nao faz nada
                    }
                }
            }
        }

        private void RefreshDaLista()
        {
            listBox.Items.Clear();
            string[] data = File.ReadAllLines(pasta + "\\DATA.EXPLIC");
            foreach (string linha in data)
            {
                string[] elementos = linha.Split('-');
                string dia = elementos[0];
                string mes = elementos[1];
                string ano = elementos[2];
                string preco = elementos[3];
                string pago = elementos[4];
                listBox.Items.Add(dia + "/" + mes + "/" + ano);
            }
            listBox.SelectedIndex = 0;
        }

        private void RefreshDaLista(object sender, RoutedEventArgs e)
        {
            RefreshDaLista();
        }
    }
}
