//--- Aura Script -----------------------------------------------------------
// Alby Int 2
//--- Description -----------------------------------------------------------
// Script for Alby Intermediate for Two.
//---------------------------------------------------------------------------

using Aura.Channel.World.Dungeons;

[DungeonScript("tircho_alby_middle_2_dungeon")]
public class AlbyIntTwoDungeonScript : DungeonScript
{
	public override void OnBoss(Dungeon dungeon)
	{
		dungeon.AddBoss(170107, 2); // Lycanthrope
		dungeon.AddBoss(110102, 5); // Gorgon

		foreach (var member in dungeon.Party)
		{
			var cutscene = new Cutscene("bossroom_lycan", member);
			cutscene.AddActor("leader", member);
			cutscene.AddActor("player1", member); // TODO: Party
			cutscene.AddActor("#lycan", 170107);
			cutscene.AddActor("#gorgon", 110102);
			cutscene.Play();
		}
	}

	public override void OnCleared(Dungeon dungeon)
	{
		var rnd = RandomProvider.Get();

		for (int i = 0; i < dungeon.Party.Count; ++i)
		{
			var member = dungeon.Party[i];
			var treasureChest = new TreasureChest();

			if (i == 0)
			{
				// Enchanted item
				Item item = null;
				switch (rnd.Next(2))
				{
					case 0: item = Item.CreateEnchanted(40043, prefix: 20105); break; // Maltreat Rolling Pin
					case 1: item = Item.CreateEnchanted(40001, prefix: 20612); break; // Careful Wooden Stick
				}
				treasureChest.Add(item);
			}

			treasureChest.AddGold(rnd.Next(3360, 6272)); // Gold
			treasureChest.Add(GetRandomTreasureItem(rnd)); // Random item

			dungeon.AddChest(treasureChest);

			member.GiveItemWithEffect(Item.CreateKey(70028, "chest"));
		}
	}

	DropData[] drops;
	public Item GetRandomTreasureItem(Random rnd)
	{
		if (drops == null)
		{
			drops = new DropData[]
			{
				new DropData(itemId: 62004, chance: 4, amountMin: 2, amountMax: 4), // Magic Powder
				new DropData(itemId: 51102, chance: 4, amountMin: 2, amountMax: 4), // Mana Herb
				new DropData(itemId: 51013, chance: 5, amountMin: 2, amountMax: 4), // Stamina 50 Potion
				new DropData(itemId: 51008, chance: 5, amountMin: 2, amountMax: 4), // MP 50 Potion
				new DropData(itemId: 51003, chance: 5, amountMin: 2, amountMax: 4), // HP 50 Potion
				new DropData(itemId: 63116, chance: 5, amount: 1, expires: 480), // Alby Intermediate Fomor Pass for One
				new DropData(itemId: 63117, chance: 3, amount: 1, expires: 480), // Alby Intermediate Fomor Pass for Two
				new DropData(itemId: 63118, chance: 3, amount: 1, expires: 480), // Alby Intermediate Fomor Pass for Four
			};
		}

		return Item.GetRandomDrop(rnd, drops);
	}
}
