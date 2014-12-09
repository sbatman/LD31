namespace LD31
{
    class Program
    {
        /// <summary>
        /// The main entry point for the game. Make sure to release ALL resource before it exits.
        /// </summary>
        private static void Main()
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}
