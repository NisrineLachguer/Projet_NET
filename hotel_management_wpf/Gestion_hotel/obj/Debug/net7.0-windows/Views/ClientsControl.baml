   M S B A M L   `   `   `  ����  Gestion_hotel    WpfApp1.ClientsControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � 	Resources  5         ModernButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  &	        '  B    '      �� 5   
   " ��6%   .�5      ��  � �� �#2196F36+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 15,8q�6(   �� 5       � ��$ 0q�60   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 4��5   !   #��z��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver$
 True��6=   . �5   "   ��  � �� �#1976D26?   5      5!      5#      5$      �� 5'   
   "  6   .�5(      ��  � ��
 �����6+   �� 5)       � �� �#E0E0E06,   �� 5*       � ��$ 1q�60   �� 5+       �@ RowBackground
 �����6.   �� 5,        �@ AlternatingRowBackground �#F5F5F569   �� 5-       �@ HeadersVisibility$ Column=�62   �� 5.       �@ GridLinesVisibility$ 
Horizontal=�64   �� 5/        �@ HorizontalGridLinesBrush �#E0E0E069   50      �� 52   
   "  6   .�53      ��  � �� �#2196F36+   �� 54       � ��
 �����6+   �� 55       � ��$ 10q�6(   �� 56       � ��$ SemiBold"�6+   57      58      .��5:      � ���#F8F9FA	 � RowDefinitions	 5;   
   �� 5<      $
��Auto �6   �� 5=      $��* �6   �� 5>      $
��Auto �6   .+�5B   
   �� 
 �� Orientation$
 
Horizontal=�6   $��10q�6.   .+�5C      �� -   5         ��Name$ NameFilterTextBox��5C      $	��150��61   $��5q�6=   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Name��6H   �� 5D      -   5        $ EmailFilterTextBox��5D      $	��150��62   $��5q�6>   $ Filter by Email��6I   �� 5E      -   5         ��Name$ SortByComboBox��5E      $	��150��6/   $��5q�6;   .@�5F      �� $
��Name��6   �� 5G      $��Email��6   5H      	� 5I      -   5         	�Name$ AscendingRadioButton��5I      $��	Ascending��68    d� 	IsChecked$
 TrueY�6L   $��5q�6]   	� 5J      -   5        $ DescendingRadioButton��5J      $��
Descending��69   $��5q�6N   �� 5K      -   5        $��Filter��5K      $��5q�6A   �� 5L      -   5        $��Reset��5L      $��5q�6F   �� 5M      -   5        $��ExportExcelBtn��5M      ���#1976D2$��Export to Excel��5O      $��5q�5Q      ��5S      �� 5T      .R�5U      �� $
 
Horizontal=�6%   .+�5V      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	FileExcel=�66   $��0,0,8,0q�5W   6   $��Center=�5X   6   �� 5Y      $��Export to Excel��6(   $��Center=�5Z   (   5[      5\      5_      5`      �� 5c   
   $��2��6   
����������#E0E0E0$��0,1,0,0q�6G    �� Padding$ 20q�6a   ._�5d      �� $
 
Horizontal=�6   $��Right=�62   .+�5e      �� -	   5        $��
AjouterBtn��5e      ���#4CAF50$��Ajouter��6@   $��5q�6k   �� 5f      -
   5        $��ModifierBtn��5f      
��� ���$��Modifier��6@   $��5q�6m   �� 5g      -   5        $��SupprimerBtn��5g      $��	Supprimer��6-   $��5q�6\   �� 5h      -   5        $��Send PDF Report��5h      $��5q�5j      5k      5l        5o   
   -   5        $��ClientsDataGrid��5o      $��1��5p         AutoGenerateColumns$ False��5q      $��20q�5r      $
��Auto=�5s         
IsReadOnly$
 True��5t         SelectionMode$ Single=�5v         SelectionUnit$ FullRow=�5w         Columns 5x      0  *System.Windows.Controls.DataGridTextColumn  5y      ,  &System.Windows.Controls.DataGridColumn   Header$	 Nom��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.5* 6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *Name+6K     5z      $ Email��6%   $ 2* 6N    64   �� *Email+6N     5{      $ Téléphone��6%   $
 1.2* 6X    68   �� *PhoneNumber+6X     5|      $ Adresse��6%   $ 2* 6R    66   �� *	Address+6R   5~      �� 5�   
   $
 
Horizontal=�6   $��2��6.   $��20q�6;   5�      5�      5�      