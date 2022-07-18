namespace Test
{
	internal record Person
	(
		[field: NonSerialized] int ID, //Attribut auf Record Property
		[field: NonSerialized] string Name, //nicht möglich ohne field:
		Person Vorgesetzter
	)
	{
		//Code in Record hinzufügen

		//Generiert auch Deconstruct, GetHashCode, Equals, ...
	}
}
