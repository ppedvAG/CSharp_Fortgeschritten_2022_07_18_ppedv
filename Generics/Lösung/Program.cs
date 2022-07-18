using SolarSystemSolution;

Node<SolarItem> sunNode = new Node<SolarItem>(new SolarItem("Sonne", SolarItemType.Star));

Node<SolarItem> merkurNode = new Node<SolarItem>(new SolarItem("Merkur", SolarItemType.Planet));
merkurNode.SetParentNode(sunNode); //Variante 1 -> ParentNode setzen

Node<SolarItem> venusNode = new Node<SolarItem>(new SolarItem("Venus", SolarItemType.Planet));

Node<SolarItem> earthNode = new Node<SolarItem>(new SolarItem("Erde", SolarItemType.Planet));
earthNode.AddChild(new SolarItem("Mond", SolarItemType.Trabant));

Node<SolarItem> marsNode = new Node<SolarItem>(new SolarItem("Mars", SolarItemType.Planet));
marsNode.AddChild(new SolarItem("Phobos", SolarItemType.Trabant));
marsNode.AddChild(new SolarItem("Deimos", SolarItemType.Trabant));

Node<SolarItem> jupiterNode = new Node<SolarItem>(new SolarItem("Jupiter", SolarItemType.Planet));
jupiterNode.AddChild(new SolarItem("Europa", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Io", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Ganymed", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Kallisto", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Metis", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Adrastea", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Amalthea", SolarItemType.Trabant));
jupiterNode.AddChild(new SolarItem("Thebe", SolarItemType.Trabant));

Node<SolarItem> saturnNode = new Node<SolarItem>(new SolarItem("Saturn", SolarItemType.Planet));
saturnNode.AddChild(new SolarItem("Titan", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Rhea", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Dione", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Tethys", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Japetus", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Telesto", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Calypso", SolarItemType.Trabant));
saturnNode.AddChild(new SolarItem("Tethys", SolarItemType.Trabant));

Node<SolarItem> uranusNode = new Node<SolarItem>(new SolarItem("Uranus", SolarItemType.Planet));
uranusNode.AddChild(new SolarItem("Miranda", SolarItemType.Trabant));
uranusNode.AddChild(new SolarItem("Ariel", SolarItemType.Trabant));
uranusNode.AddChild(new SolarItem("Umbriel", SolarItemType.Trabant));
uranusNode.AddChild(new SolarItem("Titania", SolarItemType.Trabant));
uranusNode.AddChild(new SolarItem("Oberon", SolarItemType.Trabant));
uranusNode.AddChild(new SolarItem("Triton", SolarItemType.Trabant));

Node<SolarItem> neptunNode = new Node<SolarItem>(new SolarItem("Neptun", SolarItemType.Planet));
neptunNode.AddChild(new SolarItem("Triton", SolarItemType.Trabant));
neptunNode.AddChild(new SolarItem("Proteus", SolarItemType.Trabant));
neptunNode.AddChild(new SolarItem("Halimede", SolarItemType.Trabant));
neptunNode.AddChild(new SolarItem("Nereid", SolarItemType.Trabant));
neptunNode.AddChild(new SolarItem("Naiad", SolarItemType.Trabant));
neptunNode.AddChild(new SolarItem("Thalasaa", SolarItemType.Trabant));

sunNode.AddChild(merkurNode);
sunNode.AddChild(venusNode);
sunNode.AddChild(earthNode);
sunNode.AddChild(marsNode);
sunNode.AddChild(jupiterNode);
sunNode.AddChild(saturnNode);
sunNode.AddChild(uranusNode);
sunNode.AddChild(neptunNode);

DisplaySolarSystem(sunNode);


void DisplaySolarSystem(Node<SolarItem> solarNode, int depth = 0)
{
    if (solarNode.Item.Type == SolarItemType.Star)
        Console.WriteLine($"Sonne: {solarNode.Item.Description}");

    if (solarNode.Item.Type == SolarItemType.Planet)
        Console.WriteLine($"\tPlanet: {solarNode.Item.Description} - kreist um {solarNode.ParentNode.Item.Description}");

    if (solarNode.Item.Type == SolarItemType.Trabant)
        Console.WriteLine($"\t\t -Mond: {solarNode.Item.Description} - kreist um {solarNode.ParentNode.Item.Description}");

    if (solarNode.Item.Type != SolarItemType.Trabant)
    {
        foreach (Node<SolarItem> node in solarNode.Childrens)
        {
            DisplaySolarSystem(node);
        }
    }
}





