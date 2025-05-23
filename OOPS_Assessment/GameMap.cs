﻿using SFML.Graphics;
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
        private int player_x, player_y;
        private int crate_x, crate_y;
        private int diamond_x, diamond_y;
        private List<(int x, int y)> crates = new List<(int x, int y)>();
        private List<(int x, int y)> diamonds = new List<(int x, int y)>();
        private List<(int x, int y)> walls = new List<(int x, int y)>(); // New list to track wall positions
        private List<(int x, int y)> filledCrates = new List<(int x, int y)>(); // New list to track filled crates
        private int currentLevel = 0;

        public GameMap()
        {
            map = new RectangleShape[10, 10];
            LoadLevel(currentLevel);

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
            map[diamond_y, diamond_x].Texture = new Texture("resources/img_diamond.jpg");
        }

        public void LoadLevel(int level)
        {
            // Reset lists and single-point variables
            crates.Clear();
            diamonds.Clear();
            walls.Clear(); // Clear walls list
            filledCrates.Clear(); // Clear filled crates list
            crate_x = -1;
            crate_y = -1;
            diamond_x = -1;
            diamond_y = -1;

            int[,] levelData = Levels.LevelMaps[level];
            map = new RectangleShape[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[y, x] = new RectangleShape(new Vector2f(60, 60))
                    {
                        Position = new Vector2f(x * 60f, y * 60f)
                    };

                    switch (levelData[y, x])
                    {
                        case 1: // Wall
                            walls.Add((x, y)); // Add wall position to the list
                            map[y, x].Texture = new Texture("resources/img_wall.jpg");
                            break;
                        case 2: // Player
                            player_x = x;
                            player_y = y;
                            map[y, x].Texture = new Texture("resources/img_player.jpg");
                            break;
                        case 3: // Crate
                            crates.Add((x, y));
                            // Set the first crate as the primary crate for backward compatibility
                            if (crate_x == -1)
                            {
                                crate_x = x;
                                crate_y = y;
                            }
                            map[y, x].Texture = new Texture("resources/img_crate.jpg");
                            break;
                        case 4: // Diamond
                        case 5: // Additional Diamond
                            diamonds.Add((x, y));
                            // Set the first diamond as the primary diamond for backward compatibility
                            if (diamond_x == -1)
                            {
                                diamond_x = x;
                                diamond_y = y;
                            }
                            map[y, x].Texture = new Texture("resources/img_diamond.jpg");
                            break;
                        default: // Floor
                            map[y, x].Texture = new Texture("resources/img_floor.jpg");
                            break;
                    }
                }
            }

            // Debug: Verify positions
            Console.WriteLine($"Level {level + 1} Loaded");
            Console.WriteLine($"Player: ({player_x}, {player_y})");
            Console.WriteLine($"Primary Crate: ({crate_x}, {crate_y})");
            Console.WriteLine($"Primary Diamond: ({diamond_x}, {diamond_y})");
            Console.WriteLine($"Walls count: {walls.Count}");
        }

        public bool IsCrateOnDiamond()
        {
            return crate_x == diamond_x && crate_y == diamond_y;
        }

        public bool AreAllCratesOnDiamonds()
        {
            // Check if each crate is on a diamond
            foreach (var crate in crates)
            {
                if (!diamonds.Contains(crate))
                {
                    return false;
                }
            }
            return true;
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

        public (bool success, bool crateMoved) MovePlayer(MovieDirections direction)
        {
            bool crateMoved = false;

            // Restore the floor or diamond texture from the player's previous position
            RestorePlayerPreviousPosition();

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

            // Check if the new player position is within bounds
            if (newPlayerX < 0 || newPlayerX >= 10 || newPlayerY < 0 || newPlayerY >= 10)
            {
                // Out of bounds, can't move
                map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
                return (false, crateMoved);
            }

            // Check if player is trying to move into a wall
            if (walls.Contains((newPlayerX, newPlayerY)))
            {
                // Cannot move into walls, so return without changing player position
                map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
                return (false, crateMoved);
            }

            // Check if player is trying to move into a crate
            var crateIndex = crates.FindIndex(c => c.x == newPlayerX && c.y == newPlayerY);
            if (crateIndex != -1)
            {
                // Check if this crate is a filled crate (already on a diamond)
                if (filledCrates.Contains((newPlayerX, newPlayerY)))
                {
                    // Cannot move filled crates
                    map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
                    return (false, crateMoved);
                }

                // Try to move the crate
                if (TryMoveCrate(crateIndex, direction))
                {
                    // Move player
                    player_x = newPlayerX;
                    player_y = newPlayerY;
                    crateMoved = true;
                    map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
                    return (true, crateMoved);
                }
                else
                {
                    // Couldn't move crate, so can't move player
                    map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
                    return (false, crateMoved);
                }
            }
            else
            {
                // Move player if not blocked by crate or wall
                player_x = newPlayerX;
                player_y = newPlayerY;
                map[player_y, player_x].Texture = new Texture("resources/img_player.jpg");
                return (true, crateMoved);
            }
        }

        private bool TryMoveCrate(int crateIndex, MovieDirections direction)
        {
            var crate = crates[crateIndex];
            int newCrateX = crate.x;
            int newCrateY = crate.y;

            switch (direction)
            {
                case MovieDirections.Up:
                    newCrateY--;
                    break;
                case MovieDirections.Down:
                    newCrateY++;
                    break;
                case MovieDirections.Left:
                    newCrateX--;
                    break;
                case MovieDirections.Right:
                    newCrateX++;
                    break;
            }

            // Check if new crate position is valid
            if (newCrateX < 0 || newCrateX >= 10 || newCrateY < 0 || newCrateY >= 10)
            {
                return false; // Out of bounds
            }

            // Check if the new position is not occupied by a wall
            if (walls.Contains((newCrateX, newCrateY)))
            {
                return false; // Cannot push crate into walls
            }

            // Check if the new position is not occupied by another crate
            if (crates.Any(c => c.x == newCrateX && c.y == newCrateY))
            {
                return false; // Cannot push crate into another crate
            }

            // Update crate position
            var oldCrate = crates[crateIndex];
            crates[crateIndex] = (newCrateX, newCrateY);

            // Restore previous tile texture
            map[oldCrate.y, oldCrate.x].Texture =
                diamonds.Contains((oldCrate.x, oldCrate.y))
                    ? new Texture("resources/img_diamond.jpg")
                    : new Texture("resources/img_floor.jpg");

            // Update new crate position texture
            if (diamonds.Contains((newCrateX, newCrateY)))
            {
                map[newCrateY, newCrateX].Texture = new Texture("resources/img_filled_crate.jpg");
                // Add to filled crates list
                filledCrates.Add((newCrateX, newCrateY));
            }
            else
            {
                map[newCrateY, newCrateX].Texture = new Texture("resources/img_crate.jpg");
            }

            return true;
        }

        private void RestorePlayerPreviousPosition()
        {
            // Restore the floor or diamond texture from the player's previous position
            if (diamonds.Contains((player_x, player_y)))
            {
                // If the player was standing on the diamond, restore the diamond texture
                map[player_y, player_x].Texture = new Texture("resources/img_diamond.jpg");
            }
            else
            {
                // Otherwise, restore the floor texture
                map[player_y, player_x].Texture = new Texture("resources/img_floor.jpg");
            }
        }
    }
}