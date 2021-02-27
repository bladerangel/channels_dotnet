namespace api.Models
{
  public class Channel
  {
    public string Name { get; set; }
    public string Primary { get; set; }
    public string Secondary { get; set; }
    public string Logo { get; set; }

    public Channel(string Name, string Primary, string Secondary, string Logo)
    {
      this.Name = Name;
      this.Primary = Primary;
      this.Secondary = Secondary;
      this.Logo = Logo;
    }
  }
}