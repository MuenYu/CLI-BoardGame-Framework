using Sharprompt;


namespace Game
{
    class GameSaver
    {
        public void Save(Game g)
        {
            string json = SerUtil.ToJson(g);
            try
            {
                string path = Prompt.Input<string>("Please enter a path to save the game progress");
                IOUtil.Write2File(path, json);
                Console.WriteLine("Save successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
