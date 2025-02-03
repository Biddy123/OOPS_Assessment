using SFML.Graphics;
using SFML.Window;

namespace OOPS_Assessment
{
    internal class GameController
    {
        private RenderWindow gameWindow;
        private GameMap gameMap;
        private SideView sideView;
        private int i = 0;

        public GameController()
        {
            gameWindow = new RenderWindow(new VideoMode(800, 600), "Assessment Project", Styles.Close);
            gameWindow.Closed += OnClosed;
            gameWindow.KeyReleased += OnKeyReleased;

            gameMap = new GameMap();
            sideView = new SideView();
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            gameWindow.Close();
        }

        private void OnKeyReleased(object? sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Up or Keyboard.Key.W:
                    Console.WriteLine("Up was pressed");
                    gameMap.MovePlayer(GameMap.MovieDirections.Up);
                    i++;
                    Console.WriteLine(i);
                    break;
                case Keyboard.Key.Down or Keyboard.Key.S:
                    Console.WriteLine("Down was pressed");
                    gameMap.MovePlayer(GameMap.MovieDirections.Down);
                    i++;
                    Console.WriteLine(i);
                    break;
                case Keyboard.Key.Left or Keyboard.Key.A:
                    Console.WriteLine("Left was pressed");
                    gameMap.MovePlayer(GameMap.MovieDirections.Left);
                    i++;
                    Console.WriteLine(i);
                    break;
                case Keyboard.Key.Right or Keyboard.Key.D:
                    gameMap.MovePlayer(GameMap.MovieDirections.Right);
                    Console.WriteLine("Right was pressed");
                    i++;
                    Console.WriteLine(i);
                    break;
            }

            // Update the side view text with the new value of 'i'
            sideView.UpdateTitle(i);
        }

        public void Run()
        {
            while (gameWindow.IsOpen)
            {
                // Gather User input
                gameWindow.DispatchEvents();

                // Update game
                UpdateGame();

                // render game
                RenderGame();
            }

            gameWindow.Close();
        }

        private void UpdateGame()
        {
            // Update game here
        }

        private void RenderGame()
        {
            // Clear
            gameWindow.Clear(new Color(255, 0, 255));

            // Draw map and side view
            gameMap.DrawMap(gameWindow);
            sideView.DrawSideView(gameWindow);

            // Display the window
            gameWindow.Display();
        }
    }
}
