   M S B A M L   `   `   `  ����   Gestion_hotel    WpfApp1.RoomControl   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  .��� ���#F8F9FA  � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��10q�.+��� -    ��Name$ RoomTypeFilterTextBox��$	��150��$��5q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Room Type���� -   $ RoomNumberFilterTextBox��$	��150��$��5q�$ Filter by Room Number���� -    ��Name$ SortByComboBox��$	��150��$��5q�.@��� $��	Room Type���� $��Room Number���� $��Availability��	� -    	�Name$ AscendingRadioButton��$��	Ascending�� d� 	IsChecked$
 TrueY�$��5q�	� -   $ DescendingRadioButton��$��
Descending��$��5q��� -   $��Filter��$��5q��� -   $��Reset��$��5q��� $��2��
����������#E0E0E0$��0,1,0,0q� �� Padding$ 20q�._��� $ 
Horizontal=�$��Right=�.+��� -   $��
AjouterBtn�����#4CAF50$	��Add��$��5q��� -	   $��ModifierBtn��
��� ���$
��Edit��$��5q��� -
   $��SupprimerBtn��$��Delete��$��5q�&   System.Windows.Controls.DataGrid  -   $��RoomsDataGrid��$��1��   AutoGenerateColumns$ False��$��20q�$
��Auto=�	   
IsReadOnly$
	 True��
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Room Type��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *RoomTypeName+  $ Room Number��$
 1.5*  �� *
RoomNumber+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Availability��$ 1*  �� *Availability+  $ Description��$ 2*  �� *Description+�� $ 
Horizontal=�$��2��$��20q�.+��� -   $��TtlRoomsLbl��$��Total Rooms: 0��$
��10,0q�