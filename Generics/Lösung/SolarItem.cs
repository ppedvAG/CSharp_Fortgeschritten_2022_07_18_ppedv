namespace SolarSystemSolution;

public record SolarItem(string Description, SolarItemType Type);

public enum SolarItemType { Star, Planet, Trabant }