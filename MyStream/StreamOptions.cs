namespace MyStream;

public class StreamOptions
{
    public string InputTopic { get; set; }
    public string OutputTopic { get; set; }
    public Dictionary<string, dynamic> ConnectionSettings { get; set; }
}