   M S B A M L   `   `   `  ����  Gestion_hotel     WpfApp1.ReservationControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5	      � ���#F8F9FA  � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   .+�5      �� -   5         ��Name$ ClientFilterTextBox��5      $	��150��63   $��5q�6?   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Client��6J   �� 5      -   5         ��Name$ SortByComboBox��5      $	��150��6/   $��5q�6;   .@�5      �� $��Client��6   �� 5      $
��Room��6   �� 5      $
��Date��6   5      	� 5      -   5         	�Name$ AscendingRadioButton��5      $��	Ascending��68    d� 	IsChecked$
 TrueY�6L   $��5q�6]   	� 5      -   5        $ DescendingRadioButton��5      $��
Descending��69   $��5q�6N   �� 5      -   5        $��Filter��5      $��5q�6A   �� 5      -   5        $��Reset��5      $��5q�6F   5      �� 5   
   $ 
Horizontal=�6   $��Right=�6.   $��10q�6J   .+�5       �� -   5        $��ExportExcelBtn��5       $��Export Excel��6+   $��5q�6\   �� 5!      -   5        $��SendEmailBtn��5!      $��
Send Email��6)   $��5q�6V   5"      &   System.Windows.Controls.DataGrid  5%   
   -	   5        $��ReservationsDataGrid��5%      $��1��5&         AutoGenerateColumns$ False��5'      $��20q�5(      $
��Auto=�5)         
IsReadOnly$
 True��5*      	   SelectionMode$	 Single=�5+      
   SelectionUnit$
 FullRow=�5,         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ Client��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Client.FirstName+6Z     5/      $
 Room��6%   $ 2* 6W    63   �� *Room.RoomNumber+6W     50      $ 
Start Date��6%   $ 2* 6W    69   �� *	StartDate+6W     51      $ End Date��6%   $ 2* 6S    67   �� *	EndDate+6S     52      $ 
Total Cost��6%   $ 2* 6W    69   �� *	TotalCost+6W     53      $ State��6%   $
 1.5* 6_    64   �� *ReservationState.Label+6_   55      �� 58   
   $ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��10q�6X   .+�59      �� -
   5        $��Previous��59      $��5q�6C   �� 5:      -   5         ��Name$ 	PageLabel��5:      $��Page 1��6'   $��10q�68   �� 5;      -   5        $
��Next��5;      $��5q�6;   5<      5=      5>      