   M S B A M L   `   `   `  ����H  DGestion_hotel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:WpfApp1  5   	     � Title$  
Login_Page��5
   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$
 None=�6Z    � 
ResizeMode$ NoResize=�6m   .��5      � -   5        ��5   
   �� 5       �� 
StartPoint$ 0.1,0&�6"    �� EndPoint$ 0.9,1&�65   .��5      �  � Color$ 	#FFE63070��6    � Offset$ 1Q�61   � 5      $ 	#FFFE8704��6   $ 0Q�61   5      .+�5   
   �� -   5        $	��390��5      $	��Top=�6    �� CornerRadius$ 	0 0 180 0��66   ���#100E17$��	0,-12,0,0q�6�   5      �� 5   
   	 �� Orientation$	 
Horizontal=�6   .+�5      �� $	��350��6   .+�5      �� $q��kC:\Users\irharissa.NPIF\Desktop\EMSI\.NET\Projet_NET\hotel_management_wpf\Gestion_hotel\Images\ICON4801.png��6   $	��300��6�   $	��300��6�   $	��Top=�6�   $��30q�6�   $
��Fill=�6�   5      �� 5      $	��350��6   .+�5      �� $��20 40q�6   .+�5      �� $��Admin Login��6    $��20q�63   
�������
 �� TextAlignment$
 Center=�6R   $��38(�6i   $��Great Vibes)�6w   6�   �� 5      $	 
Horizontal=�6!   $��10q�6:   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�62   $��25��6>   $��25��6I   
�������6U   �� 5       -   5         ��Name$ txtUsername��5    "   $
��10 0q�67   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Enter Username or Email��6F   
�������$��0,0,0,1q�6�   
������ $	��250��6�   
������� �� 
CaretBrush �	#FFD94448 �� SelectionBrush �	#FFD944486  5!      �� 5"      $	 
Horizontal=�6!   $��10q�6:   .+�5#        $
 Lock=�62   $��25��6>   $��25��6I   
�������6U   G� 5$      -   5         G�Name$ txtPassword��5$   &   $
��10 0q�6;   $ ********��6J   
�������$��0,0,0,1q�6�   
������ $	��250��6�   
������� G� 
CaretBrush �	#FFD94448 G� SelectionBrush �	#FFD944486�   5%      �� 5&      $��10q�6!   $��Center=�6-   .+�5'      �� -   5         ��Name$ btnLogin��5*   !   $	��100��5'   !   $��40��6-      MaterialDesignRaisedButton#��[  69   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��5(   !   $��2q�6O   ���#D94448���#D94448
������� � ToolTip$ Login��5)   I   $��Login��5*   J   5,      �� 5-      -   5         ��Name$ 	lblStatus��5-   $   
���  ��$��0,10,0,0q�6H   $
 Center=�6Z   5.      5/      50      �� 51      $	��100��6   .+�52      �� -   5        $ btnExit��53   )   $�� 10 20q�52       & "MaterialDesignFloatingActionButton#��[ 6)   $ Close��53      ��52   e   Z� .��54        $ Close=�6.   
�������$��30��6N   $��30��6Y   55      56      57      58      59      