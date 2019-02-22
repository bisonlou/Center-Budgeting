

/****** Object:  View [dbo].[CentreExpenses]    Script Date: 01/12/2016 11:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CentreExpenses]
AS
SELECT        TOP (100) PERCENT dbo.tblCenterEnt.uidCenterID, dbo.tblCenters.strLvl1, dbo.tblCenters.strLvl2, dbo.tblCenters.strLvl3, dbo.tblAccounts.strDesc, 
                         SUM(dbo.tblJournalEnt.decDebits - dbo.tblJournalEnt.decCredits) AS TOTAL, dbo.tblJournalEnt.intGLNumber, dbo.tblJournal.dteJournalDate
FROM            dbo.tblCenterEnt INNER JOIN
                         dbo.tblJournalEnt ON dbo.tblCenterEnt.lngJourID = dbo.tblJournalEnt.lngJourID AND dbo.tblCenterEnt.intLineNum = dbo.tblJournalEnt.intLineNum INNER JOIN
                         dbo.tblCenters ON dbo.tblCenterEnt.uidCenterID = dbo.tblCenters.uidCenterID INNER JOIN
                         dbo.tblAccounts ON dbo.tblJournalEnt.intGLNumber = dbo.tblAccounts.intGLNumber INNER JOIN
                         dbo.tblJournal ON dbo.tblJournalEnt.lngJourID = dbo.tblJournal.lngJourID
WHERE        (dbo.tblAccounts.intAccType = 5)
GROUP BY dbo.tblCenters.strLvl1, dbo.tblCenters.strLvl2, dbo.tblCenters.strLvl3, dbo.tblAccounts.strDesc, dbo.tblCenterEnt.uidCenterID, dbo.tblJournalEnt.intGLNumber, 
                         dbo.tblJournal.dteJournalDate
HAVING        (dbo.tblJournal.dteJournalDate >= CONVERT(DATETIME, '2016-07-01 00:00:00', 102)) AND (dbo.tblJournal.dteJournalDate <= CONVERT(DATETIME, 
                         '2017-06-30 00:00:00', 102))

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[9] 2[37] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblCenterEnt"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblJournalEnt"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 136
               Right = 497
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblCenters"
            Begin Extent = 
               Top = 6
               Left = 535
               Bottom = 136
               Right = 736
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblAccounts"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblJournal"
            Begin Extent = 
               Top = 138
               Left = 289
               Bottom = 268
               Right = 475
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 4125
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Ali' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CentreExpenses'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'as = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CentreExpenses'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CentreExpenses'
GO


