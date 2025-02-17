using SFML.Graphics;
using SFML.Window;
using System;

namespace OOPS_Assessment
{
    internal class GameController
    {
        private RenderWindow gameWindow;
        private GameMap gameMap;
        private SideView sideView;
        private int i = 0;
        private bool isGameOver = false; // Game over flag
        private Text gameOverText;       // Text for "Game Over"
        private Font basicFont;
        private LevelManager levelManager;
        private int currentLevel;

        public GameController()
        {
            gameWindow = new RenderWindow(new VideoMode(800, 600), "Assessment Project", Styles.Close);
            gameWindow.Closed += OnClosed;
            gameWindow.KeyReleased += OnKeyReleased;

            sideView = new SideView();

            // Load the font for "Game Over" text
            basicFont = new Font("resources/basic_font.ttf");
            gameOverText = new Text("GAME OVER", basicFont, 50)
            {
                Position = new SFML.System.Vector2f(200, 250),
                FillColor = Color.Red
            };

            levelManager = new LevelManager();
            currentLevel = 0;

            // Load the first level from LevelManager
            gameMap = new GameMap(levelManager.GetLevel(currentLevel));
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            gameWindow.Close();
        }

        private void LoadNextLevel()
        {
            currentLevel++;
            if (currentLevel < levelManager.LevelCount)
            {
                gameMap = new GameMap(levelManager.GetLevel(currentLevel));
            }
            else
            {
                Console.WriteLine("No more levels!");
            }
        }

        private void OnKeyReleased(object? sender, KeyEventArgs e)
        {
            if (isGameOver) return; // Do nothing if the game is over

            switch (e.Code)
            {
                case Keyboard.Key.Up or Keyboard.Key.W:
                    gameMap.MovePlayer(GameMap.MovieDirections.Up);
                    break;
                case Keyboard.Key.Down or Keyboard.Key.S:
                    gameMap.MovePlayer(GameMap.MovieDirections.Down);
                    break;
                case Keyboard.Key.Left or Keyboard.Key.A:
                    gameMap.MovePlayer(GameMap.MovieDirections.Left);
                    break;
                case Keyboard.Key.Right or Keyboard.Key.D:
                    gameMap.MovePlayer(GameMap.MovieDirections.Right);
                    break;
            }

            // Check if the game is over
            if (gameMap.IsCrateOnDiamond())
            {
                isGameOver = true; // Set game over state
                LoadNextLevel(1); // Move to the next level once the current level is complete
            }
        }


        public void Run()
        {
            while (gameWindow.IsOpen)
            {
                // Process events
                gameWindow.DispatchEvents();

                // Update game (if needed)
                UpdateGame();

                // Render game
                RenderGame();
            }

            gameWindow.Close();
        }

        private void UpdateGame()
        {
            // Add any game update logic here if needed
        }

        private void RenderGame()
        {
            // Clear the window with a background color
            gameWindow.Clear(new Color(255, 0, 255));

            // Draw the game map and side view
            gameMap.DrawMap(gameWindow);
            sideView.DrawSideView(gameWindow);

            // If the game is over, display "GAME OVER" text
            if (isGameOver)
            {
                gameWindow.Draw(gameOverText);
            }

            // Display the rendered frame
            gameWindow.Display();
        }

        // Existing LoadNextLevel method without parameters
        // Load next level without parameters

        // Load next level with a specific level index (for example)
        private void LoadNextLevel(int levelIndex)
        {
            currentLevel = levelIndex;
            if (currentLevel < levelManager.LevelCount)
            {
                gameMap = new GameMap(levelManager.GetLevel(currentLevel));
                isGameOver = false;
            }
            else
            {
                Console.WriteLine("No more levels!");
            }
        }



    }
}
