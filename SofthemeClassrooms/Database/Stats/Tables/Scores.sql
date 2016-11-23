CREATE TABLE [Stats].[Scores] (
    [TestId]    VARCHAR (10) NOT NULL,
    [StudentId] VARCHAR (10) NOT NULL,
    [Score]     TINYINT      NOT NULL,
    CONSTRAINT [PK_Scores] PRIMARY KEY CLUSTERED ([TestId] ASC, [StudentId] ASC),
    CONSTRAINT [CHK_Scores_Score] CHECK ([Score]>=(0) AND [Score]<=(100)),
    CONSTRAINT [FK_Scores_Tests] FOREIGN KEY ([TestId]) REFERENCES [Stats].[Tests] ([TestId])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_TestId_Score]
    ON [Stats].[Scores]([TestId] ASC, [Score] ASC);

