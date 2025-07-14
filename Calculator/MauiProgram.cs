using Calculator.BusinessLogic.Operations;
using Calculator.BusinessLogic.Services;
using Calculator.ViewModel;
using Calculator.ViewModel.Calculations;
using Calculator.ViewModel.Commands;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace Calculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            AddDependencies(builder);

            return builder.Build();
        }

        private static void AddDependencies(MauiAppBuilder builder)
        {
            builder.Services.AddScoped<CalculatorMainPage>();
            builder.Services.AddScoped<CalculatorViewModel>();

            builder.Services.AddTransient<IOperation, Addition>();
            builder.Services.AddTransient<IOperation, Division>();
            builder.Services.AddTransient<IOperation, Multiplication>();
            builder.Services.AddTransient<IOperation, SquareRoot>();
            builder.Services.AddTransient<IOperation, Subtraction>();
            builder.Services.AddTransient<CalculationFactory>();

            builder.Services.AddTransient<ICalculationService, CalculationService>();
        }
    }
}
