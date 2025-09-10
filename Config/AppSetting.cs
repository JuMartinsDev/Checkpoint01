namespace checkpoint01.Config
{
    public class AppSetting
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string SqlServer { get; set; }
        public string Redis { get; set; }
    }
}
