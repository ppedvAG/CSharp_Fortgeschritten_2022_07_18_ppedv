namespace Test
{
	internal record Person
	(
		[field: NonSerialized] int ID, //Attribut auf Record Property
		[NonSerialized] string Name //nicht möglich ohne field:
	)
	{
		//Code in Record hinzufügen

		//Generiert auch Deconstruct, GetHashCode, Equals, ...
	}
}
