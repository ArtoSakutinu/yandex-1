
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int MoneyCount;
        public int PlayerExp;
        public string characters;
        public string saved_bgs;

        public int money_per_click;
        public int money_per_sec;

        public int passive_cost;
        public int per_click_cost;

        public int character_cost;
        public int bg_cost;

        public int charEq;
        public int backgroundEq;

        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            MoneyCount = 0;
            PlayerExp = 0;
            characters = "";
            saved_bgs = "";

            money_per_click = 1;
            money_per_sec = 1;

            passive_cost = 75;
            per_click_cost = 50;

            character_cost = 75;
            bg_cost = 50;

            charEq = 0;
            backgroundEq = 0;
        }
    }
}
