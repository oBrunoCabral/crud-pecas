using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;


namespace CRUDPecas
{
    /// <summary>
    /// Interação lógica para NewWindow.xam
    /// </summary>
    public partial class NewWindow : Window
    {
        public NewWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgPieces.ItemsSource = Utils.LoadJson();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NewPieceFieldsCheck();
        }

        private void NewPieceFieldsCheck()
        {
            if (txtCode.Text == "" || txtDescription.Text == "" || txtPieceHeight.Text == "" || txtPieceWidth.Text == "")
            {
                MessagePrompt("Favor preencher todos os campos antes de salvar");
            }
            else
            {       
                double number;

                if (!double.TryParse(txtPieceWidth.Text, out number) || !double.TryParse(txtPieceHeight.Text, out number))
                {
                    MessagePrompt("Altura e Largura precisam ser valores numéricos");
                }
                else
                {
                    Piece p = new Piece();
                    Dimension d = new Dimension();
                    p.Code = txtCode.Text;
                    p.Description = txtDescription.Text;
                    d.Width = txtPieceWidth.Text;
                    d.Height = txtPieceHeight.Text;
                    p.Dimension = d;

                    Operations.AddOrEditPiece(p);
                    dgPieces.ItemsSource = Utils.LoadJson();
                }

            }

        }

        private void dgPieces_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgPieces.SelectedIndex >= 0)
            {
                Piece p = (Piece)dgPieces.SelectedItem;
                txtCode.Text = p.Code;
                txtDescription.Text = p.Description;
                txtPieceWidth.Text = p.Dimension.Width;
                txtPieceHeight.Text = p.Dimension.Height;
            }
        }

        private void dgPieces_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dgPieces.SelectedIndex >= 0)
                {
                    Piece p = (Piece)dgPieces.SelectedItem;
                    Operations.RemovePiece(p);
                    dgPieces.ItemsSource = Utils.LoadJson();
                }
            }
        }

        private void btnCleanFilters_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtPieceHeight.Text = "";
            txtPieceWidth.Text = "";
            txtSearchTerms.Text = "";
            dgPieces.ItemsSource = Utils.LoadJson();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = txtSearchTerms.Text;
            switch (cmbTypeOfSearch.SelectedIndex)
            {
                case 0:
                    dgPieces.ItemsSource = Operations.FindByCode(searchTerm);
                    break;
                case 1:
                    dgPieces.ItemsSource = Operations.FindByDescription(searchTerm);
                    break;
                case 2:
                    dgPieces.ItemsSource = Operations.FindByDimension(searchTerm);
                    break;
                default:
                    break;
            }
            if (cmbTypeOfSearch.SelectedIndex == 0)
            {
                var code = txtSearchTerms.Text;
                dgPieces.ItemsSource = Operations.FindByCode(code);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void MessagePrompt(string message)
        {
            if (message != "")
            {
                MessageBox.Show(message);
            }

        }
    }
}
