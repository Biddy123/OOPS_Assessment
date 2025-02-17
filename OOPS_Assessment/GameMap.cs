using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace OOPS_Assessment
{
    internal class GameMap
    {
        private RectangleShape[,] map;
        public enum MovieDirections { Left, Right, Up, Down };

        // Single declarations (removed duplicates)
        private List<Vector2i> cratePositions;
        private List<Vector2i> diamondPositions;
        private Vector2i playerPosition; // For storing the player's position

        SoundBuffer a;
        Sound jump;
        SoundBuffer b;
        Sound create_move;

        public GameMap(int[,] levelLayout)
        {
            // Load sounds (if needed)
            a = new SoundBuffer("resources/jump.ogg");
            jump = new Sound(a);
            b = new SoundBuffer("resources/moving_create.ogg");
            create_move = new Sound(b);

            map = new RectangleShape[10, 10];
            cratePositions = new List<Vector2i>();
            diamondPositions = new List<Vector2i>();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[y, x] = new RectangleShape
                    {
                        Size = new Vector2f(60, 60),
                        Position = new Vector2f(x * 60f, y * 60f),
                        Texture = new Texture("resources/img_floor.jpg") // Default texture
                    };

                    // Set textures based on level layout
                    switch (levelLayout[y, x])
                    {
                        case 1: // Player
                            playerPosition = new Vector2i(x, y);
                            map[y, x].Texture = new Texture("resources/img_player.jpg");
                            break;

                        case 2: // Crate
                            cratePositions.Add(new Vector2i(x, y));
                            map[y, x].Texture = new Texture("resources/img_crate.jpg");
                            break;

                        case 3: // Diamond
                            diamondPositions.Add(new Vector2i(x, y));
                            map[y, x].Texture = new Texture("resources/img_diamond.jpg");
                            break;

                        case 4: // Wall (optional)
                            map[y, x].Texture = new Texture("resources/img_wall.jpg");
                            break;

                        default:
                            break; // Floor is already set
                    }
                }
            }
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
            if (diamondPositions.Contains(playerPosition))
            {
                map[playerPosition.Y, playerPosition.X].Texture = new Texture("resources/img_diamond.jpg");
            }
            else
            {
                map[playerPosition.Y, playerPosition.X].Texture = new Texture("resources/img_floor.jpg");
            }

            // Calculate the potential new player position
            Vector2i newPlayerPosition = playerPosition;
            Vector2i newCratePosition;

            switch (direction)
            {
                case MovieDirections.Up:
                    newPlayerPosition.Y--;
                    break;
                case MovieDirections.Down:
                    newPlayerPosition.Y++;
                    break;
                case MovieDirections.Left:
                    newPlayerPosition.X--;
                    break;
                case MovieDirections.Right:
                    newPlayerPosition.X++;
                    break;
            }

            // Check if the new player position is within bounds
            if (newPlayerPosition.X >= 0 && newPlayerPosition.X < 10 && newPlayerPosition.Y >= 0 && newPlayerPosition.Y < 10)
            {
                // Check if player is trying to move into a crate
                int crateIndex = cratePositions.FindIndex(c => c == newPlayerPosition);
                if (crateIndex != -1)
                {
                    // Calculate the new crate position based on direction
                    newCratePosition = cratePositions[crateIndex];
                    switch (direction)
                    {
                        case MovieDirections.Up:
                            newCratePosition.Y--;
                            break;
                        case MovieDirections.Down:
                            newCratePosition.Y++;
                            break;
                        case MovieDirections.Left:
                            newCratePosition.X--;
                            break;
                        case MovieDirections.Right:
                            newCratePosition.X++;
                            break;
                    }

                    // Check if the new crate position is within bounds and not blocked
                    if (newCratePosition.X >= 0 && newCratePosition.X < 10 &&
                        newCratePosition.Y >= 0 && newCratePosition.Y < 10 &&
                        !cratePositions.Contains(newCratePosition))
                    {
                        // Store old crate position (for texture restoration)
                        Vector2i oldCratePos = cratePositions[crateIndex];

                        // Move the crate
                        cratePositions[crateIndex] = newCratePosition;

                        // Update crate texture at the new position
                        if (diamondPositions.Contains(newCratePosition))
                        {
                            map[newCratePosition.Y, newCratePosition.X].Texture = new Texture("resources/img_filled_crate.jpg");
                        }
                        else
                        {
                            map[newCratePosition.Y, newCratePosition.X].Texture = new Texture("resources/img_crate.jpg");
                        }

                        // Restore the texture at the old crate position
                        if (diamondPositions.Contains(oldCratePos))
                        {
                            map[oldCratePos.Y, oldCratePos.X].Texture = new Texture("resources/img_diamond.jpg");
                        }
                        else
                        {
                            map[oldCratePos.Y, oldCratePos.X].Texture = new Texture("resources/img_floor.jpg");
                        }

                        // Move player into the crate's former spot
                        playerPosition = newPlayerPosition;
                    }
                }
                else
                {
                    // Move player if not blocked by a crate
                    playerPosition = newPlayerPosition;
                }

                // Place the player on the new tile
                map[playerPosition.Y, playerPosition.X].Texture = new Texture("resources/img_player.jpg");
            }
        }

        public bool IsGameOver()
        {
            // Game over if all crates are on diamonds
            foreach (Vector2i crate in cratePositions)
            {
                if (!diamondPositions.Contains(crate))
                    return false;
            }
            return true;
        }

        public bool IsCrateOnDiamond()
        {
            // Returns true if every crate is on a diamond
            foreach (var crate in cratePositions)
            {
                if (!diamondPositions.Contains(crate))
                    return false;
            }
            return true;
        }
    }
}
