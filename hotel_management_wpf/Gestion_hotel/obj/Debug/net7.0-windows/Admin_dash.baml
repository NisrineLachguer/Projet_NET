   M S B A M L   `   `   `  ����  Gestion_hotel    WpfApp1.Admin_dash   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
extToolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   X TFontAwesome5.WPF, Version=1.0.5.65, Culture=neutral, PublicKeyToken=9cfaf01297a008f8/fa5%http://schemas.fontawesome.com/icons/  5   	     � Title$  Admin Dashboard��5	   	   $	��600��6!   $
��1000��6.   .��5
      �  � ColumnDefinitions 5   
   �� 5      -   5         �Name$ Sidebar��5      $��* �6.   �� 5      $��5* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   � -   5        $
��Menu��5      $��0��6   
���   � 5      �� 5      $��2* �6    �� 5      $��6* �6    .+�5      � .+�5      �� $��20,20,20,20q�6   $'��!./Assets/profile-icon-png-909.png��6-   5      �� 5!      -   5         ��Name$ SidebarItems��5!      $��Disabled=�6^   $��0q�6�   ���#3b3c40$��1��6�   
���������5"      �� 5#       �� 
TargetType" ��6    �� Triggers 5$      P� 5%       P� Property �@��
IsSelected P� Value$
 True��6<   . �5&   "   �� 	 �� Property	 � ��
 �� Value

 �����6?   5'      5)      .@�5,      �� -   5         ��Name$ 	Dashboard��5,      $��60��62   .��5-      ��  �� Orientation$ 
Horizontal=�6!   .+�5.      !  FontAwesome5.WPF.SvgAwesome     
Foreground
 �����$��10,0,0,0q�6<   $
��20px��6N      Icon$ Solid_TachometerAlt =�6[   $��Center=�6w   $��Center=�6�   �� 5/      $��Dashboad��6$   $
��Left=�64   $��Center=�6O   $��20 10q�6j   50      51      �� 52      -   5        $ ItemEmployees��52      $��60��66   .��53      �� $ 
Horizontal=�6!   .+�54        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ Solid_Users=�6[   $��Center=�6n   $��Center=�6�   �� 55      $��	Employees��6$   $
��Left=�65   $��Center=�6P   $��20 10q�6k   56      57      �� 58      -   5        $ ItemClients��58      $��60��64   .��59      �� $ 
Horizontal=�6!   .+�5:        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ 
Solid_User=�6[   $��Center=�6m   $��Center=�6�   �� 5;      $��Clientes��6$   $
��Left=�64   $��Center=�6O   $��20 10q�6j   5<      5=      �� 5>      -   5        $ ItemRoom��5>      $��60��61   .��5?      �� $ 
Horizontal=�6!   .+�5@        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ 
Solid_Bed =�6[   $��Center=�6m   $��Center=�6�   �� 5A      $��Rooms��6$   $��Center=�61   $��20 10q�6L   5B      5C      �� 5D      -   5        $ ItemCate��5D      $��60��61   .��5E      �� $ 
Horizontal=�6!   .+�5F        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ Solid_LayerGroup=�6[   $��Center=�6s   $��Center=�6�   �� 5G      $��
Categories��6$   $��Center=�66   $��20 10q�6Q   5H      5I      �� 5J      -	   5        $ ItemBooking��5J      $��60��64   .��5K      �� $ 
Horizontal=�6!   .+�5L        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ 
Solid_List=�6[   $��Center=�6m   $��Center=�6�   �� 5M      $��Booking��6$   $��Center=�63   $��20 10q�6N   5N      5O      5P      5Q      � 5T   
   $��1��6    5U      �� 5V      $
��70px��6    $��* �61   �� 5W      $��5* �6    .+�5[      � $��0��6   
���   �.+�5]      �� $��Center=�6   
�������$��Center=�6L   $��22(�6g   Hotel Management6�   &   Xceed.Wpf.Toolkit.DropDownButton  5_      $��RightToLeft=�6,   
�������$
��50px��6[   $
��40px��6h   $��8,8,8,8q�6v   $��5,5,70,5q�6�   $��Right=�6�   .��5`        
 �   �$��0,0,0,0q�68   $
��20px��6I   $ 
Solid_Bell=�6V   $��Center=�6h   $��Center=�6�      DropDownContent 5a      �� 5b      $��RightToLeft=�6#   $��Disabled=�6?   $��0q�6u   ���#3b3c40
�������5d      5f        5h      -
   5        $��RightToLeft=�5h   C   
�������$
��50px��6r   $
��40px��6   $��8,8,8,8q�6�   $��5,5,10,5q�6�   $��Right=�6�   .��5i        
 �   �$��0,0,0,0q�68   $
��20px��6I   $ Solid_SignOutAlt=�6V   $��Center=�6n   $��Center=�6�    5j      �� 5k      $��RightToLeft=�6#   $��Disabled=�6?   $��0q�6u   ���#3b3c40
�������5m      5o      5p      �� 5s      $��1��6   
�������._�5t      � -   5        $��ContentMain��5t      $��1��6*   $��2,2,2,0q�67   
�������5v      5w      5x      5y      5z      