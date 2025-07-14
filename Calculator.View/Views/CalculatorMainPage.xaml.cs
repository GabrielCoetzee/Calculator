using Calculator.ViewModel;

namespace Calculator
{
    public partial class CalculatorMainPage : ContentPage
    {
        public CalculatorMainPage(CalculatorViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
