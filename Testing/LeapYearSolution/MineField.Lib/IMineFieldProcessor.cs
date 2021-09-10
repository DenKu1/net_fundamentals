namespace MineField.Lib
{
    public interface IMineFieldProcessor
    {
        char[,] GetHintField(char[,] mineField);
    }
}