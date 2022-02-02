namespace FantasyMAST;

public partial class App : FantasyMvvm.FantasyBootStarter
{
    //public App()
    //{
    //    InitializeComponent();

    //    MainPage = new NavigationPage( new MainPage());
    //}
    protected override string CreateShell()
    {
        return "LoginPage";
    }
}
