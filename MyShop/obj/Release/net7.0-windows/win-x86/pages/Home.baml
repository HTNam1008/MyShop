   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.8.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  A =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyShop.pagesMyShop.pages    MyShop.pages.Home   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     ,localclr-namespace:MyShop.pages       N� Title$
  Home�� � 	Resources    test�� %.   &	        *�/res/font/#SVN-Transformer)�.���� 
�������._��  � RowDefinitions �� $��60 ��� $��2* ��� $��60 ��� .+��� $��0��$	��Top=�.+��� #��[  �� $�Hi...���� #��[  $��35(��� $�Welcome back  -- HOME��� $��1�� �� ��  � ColumnDefinitions �� �� $��2* �.+��� 
���   �$��1q�
������� �� CornerRadius$ 15��._��� $��10q� �� Orientation$ Vertical=�.+��� $��Tổng số sản phẩm��$��25(�$��Normal$�$��Center=��� -   $��txtTotalPhone��$��40(�$��Heavy"�
������$��Center=��� $��0��$��1��
���   �$��1q�
�������$ 15��._��� $��10q�$ Vertical=�.+��� $��Tổng số nhãn hiệu��$��25(�$��Normal$�$��Center=��� -   $��txtTotalBrand��$��40(�$��Heavy"�
������$��Center=�� $��1��$��0�� �� $��20 ��� .+��� $��2q�$��Center=�$'��!Số đơn hàng mới trong năm���� $��1��
�������$��10q�$ 15��._�#   LiveCharts.Wpf.CartesianChart  -   $��	chartWeek��&    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�$��10q�� $��1��$��1�� �� $��20 ��� .+��� $��2q�$��Center=�$)��#Số đơn hàng mới trong tháng���� 
�������$��10q�$��1��$��1��$ 15��._�  -   $��
chartMonth��$ Bottom=�$��10q��� $��2��$*��$Top 5 sản phẩm sắp hết hàng��$��20(�$��DemiBold"�
����+��
�������$��Center=�$��Center=�&   System.Windows.Controls.DataGrid  -     Name$ topSellersGrid��$��3��   AutoGenerateColumns$ False��	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 
Phone Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	PhoneName+  $
 Quantity��$ *  �� *TotalAmount+