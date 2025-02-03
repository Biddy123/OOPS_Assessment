using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Resources;

namespace OOPS_Assessment
{
    internal class GameMap
    {
        private RectangleShape[,] map;
        public enum MovieDirections { Left, Right, Up, Down };
        private int player_x = 3;
        private int player_y = 3;

        

        public GameMap()
        {
            map = new RectangleShape[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[y, x] = new RectangleShape();
                    map[y, x].Size = new Vector2f(60, 60);
                    map[y, x].Position = new Vector2f(x * 60f, y * 60f);
                    map[y, x].Texture = new Texture("resources/img_floor.jpg");
                }
            }

            map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
        }

        public void DrawMap(RenderWindow window)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    window.Draw(map[y, x]);
                }
            }
        }

        public void MovePlayer(MovieDirections direction)
        {
            // Remove the player from the current tile
            map[player_y, player_x].Texture = new Texture("resources/img_floor.jpg");

            // Calculate the potential new position
            int newPlayerX = player_x;
            int newPlayerY = player_y;

            switch (direction)
            {
                case MovieDirections.Up:
                    newPlayerY--;
                    break;
                case MovieDirections.Down:
                    newPlayerY++;
                    break;
                case MovieDirections.Left:
                    newPlayerX--;
                    break;
                case MovieDirections.Right:
                    newPlayerX++;
                    break;
            }

            // Check if the new position is within bounds
            if (newPlayerX >= 0 && newPlayerX < 10 && newPlayerY >= 0 && newPlayerY < 10)
            {
                // Update the player's position if it's within bounds
                player_x = newPlayerX;
                player_y = newPlayerY;
            }

            // Place the player on the new tile
            map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
        }

    }
}
