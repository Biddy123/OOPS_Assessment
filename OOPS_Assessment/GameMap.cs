using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Resources;
using SFML.Audio;

namespace OOPS_Assessment
{
    internal class GameMap
    {
        private RectangleShape[,] map;
        public enum MovieDirections { Left, Right, Up, Down };
        private int player_x = 3;
        private int player_y = 3;
        private int crate_x = 5;
        private int crate_y = 5;
        private int diamond_x = 8;
        private int diamond_y = 8;

        SoundBuffer a;
        Sound jump;
        SoundBuffer b;
        Sound create_move;


        public GameMap()
        {
            a = new SoundBuffer("resources/jump.ogg");
            jump = new Sound(a);
            b = new SoundBuffer("resources/moving_create.ogg");
            create_move = new Sound(b);

            map = new RectangleShape[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[y, x] = new RectangleShape();
                    map[y, x].Size = new Vector2f(60, 60);
                    map[y, x].Position = new Vector2f(x * 60f, y * 60f);
                    map[y, x].Texture = new Texture("resources/img_floor.jpg"); // Default texture
                }
            }

            // Set crate texture
            map[crate_y, crate_x].Texture = new Texture("resources/img_crate.jpg");

            // Set player texture
            map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");

            // Set diamond texture
            map[diamond_x, diamond_y].Texture = new Texture("resources/img_diamond.jpg");
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
            // Restore the floor or diamond texture from the player's previous position
            if (player_x == diamond_x && player_y == diamond_y)
            {
                // If the player was standing on the diamond, restore the diamond texture
                map[player_y, player_x].Texture = new Texture("resources/img_diamond.jpg");
            }
            else
            {
                // Otherwise, restore the floor texture
                map[player_y, player_x].Texture = new Texture("resources/img_floor.jpg");
            }

            // Calculate the potential new position
            int newPlayerX = player_x;
            int newPlayerY = player_y;
            int newCrateX = crate_x;
            int newCrateY = crate_y;

            switch (direction)
            {
                case MovieDirections.Up:
                    newPlayerY--;
                    newCrateY = crate_y - 1;
                    break;
                case MovieDirections.Down:
                    newPlayerY++;
                    newCrateY = crate_y + 1;
                    break;
                case MovieDirections.Left:
                    newPlayerX--;
                    newCrateX = crate_x - 1;
                    break;
                case MovieDirections.Right:
                    newPlayerX++;
                    newCrateX = crate_x + 1;
                    break;
            }

            // Check if the new player position is within bounds
            if (newPlayerX >= 0 && newPlayerX < 10 && newPlayerY >= 0 && newPlayerY < 10)
            {
                // Check if player is trying to move into the crate
                if (newPlayerX == crate_x && newPlayerY == crate_y)
                {
                    // Check if the crate's new position is within bounds
                    if (newCrateX >= 0 && newCrateX < 10 && newCrateY >= 0 && newCrateY < 10)
                    {
                        // Restore diamond texture if crate moves off the diamond
                        if (crate_x == diamond_x && crate_y == diamond_y)
                        {
                            map[diamond_y, diamond_x].Texture = new Texture("resources/img_diamond.jpg");
                        }

                        // Move the crate to the new position
                        crate_x = newCrateX;
                        crate_y = newCrateY;

                        // Check if the new crate position is over the diamond
                        if (crate_x == diamond_x && crate_y == diamond_y)
                        {
                            // Leave the diamond visible underneath the crate
                            map[crate_y, crate_x].Texture = new Texture("resources/img_crate.jpg");
                        }
                        else
                        {
                            // Normal crate texture
                            map[crate_y, crate_x].Texture = new Texture("resources/img_crate.jpg");
                        }

                        // Move player
                        player_x = newPlayerX;
                        player_y = newPlayerY;
                    }
                }
                else
                {
                    // Move player if not blocked by crate
                    player_x = newPlayerX;
                    player_y = newPlayerY;
                }
            }

            // Place the player on the new tile
            map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
        }

    }
}
