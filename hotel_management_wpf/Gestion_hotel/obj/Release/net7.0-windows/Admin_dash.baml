   M S B A M L   `   `   `  ����   Gestion_hotel    WpfApp1.Admin_dash   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
extToolkit)http://schemas.xceed.com/wpf/xaml/toolkit  X TFontAwesome5.WPF, Version=1.0.5.65, Culture=neutral, PublicKeyToken=9cfaf01297a008f8/fa5%http://schemas.fontawesome.com/icons/    � Title$  Admin Dashboard��$	��600��$
��1000��.���  � ColumnDefinitions �� -    �Name$ Sidebar��$��* ��� $��5* � � RowDefinitions �� $��* �.+�� -   $
��Menu��$��0��
���   � �� $��2* ��� $��6* �.+�� .+��� $��20,20,20,20q�$'��!./Assets/profile-icon-png-909.png���� -    ��Name$ SidebarItems��$��Disabled=�$��0q����#3b3c40$��1��
�����������  �� 
TargetType" �� �� Triggers P�  P� Property �@��
IsSelected P� Value$
 True��. ��� 	 �� Property	 � ��
 �� Value

 �����.@��� -    ��Name$ 	Dashboard��$��60��.����  �� Orientation$ 
Horizontal=�.+�!  FontAwesome5.WPF.SvgAwesome     
Foreground
 �����$��10,0,0,0q�$
��20px��   Icon$ Solid_TachometerAlt =�$��Center=�$��Center=��� $��Dashboad��$
��Left=�$��Center=�$��20 10q��� -   $ ItemEmployees��$��60��.���� $ 
Horizontal=�.+�  
 �����$��10,0,0,0q�$
��20px��$ Solid_Users=�$��Center=�$��Center=��� $��	Employees��$
��Left=�$��Center=�$��20 10q��� -   $ ItemClients��$��60��.���� $ 
Horizontal=�.+�  
 �����$��10,0,0,0q�$
��20px��$ 
Solid_User=�$��Center=�$��Center=��� $��Clientes��$
��Left=�$��Center=�$��20 10q��� -   $ 	ItemSales��$��60��.���� $ 
Horizontal=�.+�  
 �����$��10,0,0,0q�$
��20px��$ 
Solid_Bed =�$��Center=�$��Center=��� $��Rooms��$��Center=�$��20 10q��� -   $ ItemImports��$��60��.���� $ 
Horizontal=�.+�  
 �����$��10,0,0,0q�$
��20px��$ Solid_LayerGroup=�$��Center=�$��Center=��� $��
Categories��$��Center=�$��20 10q��� -	   $ 	ItemStats��$��60��.���� $ 
Horizontal=�.+�  
 �����$��10,0,0,0q�$
��20px��$ 
Solid_List=�$��Center=�$��Center=��� $��Booking��$��Center=�$��20 10q�� $��1�� �� $
��70px��$��* ��� $��5* �.+�� $��0��
���   �.+��� $��Center=�
�������$��Center=�$��22(�Hotel Management&   Xceed.Wpf.Toolkit.DropDownButton  $��RightToLeft=�
�������$
��50px��$
��40px��$��8,8,8,8q�$��5,5,70,5q�$��Right=�.��  
 �   �$��0,0,0,0q�$
��20px��$ 
Solid_Bell=�$��Center=�$��Center=�   DropDownContent �� $��RightToLeft=�$��Disabled=�$��0q����#3b3c40
�������  $��RightToLeft=�
�������$
��50px��$
��40px��$��8,8,8,8q�$��5,5,10,5q�$��Right=�.��  
 �   �$��0,0,0,0q�$
��20px��$ Solid_SignOutAlt=�$��Center=�$��Center=� �� $��RightToLeft=�$��Disabled=�$��0q����#3b3c40
��������� $��1��
�������._�� -
   $��ContentMain��$��1��$��2,2,2,0q�
�������