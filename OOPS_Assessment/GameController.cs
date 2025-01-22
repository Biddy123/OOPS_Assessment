using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace OOPS_Assessment
{
    internal class GameController
    {
        private RenderWindow gameWindow;
        
        public GameController() 
        {
            gameWindow = new RenderWindow(new VideoMode(800, 600), "Assessment Project", Styles.Close);
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

            // Draw all game elements


            // Display
            gameWindow.Display();
        }
    }
}
