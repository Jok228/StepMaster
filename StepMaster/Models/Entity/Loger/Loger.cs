namespace StepMaster.Models.Entity.Loger
{
    public class Loger
    {
        public static async Task WriterLoger(Day day, string name)
        {
            string path = "LogerDay/Days.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                var date = DateTime.Now.Date;
                await writer.WriteLineAsync(name);
                await writer.WriteLineAsync("Дата: " + date.Date.ToString());
                await writer.WriteLineAsync("Время: " + date.TimeOfDay.ToString());
                await writer.WriteLineAsync(day.email);
                await writer.WriteLineAsync(day.calories.ToString());
                await writer.WriteLineAsync(day.steps.ToString());
                await writer.WriteLineAsync(day.distance.ToString());
                await writer.WriteLineAsync();

            }
        }
    }
}
