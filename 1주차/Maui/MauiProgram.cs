namespace Notes;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp() // EntryPoint
	{
		var builder = MauiApp.CreateBuilder(); // MauiAppBuilder 초기화

        builder
			.UseMauiApp<App>()  // <> 괄호 안에 들어간 형식의 Application 을 사용한다
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build(); // MauiApp을 빌드한다.
	}
}
