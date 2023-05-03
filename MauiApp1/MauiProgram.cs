using LiteDB;
using MauiApp1.Repositories;
using Microsoft.Extensions.Logging;

namespace MauiApp1;

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
			})
			.RegisterDatabaseAndRespositories();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	public static MauiAppBuilder RegisterDatabaseAndRespositories(this MauiAppBuilder mauiAppbuilder)
	{
		mauiAppbuilder.Services.AddSingleton<LiteDatabase>(
			options =>
			{
				return new LiteDatabase($"Filename={AppSettings.DatabasePath} ;Connection=Shared");
			});

		mauiAppbuilder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

		return mauiAppbuilder;

    }

}
