namespace Calculator
{
    public partial class App : Application
    {
        public CalculatorMainPage _mainPage { get; set; }

        public App(CalculatorMainPage mainPage)
        {
            InitializeComponent();

            _mainPage = mainPage;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            const int newHeight = 600;
            const int newWidth = 450;

            var newWindow = new Window(_mainPage)
            {
                Height = newHeight,
                Width = newWidth
            };

            return newWindow;
        }
    }
}