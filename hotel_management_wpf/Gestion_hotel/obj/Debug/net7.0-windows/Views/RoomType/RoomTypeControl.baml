   M S B A M L   `   `   `  ����  Gestion_hotel    WpfApp1.RoomTypeControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � ���#F8F9FA  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   .+�5      �� -   5         ��Name$ NameFilterTextBox��5      $	��150��61   $��5q�6=   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Name��6H   �� 5      -   5         ��Name$ SortByComboBox��5      $	��150��6/   $��5q�6;   .@�5      �� $
��Name��6   �� 5      $��Price��6   �� 5      $��Capacity��6   5      	� 5      -   5         	�Name$ AscendingRadioButton��5      $��	Ascending��68    d� 	IsChecked$
 TrueY�6L   $��5q�6]   	� 5      -   5        $ DescendingRadioButton��5      $��
Descending��69   $��5q�6N   �� 5      -   5        $��Filter��5      $��5q�6A   �� 5      -   5        $��Reset��5      $��5q�6F   5      �� 5    
   $ 
Horizontal=�6   $��Right=�6.   $��10q�6J   .+�5!      �� -   5        $��ExportExcelBtn��5!      $��Export Excel��6+   $��5q�6\   �� 5"      -   5        $��SendEmailBtn��5"      $��
Send Email��6)   $��5q�6V   5#      �� 5&   
   $��2��6   
����������#E0E0E0$��0,1,0,0q�6G    �� Padding$ 20q�6a   ._�5'      �� $ 
Horizontal=�6   $��Right=�62   .+�5(      �� -	   5        $��
AjouterBtn��5(      ���#4CAF50$	��Add��6@   $��5q�6f   �� 5)      -
   5        $��ModifierBtn��5)      
��� ���$
��Edit��6@   $��5q�6h   �� 5*      -   5        $��SupprimerBtn��5*      $��Delete��6-   $��5q�6Y   5+      5,      &   System.Windows.Controls.DataGrid  50   
   -   5        $��RoomTypesDataGrid��50      $��1��51         AutoGenerateColumns$ False��52      $��20q�53      $
��Auto=�54      	   
IsReadOnly$
	 True��55      
   SelectionMode$
 Single=�56         SelectionUnit$ FullRow=�57         Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+6L     5:      $ Description��6%   $ 3* 6Z    6:   �� *Description+6Z     5;      $ Price��6%   $
 1.5* 6N    64   �� *Price+6N     5<      $ Capacity��6%   $ 1* 6T    67   �� *
Capacity+6T   5>      �� 5A   
   $ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��10q�6X   .+�5B      �� -   5        $��Previous��5B      $��5q�6C   �� 5C      -   5         ��Name$ 	PageLabel��5C      $��Page 1��6'   $��10q�68   �� 5D      -   5        $
��Next��5D      $��5q�6;   5E      5F      5G      