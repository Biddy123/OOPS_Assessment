﻿using OOPS_Assessment;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class GameController
{
    private RenderWindow gameWindow;
    private GameMap gameMap;
    private SideView sideView;
    private int currentLevel = 0;
    private Font font;
    private Text gameOverText;
    private int i = 0;

    public GameController()
    {
        gameWindow = new RenderWindow(new VideoMode(800, 600), "Assessment Project", Styles.Close);
        gameWindow.Closed += OnClosed;
        gameWindow.KeyPressed += OnKeyPressed;

        gameMap = new GameMap();
        sideView = new SideView();

        // Start background music with the audio manager
        AudioManager.Instance.PlayBackgroundMusic();

        font = new Font("resources/basic_font.ttf");
        gameOverText = new Text("Game Over!\n\nPress ENTER to Restart\nPress ESC to Quit", font, 30);
        gameOverText.Position = new Vector2f(250, 250);
        gameOverText.FillColor = Color.Red;
        ShowWelcomeScreen();
        RenderGame();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        gameWindow.Close();
    }

    private void OnKeyPressed(object? sender, KeyEventArgs e)
    {
        bool moveSuccessful = false; // Flag to track if a move was successful
        GameMap.MovieDirections? direction = null;

        switch (e.Code)
        {
            case Keyboard.Key.Up or Keyboard.Key.W:
                Console.WriteLine("Up was pressed");
                direction = GameMap.MovieDirections.Up;
                break;
            case Keyboard.Key.Down or Keyboard.Key.S:
                Console.WriteLine("Down was pressed");
                direction = GameMap.MovieDirections.Down;
                break;
            case Keyboard.Key.Left or Keyboard.Key.A:
                Console.WriteLine("Left was pressed");
                direction = GameMap.MovieDirections.Left;
                break;
            case Keyboard.Key.Right or Keyboard.Key.D:
                Console.WriteLine("Right was pressed");
                direction = GameMap.MovieDirections.Right;
                break;
        }

        if (direction.HasValue)
        {
            // We pass the direction and get back both success status and whether a crate was moved
            (moveSuccessful, bool crateMoved) = gameMap.MovePlayer(direction.Value);

            // Play appropriate sound
            if (moveSuccessful)
            {
                if (crateMoved)
                    AudioManager.Instance.PlayJumpSound();

                i++;
                Console.WriteLine(i);
                sideView.UpdateTitle(i);
            }
        }

        if (gameMap.AreAllCratesOnDiamonds())
        {
            currentLevel++;
            if (currentLevel < Levels.LevelMaps.Length)
            {
                sideView.UpdateTitle(0);
                i = 0;
                gameMap.LoadLevel(currentLevel);
                Console.WriteLine($"Loading level {currentLevel + 1}"); // Debug log
            }
            else
            {
                // Show game over screen
                sideView.UpdateTitle(0);
                i = 0;
                RenderGameOver();
                Console.WriteLine("All levels completed!"); // Debug log
            }
        }
    }

    public void Run()
    {
        while (gameWindow.IsOpen)
        {
            gameWindow.DispatchEvents();
            UpdateGame();
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
        gameWindow.Clear(new Color(255, 0, 255));
        gameMap.DrawMap(gameWindow);
        sideView.DrawSideView(gameWindow);
        gameWindow.Display();
    }

    private void RenderGameOver()
    {
        gameWindow.Clear(Color.Black);
        gameWindow.Draw(gameOverText);
        gameWindow.Display();
        AudioManager.Instance.StopBackgroundMusic();

        while (gameWindow.IsOpen)
        {
            gameWindow.DispatchEvents(); // Allow events like keypresses

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                gameWindow.Close(); // Close the game when Escape is pressed
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                currentLevel = 0; // Reset to level 1
                gameMap.LoadLevel(currentLevel);
                AudioManager.Instance.PlayBackgroundMusic();
                return; // Exit the loop and restart the game
            }
        }
    }

    private void ShowWelcomeScreen()
    {
        Text welcomeText = new Text("Welcome to Sokoban!\n\nPress ENTER to Start\nPress ESC to Quit", font, 30);
        welcomeText.Position = new Vector2f(150, 200);
        welcomeText.FillColor = Color.White;

        while (gameWindow.IsOpen)
        {
            gameWindow.Clear(Color.Black);
            gameWindow.Draw(welcomeText);
            gameWindow.Display();

            gameWindow.DispatchEvents(); // Process window events

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                return; // Start the game
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                gameWindow.Close(); // Quit the game
            }
        }
    }
}