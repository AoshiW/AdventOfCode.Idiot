using System.Net.Http.Headers;

namespace AdventOfCode.Client;

public class AdventOfCodeClientOptions
{
    public required string Session {  get; set; }
    public required ProductInfoHeaderValue ContactInformation { get; set; }
}
