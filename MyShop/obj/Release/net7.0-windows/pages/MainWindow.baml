   M S B A M L   `   `   `  ���� A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MyShopMyShop  1clr-namespace:MyShop.controlsMyShop.controls      MyShop.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:MyShop     .ctlsclr-namespace:MyShop.controls       � Title$  MyShop��$	��750��$
��1200�� � 	Resources    test  
expandGrid �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value :� RoutedEvent	 �� TargetProperty	
 �� To g� Duration  	txt_style  	btn_style��%�  &	        &	 $     &	     &	 �    *�/res/font/#SVN-Transformer)��� " �.���  � ��
 ����  P�  �@J�IsMouseOver$
 True��. ���  � �� �#bee6fd:� $ PreviewMouseDown��.[��� .���� .���� $/	 )(Grid.Background).(SolidColorBrush.Color)�
 #bee6fd$ 0:0:0D��� " ��.���  � ��
 �   ���  � ��
 ���� ��  � ��$	 100����  � ��$ 28(���  � ��$ 60����  � ��$ 
100,20,0,0q���  � ��$
 Left=��� " ��.���  � ��
 �   ���  � ��
 ���� ��  � ��$	 100����  � ��$ 60����  � ��$ 	0,10,0,10q���  � ��$ 	0,10,0,10q���  � ��$ 0q���  � ��$
 Left=���  � ��$ Center=�.��� 
������� � ColumnDefinitions �� $
��Auto ��� $	��700��$��* �.+�� -    �Name$	 nav��$��0��$	��100��$
��Left=�.+��� $��0��
������� �� CornerRadius$ 20 20 20 20��._���  �� Orientation$ Vertical=�.+�� -   $ gridMenu��.+��� $��M E N U��$��28(�$	��100��$��60��$��Center=�$
��Left=�$��
100,20,0,0q�
������ 
���   � J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   ShadowDepth$ 1Q�   	Direction$	 -90Q�   Color$ #bee6fd��d� -    d�Name$ 	btnToggle��$��60��$��60��$��20q�$��Center=�$
��Left=�$��0q�
������ .����  �� BitmapScalingMode$ HighQuality=�$��res/icon/toggle.png�� ��%9   '��       �� " ��.���  �@��CornerRadius$ 8�� � Triggers :� $ ToggleButton.Unchecked��.[��� .���� -    ��Name$ HideStackPanel��.��W�  �� 
TargetName$	 nav��$	 Width� g� 	BeginTime	 0:0:0 W� From 250	 W� To 100$ 	0:0:0:0.2D�:� $ ToggleButton.Checked��.[��� .���� -   $ ShowStackPanel��.��W� $	 nav��$	 Width�	 0:0:0 100 250$ 	0:0:0:0.3D�� #��[ .+��� -    ��Name$ btnHome��#��[ .���� $ HighQuality=�$��/res/icon/home.png���� -   $
��Home��#��[ � #��[ .+��� -   $ 
btnProduct��#��[ .���� $ HighQuality=�$��/res/icon/product.png���� -	   $��Product��#��[ � #��[ .+��� -
   $ btnStatistic��#��[ .���� $ HighQuality=�$��/res/icon/line_chart.png���� -   $��Revenue Statistic��#��[ � #��[ .+��� -   $ btnProductStatistic��#��[ .���� $ HighQuality=�$$��/res/icon/productStatistic.png���� -   $��Product Statistic��#��[ � #��[ .+��� -   $ btnOrder��#��[ .���� $ HighQuality=�$��/res/icon/order.png���� -   $��Order��#��[ � #��[ .+��� -   $ btnCus��#��[ .���� $ HighQuality=�$��/res/ava/ava_1.png���� -   $��Product��#��[ � $
��Left=�.+�F� ���	#FF000000$��1�����	#99000000����        � ��    	$��20q� � #��[ .+��� -   $ 
btnSetting��#��[ .���� $ HighQuality=�$��/res/icon/setting.png���� -   $��Setting��#��[ � #��[ .+��� -   $ btnExit��#��[ .���� $ HighQuality=�$��/res/icon/exit.png���� -   $
��Exit��#��[ � -    �Name$ Popup�� � AllowsTransparency$
 True��  � HorizontalOffset$  0��! � VerticalOffset$! 10��.��   MyShop.controls.UserPopUp  -   "  Name$" Header��
���   �� $��1�� �� $	��400��$��* ��� $	��300 �.+� � -   $��
fContainer��$��0��$��20q�#  � NavigationUIVisibility$# Hidden=��� $��1��$ 20, 0, 0, 20��
�����_�._��� .+�� $��0,0,10,0q�.+��� $��20q�#��[  
Calendar�� -   $ btnNotification��$��Right=�$��40��$��40��$��0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 8��.���� $ HighQuality=�$ ��/res/icon/notification.png��� $ � RowDefinitions$ �� $
��Auto ��� $	��30* ��� $��* �.+�&   System.Windows.Controls.Calendar  $��0��$��20q�$	��260���� $��1��
�������$ 30��._��� 
�������$��0q�$��15,15,10,15q��� $��2��
�������$ 30��$��80��$��15q�._��� 
�������$��0q�