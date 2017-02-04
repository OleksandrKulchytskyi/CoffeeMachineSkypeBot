CREATE TABLE [dbo].[ApprovalQueue] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [nvarchar](100) NOT NULL,
    [UserName] [nvarchar](100),
    [Approved] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.ApprovalQueue] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserActitvity] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Cups] [int] NOT NULL,
    [UserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UserActitvity] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[UserActitvity]([UserId])
CREATE TABLE [dbo].[User] (
    [Id] [int] NOT NULL IDENTITY,
    [UserName] [nvarchar](100) NOT NULL,
    [UserDescription] [nvarchar](100),
    [CreatedOn] [datetime] NOT NULL,
    [Active] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY ([Id])
)
ALTER TABLE [dbo].[UserActitvity] ADD CONSTRAINT [FK_dbo.UserActitvity_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)

INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201702041520525_InitialContext', N'CoffeeMachine.Infrastructure.Migrations.Configuration',  0x1F8B0800000000000400ED5ACD6EE33610BE17E83B083A1659CBC95EDAC0DE45D6498AA09B388DB38BDE025A1A3B44295225A9C0C6A24FD6431FA9AFD0A1FE45C9B6FC9364B158E462939C5F7E33C399F8BF7FFE1DBC5F84CC7902A9A8E043F7B8D7771DE0BE08289F0FDD58CFDEFCECBE7FF7E30F838B205C389FF3736FCD39A4E46AE83E6A1D9D7A9EF21F2124AA17525F0A2566BAE78BD02381F04EFAFD5FBCE3630F90858BBC1C677017734D4348BEE0D791E03E443A26EC5A04C054B68E3B9384AB7343425011F161E88EC46C06704DFC47CAA177C56792282D635FC7125CE78C51823A4D80CD5C87702E34D1A8F1E92705132D059F4F225C20EC7E19019E9B11A620B3E4B43CDED5A8FE8931CA2B0973567EACB408B76478FC36F3926793EFE46BB7F022FAF102FDAD97C6EAC49743F72C8AA47822ECF71862F4802DF174C4A4396DFB3BBD9F5E8DFAC8693B7354A0054165FEF058CCCC2D0D39C45A1276E4DCC65346FDDF60792FFE043EE4316355AD516FDCAB2DE0D2AD141148BDBC835966CB55E03A5E9DCEB3090BB20A4D6AE015D76F4F5CE70685932983021415674CB490F02B70904443704BB406C90D0F48DCDA906EC942F0C9521EE21083CB75AEC9E223F0B97EC4B0EB63385DD20504F94AA6C3274E31169108110E2D3A6E966B3E1D40F27A41291CA030F183100C08DFA0F0C02B51B916ABC68E331F179F12676F89D51AF577AC6EB8CA73A4CAA599CFF734DC1E78A338529B54DE26643AF2B8214F749E78A1859BEBDC014B36D5238DD2129160E3C18003B1410175BE9422BC132CA3A96C3DDC1339078D1A89F6FD8988A5BF0FC27702F6773CBF5C0EDC01C2E7A07C49A3B4863F73121E49301E1AF3BDC33741356C97CCD7855F35C0F60BC23CC85605611EA45D82F04C29E1D34499CA8D5515A99B77C103678356E905E736E11D63D0D108C30CC5E335BB76388DF93930D0E01866E6953822CA2741D3B56842D0559B2251D5B5A914D0BA5A3F35A461748334E14518BECCF1754D28D7CD5440B94F23C2D63BC522EB98438CC985007BE71C22E026FCD73BA08BE4BCCC34A51742AC8BD8E49B8157C15533E7238D460A9079D056B3B7D984856E2904A867560B54960C6C3418E613D0EDAFFB3200324058FBDE7A6E397C52F4349859E8EAC04CADE0D220AEF8B2CEA1962F2AA756A414FB7A37C772A176A17103239B43D06252FAD00EEFBA9D2DC9ABC04DD91A7B696F9CF7D0DE8A267A704DA2082B4EA5A9CE569C49D651BF996CDF6086290FCF572D7D66A16D21098B389983B56BE01FC025954A63AD2253628ACA28085B8EB544C90A98E522DB02A179833908732AF3B92532AD39436F2DDBD2C3976874681255F288A9A061037D32F8208CC89687D348B038E4AB13E76AEA3CD95539AC4A80EBB9A40F2A9B4FBADA9D53D935563995AB4D4E03CF726C236D376EB45147EB30E904A25AEC1E0C43F5B4B93D8636D03F0F86D2F6B04A9FAE74E790F686550EE9CA73E1F81531D35677F600CB8E1879C9F47298C4D0E89C6C86B5CD2DA057764835FC95CB5B24AFAC47AAA5AE6CED8541D87823D8470AE9C55BC17A130CB2FADC65FA6E15ECF488EBA07B9E68608AF564A934843D73A037F98B8D18457BCB03D784D319289D8E1DDC93FEF18935B6FF7A46E89E5201DB668EFEE203146A5CBB7144B2D7F08D3F11E93F12D91C59EC398EEEC27897E9F394EAE7993C7F1BB75B9DF006F859EF3BE14DB4DC0B62098746D37B85FDF762E87E49484E9DAB3F1E52AA23672C31919C3A7DE7EF435EF43772BFBB84D93E03CD438571637EB93338EBF3CBDDD3C17E13C2D79C0756C631AF30067CA9A1DFBAEEE9AB9CF5D9C38E830EF1D2971886CD54E0ADA760DF6DCEA7BB0DFADA046E3F0B5C390A5CC5FE9586848D69DEC68960C3952D6DFBB34C029BAF79C472E51737184C8ACE4B16E6F7371CFC1A8A8B33D8888A3C982C8DF22356FABD064D30779333A9E98CF81AB77D502AF9AFD767C2623C72114E21B8E2E35847B14693219CB29A334C50AE939F8C3BEB3A0FC6495952873001D5A4A6FC8CF98798B2A0D0FBB2A578AC6061A23DABEAE62EB5A9EEF365C1E946F08E8C32F71549EA1EC288213335E61362CADCF6BA21063FC29CF8CBBC295BCD64F345D4DD3E38A7642E49A8321E253D7E450C07E1E2DDFFBEFFD8D078260000 , N'6.1.3-40302')

