using System;
using Microsoft.Data.Sqlite;
using System.IO;
using FishGame.Models;
using FishGame.Constants;

namespace FishGame.Services;

public class FishDataService
{
    private readonly string _dbPath;
    private readonly string _connectionString;

    public FishDataService(string dbFileName = "fishgame.db")
    {
        _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);
        _connectionString = $"Data Source={_dbPath}";
        InitializeDatabase();
    }

    public void InitializeDatabase()
    {
        // Microsoft.Data.Sqlite creates the database file if it doesn't exist
        // don't need SQLiteConnection.CreateFile, that's for System.Data.SQLite

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = @"
            CREATE TABLE IF NOT EXISTS Fish (
                Id TEXT PRIMARY KEY,
                UserId TEXT,
                X INTEGER,
                Y INTEGER,
                IsFacingRight INTEGER,
                Fullness INTEGER,
                Color INTEGER,
                LastUpdated TEXT
            )";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void SaveFish(Fish fish, string userId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = @"
            INSERT OR REPLACE INTO Fish
            (UserId, Id, X, Y, IsFacingRight, Fullness, Color, LastUpdated)
            VALUES (@UserId, @Id, @X, @Y, @IsFacingRight, @Fullness, @Color, @LastUpdated)";


            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", fish.Id);
                command.Parameters.AddWithValue("@X", fish.X);
                command.Parameters.AddWithValue("@Y", fish.Y);
                command.Parameters.AddWithValue("@IsFacingRight", fish.isFacingRight ? 1 : 0);
                command.Parameters.AddWithValue("@Fullness", fish.fullness);
                command.Parameters.AddWithValue("@Color", (int)fish.Color);
                command.Parameters.AddWithValue("@LastUpdated", DateTime.UtcNow.ToString("o"));
                command.ExecuteNonQuery();
            }
        }
    }

    public Fish? LoadFish(string userId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            string sql = @"SELECT * FROM Fish WHERE UserId = @UserId";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int x = reader.GetInt32(reader.GetOrdinal("X"));
                        int y = reader.GetInt32(reader.GetOrdinal("Y"));
                        bool isFacingRight = reader.GetInt32(reader.GetOrdinal("IsFacingRight")) == 1;
                        ConsoleColor color = (ConsoleColor)reader.GetInt32(reader.GetOrdinal("Color"));

                        var fish = new Fish(x, y, isFacingRight, color);
                        fish.Id = Guid.Parse(reader.GetString(reader.GetOrdinal("Id")));
                        fish.fullness = reader.GetInt32(reader.GetOrdinal("Fullness"));

                        // Process hunger based on elapsed time
                        string lastUpdatedStr = reader.GetString(reader.GetOrdinal("LastUpdated"));
                        if (!string.IsNullOrEmpty(lastUpdatedStr))
                        {
                            DateTime lastUpdated = DateTime.Parse(lastUpdatedStr);
                            UpdateFullnessBasedOnElapsedTime(fish, lastUpdated);
                        }

                        return fish;
                    }
                }
            }
        }
        return null;
    }

    private void UpdateFullnessBasedOnElapsedTime(Fish fish, DateTime lastUpdated)
    {
        DateTime now = DateTime.UtcNow;
        TimeSpan elapsed = now - lastUpdated;

        // Decrease fullness by GameConstants.Hunger.DecreaseAmount every GameConstants.Hunger.DecreaseInterval minutes
        int decreaseAmount = (int)(elapsed.TotalMinutes / GameConstants.Hunger.DecreaseInterval) * GameConstants.Hunger.DecreaseAmount;

        if (decreaseAmount > 0)
        {
            for (int i = 0; i < decreaseAmount / GameConstants.Hunger.DecreaseAmount; i++)
            {
                fish.DecreaseFullness();
            }
        }
    }
}