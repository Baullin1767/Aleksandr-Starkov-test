public interface IAdapter 
{
    void InitializeGame();  // Инициализация игры
    void ProcessTurn();     // Обработка хода (ход игрока и противника)
    void RestartGame();     // Перезапуск игры
}

