   M S B A M L   `   `   `  ���� Z  VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466J6clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPFFontAwesome.WPF  U QLiveCharts.Wpf, Version=0.9.8.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf A =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyShop.pagesMyShop.pages    MyShop.pages.Statistic   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Efa6clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPF     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     ,localclr-namespace:MyShop.pages     -     N� Title$  	Statistic�� � 	Resources    test�� %.   &	        *�/res/font/#SVN-Transformer)�.��� -   $��MainContainer�� � RowDefinitions �� $��60 ���  � ColumnDefinitions �� $��* �.+�� -    �Name$ SearchContainer��$��0,0,30,0q�$��1��$
��Left=�$	��450��
������  �� $	��100 ��� �� .+��� $��1�� �� Orientation$ 
Horizontal=�$��Bottom=�.+��� $��10q�$��
Từ ngày��$��12(�$
��Bold"�(  "System.Windows.Controls.DatePicker  -   $��	StartDate��$��0,0,0,5q�$��Bottom=�   FirstDayOfWeek$ Monday=�   SelectedDateFormat$ Short=��� $��2��$ 
Horizontal=�$��Bottom=�.+��� $��10q�$��Đến ngày��$��12(�$
��Bold"�  -   $��EndDate��$��0,0,0, 5q�$��Bottom=�$ Monday=�$ Short=�   DisplayDateStart 1/04/23�� -   $��Search��$��0��$��35��$��28��$	��0.5q�
��� ���$��Center=�$��Bottom=�$��0,0,0,3q�.���� $ 
Horizontal=�.+�"   FontAwesome.WPF.ImageAwesome  $��12��$��12��	 �� BitmapScalingMode$	 HighQuality=�
   Icon$
 Search=�   
Foreground �#8B4000�� $��Right=�$��Bottom=�$��1��$��30��$	��120��$��	0,0,150,3q�._��� -   $��YearCombobox��$��0q�
������ $��90��$��Center=�����  �� 
TargetType" ���� $��Right=�$��Bottom=�$��1��$��30��$	��120��$��0,0,10,3q�._��� -   $��MonthCombobox��${�False��$��0q�
������ $��90��$��Center=����� " ��.@���  J� IsHitTestVisible$ False��${�False�� �� 
IsSelected$
 True��.��Chọn tháng�� .��
Tháng 1�� .��
Tháng 2�� .��
Tháng 3�� .��
Tháng 4�� .��
Tháng 5�� .��
Tháng 6�� .��
Tháng 7�� .��
Tháng 8�� .��
Tháng 9�� .��	Tháng 10�� .��	Tháng 11�� .��	Tháng 12� $��1��$��1�� �� �� $��40 �.+��� -	   $��Title��$��1��$0��*Đang hiển thị xem doanh thu theo năm��$��10,0,0,0q�
�������$��Center=�$��Center=�$��16(�$
��Bold"�$��Segoe UI)��� ���	#60ffffff$��10q�$��0�� �� CornerRadius$ 15��._�#  LiveCharts.Wpf.CartesianChart  -
   $��chart��&   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�$��10q�