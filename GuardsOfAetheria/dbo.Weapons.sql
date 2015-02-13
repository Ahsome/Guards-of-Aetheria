CREATE TABLE [dbo].[Equipment] (
    [Name]          VARCHAR (MAX) NOT NULL,
    [Class]         VARCHAR (10)  NOT NULL,
    [MainAttNeeded] INT           NOT NULL,
    [Rarity]        VARCHAR (10)  NOT NULL,
    [Type] VARCHAR(MAX) NOT NULL, 
    CHECK ([Class]='Magic' OR [Class]='Ranged' OR [Class]='Melee'),
    CHECK ([Rarity]='Supreme' OR [Rarity]='Legendary' OR [Rarity]='Epic' OR [Rarity]='Rare' OR [Rarity]='Uncommon' OR [Rarity]='Common'), 
	CHECK ([Type]='Broadsword' OR [Type]='Dagger' OR [Type]='Stiletto' OR [Type]='Scythe?' OR [Type]='Staff' OR [Type]='Wand' OR [Type]='Crossbow' OR [Type]='Longbow')
);

