   M S B A M L   `   `   `  ����  Gestion_hotel%    WpfApp1.ReservationStateControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � ���#F8F9FA  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   .+�5      �� -   5         ��Name$ CodeFilterTextBox��5      $	��150��61   $��5q�6=   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Filter by Code��6H   �� 5      -   5        $ LabelFilterTextBox��5      $	��150��62   $��5q�6>   $ Filter by Label��6I   �� 5      -   5        $��Filter��5      $��5q�6A   �� 5      -   5        $��Reset��5      $��5q�6F   5      �� 5   
   $ 
Horizontal=�6   $��Right=�6.   $��10q�6J   .+�5      �� -   5        $��ExportExcelBtn��5      $��Export Excel��6+   $��5q�6\   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��ReservationStatesDataGrid��5      $��1��5          AutoGenerateColumns$ False��5!      $��20q�5"      $
��Auto=�5#         
IsReadOnly$
 True��5$         SelectionMode$ Single=�5%         SelectionUnit$ FullRow=�5&         Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Code��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 2* 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Code+6L     5)      $	 Label��6%   $
 3* 6N    64   �� *Label+6N   5+      �� 5.   
   $ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��10q�6X   .+�5/      �� -   5        $��Previous��5/      $��5q�6C   �� 50      -   5         ��Name$ 	PageLabel��50      $��Page 1��6'   $��10q�68   �� 51      -	   5        $
��Next��51      $��5q�6;   52      53      54      