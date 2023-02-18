namespace Notes;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell(); // MainPage의 마크업의 루트 태그가 굳이 Shell 이 아니어도 된다.
	}
}
