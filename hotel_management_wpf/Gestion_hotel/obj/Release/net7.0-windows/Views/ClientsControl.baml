   M S B A M L   `   `   `  ����   Gestion_hotel    WpfApp1.ClientsControl   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � 	Resources     ModernButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  &	        '  ;    '      �� " ��.���  � �� �#2196F3��  � ��
 �������  � ��$ 14(���  � ��$
 15,8q���  � ��$ 0q���  � ��$ 5q���  � �� �� " ��.R��� #��z��$ 4��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#1976D2�� "  .���  � ��
 �������  � �� �#E0E0E0��  � ��$ 1q���  �@ RowBackground
 �������   �@ AlternatingRowBackground �#F5F5F5��  �@ HeadersVisibility$ Column=���  �@ GridLinesVisibility$ 
Horizontal=���   �@ HorizontalGridLinesBrush �#E0E0E0�� "  .���  � �� �#2196F3��  � ��
 �������  � ��$ 10q���  � ��$ SemiBold"�.��� ���#F8F9FA	 � RowDefinitions	 �� $
��Auto ��� $��* ��� $
��Auto �.+��� 
 �� Orientation$
 
Horizontal=�$��10q�.+��� -    ��Name$ NameFilterTextBox��$	��150��$��5q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Name���� -   $ EmailFilterTextBox��$	��150��$��5q�$ Filter by Email���� -    ��Name$ SortByComboBox��$	��150��$��5q�.@��� $
��Name���� $��Email��	� -    	�Name$ AscendingRadioButton��$��	Ascending�� d� 	IsChecked$
 TrueY�$��5q�	� -   $ DescendingRadioButton��$��
Descending��$��5q��� -   $��Filter��$��5q��� -   $��Reset��$��5q��� -   $��ExportExcelBtn�����#1976D2$��Export to Excel��$��5q����� .R��� $
 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	FileExcel=�$��0,0,8,0q�$��Center=��� $��Export to Excel��$��Center=��� $��2��
����������#E0E0E0$��0,1,0,0q� �� Padding$ 20q�._��� $
 
Horizontal=�$��Right=�.+��� -	   $��
AjouterBtn�����#4CAF50$��Ajouter��$��5q��� -
   $��ModifierBtn��
��� ���$��Modifier��$��5q��� -   $��SupprimerBtn��$��	Supprimer��$��5q�  -   $��ClientsDataGrid��$��1��   AutoGenerateColumns$ False��$��20q�$
��Auto=�   
IsReadOnly$
 True��   SelectionMode$ Single=�   SelectionUnit$ FullRow=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 Nom��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.5* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ Email��$ 2*  �� *Email+  $ Téléphone��$
 1.2*  �� *PhoneNumber+  $ Adresse��$ 2*  �� *	Address+�� $
 
Horizontal=�$��2��$��20q�.+��� -    ��Name$ 
lblClients��$��Total des clients : 6��$
��10,0q�