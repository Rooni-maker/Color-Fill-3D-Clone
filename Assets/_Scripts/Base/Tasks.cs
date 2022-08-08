//Shady
using System.Threading.Tasks;

public static class Tasks
{
    public static async Task Delay(float delay) => await System.Threading.Tasks.Task.Delay((int)(delay * 1000));
}//class end
