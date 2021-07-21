using FujiFilm214.FujiFilm;

namespace FujiFilm214
{
    public class Program
    {
        private static void Main(string[] args)
        {
            FujiFilmController fuji = new();
            fuji.InitializeTriggers(Configuration.ProgramStatusDbConnection);
        }
    }
}
