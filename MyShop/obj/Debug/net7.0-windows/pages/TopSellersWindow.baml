   M S B A M L   `   `   `  ����
  MyShop#    MyShop.pages.TopSellersWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Top Sellers��5   	   $	��450��6   $	��800��6*   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5	      .+�5   
   �� $��0��6   $��25(�6!   $��Center=�6/   $
��Bold"�6L   $$��Top 5 sản phẩm bán chạy��6^   6   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      �� $��Right=�6   $��Bottom=�61   $��1��5      $��30��5      $	��120��6   $��	0,0,140,3q�6)   ._�5      �� -   5        $��YearCombobox��5      $��1q�5      
������ $��90��6E   $��Center=�5      ��5      �� 5       �� 
TargetType" ��6    5      5      5       �� 5!      $��Right=�6   $��Bottom=�61   $��1��5"      $��30��5#      $	��120��6   $��0,0,10,3q�6)   ._�5%      �� -   5        $��MonthCombobox��5&      ${�False��5%      $��0q�5'      
������ $��90��6E   $��Center=�5(      ��5*      �� 5+      " ��6    5-      .@�5/      ��  J� IsHitTestVisible$ False��6#   ${�False��6<    �� 
IsSelected$
 True��6N   .��6`   Chọn tháng6l   �� 50      .��6#   
Tháng 16,   �� 51      .��6#   
Tháng 26,   �� 52      .��6#   
Tháng 36,   �� 53      .��6#   
Tháng 46,   �� 54      .��6#   
Tháng 56,   �� 55      .��6#   
Tháng 66,   �� 56      .��6#   
Tháng 76,   �� 57      .��6#   
Tháng 86,   �� 58      .��6#   
Tháng 96,   �� 59      .��6#   	Tháng 106-   �� 5:      .��6#   	Tháng 116-   �� 5;      .��6#   	Tháng 126-   5<      5=      5>      &   System.Windows.Controls.DataGrid  5?   
   -   5          Name$ topSellersGrid��5?       $��2��6      AutoGenerateColumns$ False��68      Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Phone Name��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	PhoneName+6W     5B      $	 Total Amount��6%   $
 * 6[    6;   �� *TotalAmount+6[   5D      5E      5F      