   M S B A M L   `   `   `  ����   Gestion_hotel    WpfApp1.RoomTypeControl   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  .��� ���#F8F9FA  � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��10q�.+��� -    ��Name$ NameFilterTextBox��$	��150��$��5q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Name���� -    ��Name$ SortByComboBox��$	��150��$��5q�.@��� $
��Name���� $��Price���� $��Capacity��	� -    	�Name$ AscendingRadioButton��$��	Ascending�� d� 	IsChecked$
 TrueY�$��5q�	� -   $ DescendingRadioButton��$��
Descending��$��5q��� -   $��Filter��$��5q��� -   $��Reset��$��5q��� $ 
Horizontal=�$��Right=�$��10q�.+��� -   $��ExportExcelBtn��$��Export Excel��$��5q��� -   $��SendEmailBtn��$��
Send Email��$��5q��� $��2��
����������#E0E0E0$��0,1,0,0q� �� Padding$ 20q�._��� $ 
Horizontal=�$��Right=�.+��� -	   $��
AjouterBtn�����#4CAF50$	��Add��$��5q��� -
   $��ModifierBtn��
��� ���$
��Edit��$��5q��� -   $��SupprimerBtn��$��Delete��$��5q�&   System.Windows.Controls.DataGrid  -   $��RoomTypesDataGrid��$��1��   AutoGenerateColumns$ False��$��20q�$
��Auto=�	   
IsReadOnly$
	 True��
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ Description��$ 3*  �� *Description+  $ Price��$
 1.5*  �� *Price+  $ Capacity��$ 1*  �� *
Capacity+�� $ 
Horizontal=�$��Center=�$��2��$��10q�.+��� -   $��Previous��$��5q��� -    ��Name$ 	PageLabel��$��Page 1��$��10q��� -   $
��Next��$��5q�