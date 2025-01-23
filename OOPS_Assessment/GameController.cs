using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard;

namespace OOPS_Assessment
{
    internal class GameController
    {
        private RenderWindow gameWindow;
        private GameMap gameMap;
        
        public GameController() 
        {
            gameWindow = new RenderWindow(new VideoMode(800, 600), "Assessment Project", Styles.Close);
            gameWindow.Closed += OnClosed;
            gameWindow.KeyReleased += OnKeyReleased;

            gameMap = new GameMap();
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            gameWindow.Close();
        }

        private void OnKeyReleased(object? sender, KeyEventArgs e)
        {
            switch(e.Code)
            {
                case Keyboard.Key.Up or Keyboard.Key.W:
                    Console.WriteLine("Up was pressed");
                    gameMap.MovePlayer(GameMap.MovieDirections.Up);
                    break;
                case Keyboard.Key.Down or Keyboard.Key.S:
                    Console.WriteLine("Down was pressed");
                    gameMap.MovePlayer(GameMap.MovieDirections.Down);
                    break;
                case Keyboard.Key.Left or Keyboard.Key.A:
                    Console.WriteLine("Left was pressed");
                    gameMap.MovePlayer(GameMap.MovieDirections.Left);
                    break;
                case Keyboard.Key.Right or Keyboard.Key.D:
                    gameMap.MovePlayer(GameMap.MovieDirections.Right);
                    Console.WriteLine("Right was pressed");
                    break;
            }
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
            // Draw game here
            // Clear
            gameWindow.Clear(new Color(255,0,255));

            gameMap.DrawMap(gameWindow);


            // Display
            gameWindow.Display();
        }
    }
}