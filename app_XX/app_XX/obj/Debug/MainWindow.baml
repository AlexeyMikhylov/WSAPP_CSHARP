   M S B A M L   `   `   `  ����A  =app_XX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:app_XXapp_XX  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       app_XX.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:app_XX     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   -   5          � Title$  
MainWindow��5	   	   $	��700��6   $
��1250��6)    � WindowStartupLocation$ CenterScreen=�6M    � 
ResizeMode$ CanMinimize=�6r    � SizeToContent$ WidthAndHeight=�6�   .��5
      � .+�5   
   �� $
��Left=�6   $��23��6,   $	��Top=�68   $
��1244��6P   ���	#FFEBEBEB.+�5      1&clr-namespace:System;assembly=mscorlibSystem 6   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6   -   5         ��Name$ cmbx_tableselection��5   J   $��Center=�6g   $
��Left=�6�   $	��Top=�6�   $��72��6�   $��23��6�    �� SelectedIndex$ 0��6�    �� Left$ 25��6�    � 	Resources 5      ��%X   (��       5   !   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   0Q�6b   5      .@�5      �� $��Select table��6   �� 5      $��clients��6   5      1&clr-namespace:System;assembly=mscorlibSystem 5      �� 6   6sys&clr-namespace:System;assembly=mscorlib     6   -   5        $ cmbx_searchselection��5   J   $��Center=�6h   $
��Left=�6�   $	��Top=�6�   $��70��6�   $��23��6�   $	 270��6�   $ 0��6�    �� 
IsReadOnly .  5      ��%X   (��       5   !   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   0Q�6b   5      .@�5      �� $��
Search by:��6   �� 5      $��
first name��6   �� 5      $��	last name��6   �� 5      $��
patronymic��6   �� 5      $��phone��6   �� 5      $��email��6   5      �� 5      -   5        	 ��Name$	 txtbx_search��5      $��Center=�6,   $
��Left=�6N   $��23��6i   
 �� TextWrapping$

 Wrap=�6u   $	��Top=�6�   $	��230��6�   $	 340��6�   6�   1&clr-namespace:System;assembly=mscorlibSystem 5       �� 6   6sys&clr-namespace:System;assembly=mscorlib     6   -   5        $ cmbx_genderfilter��5    J   $��Center=�6e   $
��Left=�6�   $	��Top=�6�   $��78��6�   $��23��6�   $	 192��6�   $ 0��6�    5!      ��%X   (��       5"   !   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   0Q�6b   5#      .@�5$      �� $��Gender filter��6   �� 5%      $	��all��6   �� 5&      $��	only male��6   �� 5'      $��only female��6   5(      1&clr-namespace:System;assembly=mscorlibSystem 5*      �� 6   6sys&clr-namespace:System;assembly=mscorlib     6   -   5        $ cmbx_rowsammount��5*   J   $��Center=�6d   $
��Left=�6�   $	��Top=�6�   $��95��6�   $��23��6�   $ 97��6�   $ 0��6�    5+      ��%X   (��       5,   $   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   0Q�6e   5-      .@�5.      �� $��Rows ammount��6   �� 5/      $��10��6   �� 50      $��50��6   �� 51      $	��200��6   52      �� 54      -   5        $ 
cmbx_order��54      $
��Left=�6+   $	��Top=�6F   $��64��6^   $��23��6i   $	 570��6u   $ 0��6�   $��Center=�6�    55      ��%X   (��       56   $   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   0Q�6e   57      .@�58      �� $��Order by��6   �� 59      $��	last name��6   �� 5:      $��	last seen��6   �� 5;      $��requests��6   5<      �� 5=      -   5         ��Name$ btn_addclient��5=      $��
Add client��6,   $	 694��6A   $��75��6S   $��23��6^   ���	#FFACACAC���	#FFEBEBEB6�   �� 5>      -	   5        $ btn_editclient��5>      $��Edit client��6-   $	 769��6C   $��75��6U   $��23��6`   ���	#FFACACAC���	#FFEBEBEB6�   �� 5?      -
   5        $ 
btn_update��5?      $
��🔄��6)   $��25��66   $��23��6A   ���	#FFACACAC���	#FFEBEBEB6|   �� 5@      -   5        $ btn_deleteclient��5@      $��Delete client��6/   $	 844��6G   $��75��6Y   $��23��6d   ���	#FFACACAC���	#FFEBEBEB6�   �� 5A      -   5        $ btn_showbirthday��5A      $��Birthday��6/   $	 634��6B   $��60��6T   $��23��6_   ���	#FFACACAC���	#FFEBEBEB6�   5B      &   System.Windows.Controls.DataGrid  5C   
   -   5          Name$ grid_clients��5C      $
��Left=�6)   $	��610��6D   $��	10,49,0,0q�6Q   $	��Top=�6d   $
��1222��6|      CanUserAddRows .     
IsReadOnly . 6�   �� 5D   
   -   5         ��Name$ lbl_totalrows��5D      $��Label��6'   $��70,23,1136,622q�67   �� 5E   
   -   5        $ lbl_currentrows��5E      $��Label��6)   $��10,23,1196,622q�69   �� 5F   
   -   5        $ lbl_deletemelater_Copy1��5F      $
��из��61   $��48,23,1174,622q�6>   5H      5I      