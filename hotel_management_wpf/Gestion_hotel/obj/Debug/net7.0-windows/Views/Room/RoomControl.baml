   M S B A M L   `   `   `  ����  Gestion_hotel    WpfApp1.RoomControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � ���#F8F9FA  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   .+�5      �� -   5         ��Name$ RoomTypeFilterTextBox��5      $	��150��65   $��5q�6A   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Room Type��6L   �� 5      -   5        $ RoomNumberFilterTextBox��5      $	��150��67   $��5q�6C   $ Filter by Room Number��6N   �� 5      -   5         ��Name$ SortByComboBox��5      $	��150��6/   $��5q�6;   .@�5      �� $��	Room Type��6   �� 5      $��Room Number��6   �� 5      $��Availability��6   5      	� 5      -   5         	�Name$ AscendingRadioButton��5      $��	Ascending��68    d� 	IsChecked$
 TrueY�6L   $��5q�6]   	� 5      -   5        $ DescendingRadioButton��5      $��
Descending��69   $��5q�6N   �� 5      -   5        $��Filter��5      $��5q�6A   �� 5      -   5        $��Reset��5      $��5q�6F   5      �� 5!   
   $ 
Horizontal=�6   $��Right=�6.   $��10q�6J   .+�5"      �� -   5        $��ExportExcelBtn��5"      $��Export Excel��6+   $��5q�6\   �� 5#      -	   5        $��SendEmailBtn��5#      $��
Send Email��6)   $��5q�6V   5$      �� 5'   
   $��2��6   
����������#E0E0E0$��0,1,0,0q�6G    �� Padding$ 20q�6a   ._�5(      �� $ 
Horizontal=�6   $��Right=�62   .+�5)      �� -
   5        $��
AjouterBtn��5)      ���#4CAF50$	��Add��6@   $��5q�6j   �� 5*      -   5        $��ModifierBtn��5*      
��� ���$
��Edit��6@   $��5q�6n   �� 5+      -   5        $��SupprimerBtn��5+      $��Delete��6-   $��5q�6]   5,      5-      &   System.Windows.Controls.DataGrid  50   
   -   5        $��RoomsDataGrid��50      $��1��51         AutoGenerateColumns$ False��52      $��20q�53      $
��Auto=�54      	   
IsReadOnly$
	 True��55      
   SelectionMode$
 Single=�56         SelectionUnit$ FullRow=�57         Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Room Type��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *RoomTypeName+6Y     5:      $ Room Number��6%   $
 1.5* 6Y    6:   �� *
RoomNumber+6Y   4  .System.Windows.Controls.DataGridCheckBoxColumn  5;      $ Availability��6)   $ 1* 6`    6?   �� *Availability+6`     5<      $ Description��6%   $ 2* 6Z    6:   �� *Description+6Z   5>      �� 5A   
   $ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��10q�6X   .+�5B      �� -   5        $��Previous��5B      $��5q�6C   �� 5C      -   5         ��Name$ 	PageLabel��5C      $��Page 1��6'   $��10q�68   �� 5D      -   5        $
��Next��5D      $��5q�6;   5E      5F      5G      