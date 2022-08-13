namespace AssasultCube___Hack
{
    public static class Program
    {
        public static bool OpenDetailFormOnClose { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OpenDetailFormOnClose = false;

            Application.Run(new Form2());

            if (OpenDetailFormOnClose)
            {
                Application.Run(new Form3());
            }
        }
    }
}