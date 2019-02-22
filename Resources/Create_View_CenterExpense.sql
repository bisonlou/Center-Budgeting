
/****** Object:  View [dbo].[CentreExpense]    Script Date: 01/12/2016 11:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CentreExpense]
AS
SELECT        TOP (100) PERCENT dbo.tblCenterEnt.uidCenterID, dbo.tblCenters.strLvl1, dbo.tblCenters.strLvl2, dbo.tblCenters.strLvl3, dbo.tblAccounts.strDesc, 
                         SUM(dbo.tblJournalEnt.decDebits - dbo.tblJournalEnt.decCredits) AS TOTAL, SUM(dbo.tblJournalEnt.decDebitsF - dbo.tblJournalEnt.decCreditsF) AS TOTALF, 
                         dbo.tblJournalEnt.intGLNumber
FROM            dbo.tblCenterEnt INNER JOIN
                         dbo.tblJournalEnt ON dbo.tblCenterEnt.lngJourID = dbo.tblJournalEnt.lngJourID AND dbo.tblCenterEnt.intLineNum = dbo.tblJournalEnt.intLineNum INNER JOIN
                         dbo.tblCenters ON dbo.tblCenterEnt.uidCenterID = dbo.tblCenters.uidCenterID INNER JOIN
                         dbo.tblAccounts ON dbo.tblJournalEnt.intGLNumber = dbo.tblAccounts.intGLNumber
WHERE        (dbo.tblAccounts.intAccType = 5)
GROUP BY dbo.tblCenters.strLvl1, dbo.tblCenters.strLvl2, dbo.tblCenters.strLvl3, dbo.tblAccounts.strDesc, dbo.tblCenterEnt.uidCenterID, dbo.tblJournalEnt.intGLNumber

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[17] 2[23] 3) )"
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
               Top = 4
               Left = 6
               Bottom = 189
               Right = 176
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblJournalEnt"
            Begin Extent = 
               Top = 4
               Left = 194
               Bottom = 190
               Right = 413
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "tblCenters"
            Begin Extent = 
               Top = 0
               Left = 419
               Bottom = 190
               Right = 604
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblAccounts"
            Begin Extent = 
               Top = 0
               Left = 613
               Bottom = 188
               Right = 826
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
      Begin ColumnWidths = 10
         Width = 284
         Width = 3750
         Width = 1080
         Width = 3225
         Width = 2715
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
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CentreExpense'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CentreExpense'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CentreExpense'
GO


